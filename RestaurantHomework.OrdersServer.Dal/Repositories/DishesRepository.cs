using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Dal.Repositories;

public class DishesRepository : IDishesRepository
{
    public Task Add(DishEntity dish, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<DishEntity>> Query(DishesQueryModel model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DishEntity> Query(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(DishEntity dish, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int dishId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}