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

    public async Task Add(OrderEntity order, CancellationToken cancellationToken)
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
        
    }

    public Task<OrderEntity?> Query(int orderId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}