using Dapper;
using Microsoft.Extensions.Options;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Options;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Dal.Repositories;

public class OrdersRepository : BaseRepository, IOrdersRepository
{
    public OrdersRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<int> Add(OrderEntity order, CancellationToken cancellationToken)
    {
        const string sqlInsertOrder = @"insert into orders (user_id, status, special_requests)
values (@UserId, @Status, @SpecialRequests) returning id;";

        var sqlInsertOrderParams = new
        {
            order.UserId,
            order.Status,
            order.SpecialRequests,
        };

        await using var connection = await GetAndOpenConnection();
        var queryResult = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlInsertOrder,
                sqlInsertOrderParams,
                cancellationToken: cancellationToken));

        var orderId = queryResult?.FirstOrDefault() ?? 0;

        const string sqlInsert = @"insert into orders_dishes (order_id, dish_id, quantity, price)
    select @OrderId as order_id, id as dish_id, quantity, price from unnest(@Dishes);";
        var sqlInsertParams = new
        {
            OrderId = orderId,
            order.Dishes
        };
        
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlInsert,
                sqlInsertParams,
                cancellationToken: cancellationToken));

        return orderId;
    }

    public async Task Update(OrderEntity order, CancellationToken cancellationToken)
    {
        const string sqlUpdate = @"update orders set status = @Status where id = @Id";
        
        var sqlUpdateParams = new
        {
            order.Status
        };
        
        await using var connection = await GetAndOpenConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlUpdate,
                sqlUpdateParams,
                cancellationToken: cancellationToken));
    }

    public async Task<OrderEntity?> Query(int orderId, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id,
       user_id,
       status,
       special_requests
from orders
where id = @Id";
        
        var sqlQueryParams = new
        {
            Id = orderId
        };

        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<OrderEntity>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: cancellationToken));

        var order = result.SingleOrDefault();
        
        const string sqlQueryDishes = @"
select d.* 
from dishes as d join orders_dishes od on od.dish_id = d.id
where order_id = @Id";
        var sqlQueryDishesParams = new
        {
            Id = orderId
        };
        
        var dishes = await connection.QueryAsync<DishEntity>(
            new CommandDefinition(
                sqlQueryDishes,
                sqlQueryDishesParams,
                cancellationToken: cancellationToken));

        if (order is not null)
        {
            order.Dishes = dishes.ToArray();
        }

        return order;
    }

    public async Task<OrderEntity[]> QueryByStatus(string status, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id,
       user_id,
       status,
       special_requests
from orders
where status = @Status";
        
        var sqlQueryParams = new
        {
            Status = status
        };

        await using var connection = await GetAndOpenConnection();
        var orders = await connection.QueryAsync<OrderEntity>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: cancellationToken));

        const string sqlQueryDishes = @"
select d.* 
from dishes as d join orders_dishes od on od.dish_id = d.id
where order_id = @Id";

        var orderEntities = orders as OrderEntity[] ?? orders.ToArray();
        foreach (var order in orderEntities) {
            var sqlQueryDishesParams = new
            {
                order.Id
            };
        
            var dishes = await connection.QueryAsync<DishEntity>(
                new CommandDefinition(
                    sqlQueryDishes,
                    sqlQueryDishesParams,
                    cancellationToken: cancellationToken));

            order.Dishes = dishes.ToArray();
        }
        

        return orderEntities;
    }
}