using Dapper;
using Microsoft.Extensions.Options;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Models;
using RestaurantHomework.OrdersServer.Dal.Options;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Dal.Repositories;

public class DishesRepository : BaseRepository, IDishesRepository
{
    public DishesRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<int> Add(DishEntity dish, CancellationToken cancellationToken)
    {
        const string sqlInsert = @"insert into dishes (name, description, price, quantity)
values (@Name, @Description, @Price, @Quantity) returning id";

        var sqlInsertParams = new
        {
            dish.Name,
            dish.Description,
            dish.Price,
            dish.Quantity,
        };

        await using var connection = await GetAndOpenConnection();
        var queryResult = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlInsert,
                sqlInsertParams,
                cancellationToken: cancellationToken));

        return queryResult.FirstOrDefault();
    }

    public async Task<DishEntity[]> Query(DishesQueryModel model, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id,
       name,
       description,
       price,
       quantity,
       created_at,
       updated_at
from dishes
LIMIT @Limit OFFSET @Offset;";

        var sqlQueryParams = new
        {
            Limit = model.Take,
            Offset = model.Skip
        };

        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<DishEntity>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: cancellationToken));

        return result.ToArray();
    }
    public async Task<DishEntity[]> Query(int[] dishIds, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id,
       name,
       description,
       price,
       quantity,
       created_at,
       updated_at
from dishes
where id = ANY(@Ids)";
        
        var sqlQueryParams = new
        {
            Ids = dishIds
        };

        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<DishEntity>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: cancellationToken));

        return result.ToArray();
    }

    public async Task<DishEntity?> Query(int id, CancellationToken cancellationToken)
    {
        const string sqlQuery = @"
select id,
       name,
       description,
       price,
       quantity,
       created_at,
       updated_at
from dishes
where id = @Id";

        var sqlQueryParams = new
        {
            Id = id
        };

        await using var connection = await GetAndOpenConnection();
        var result = await connection.QueryAsync<DishEntity>(
            new CommandDefinition(
                sqlQuery,
                sqlQueryParams,
                cancellationToken: cancellationToken));

        return result?.FirstOrDefault();
    }

    public async Task Update(DishEntity dish, CancellationToken cancellationToken)
    {
        const string sqlUpdate = @"update dishes set 
                  name = @Name,
                  description = @Description,
                  price = @Price,
                  Quantity = @Quantity
              where id = @Id";

        var sqlUpdateParams = new
        {
            dish.Name,
            dish.Description,
            dish.Price,
            dish.Quantity
        };

        await using var connection = await GetAndOpenConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlUpdate,
                sqlUpdateParams,
                cancellationToken: cancellationToken));
    }

    public async Task Delete(int dishId, CancellationToken cancellationToken)
    {
        const string sqlDelete = @"delete from dishes where id = @Id";

        var sqlDeleteParams = new
        {
            Id = dishId
        };

        await using var connection = await GetAndOpenConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlDelete,
                sqlDeleteParams,
                cancellationToken: cancellationToken));
    }
}