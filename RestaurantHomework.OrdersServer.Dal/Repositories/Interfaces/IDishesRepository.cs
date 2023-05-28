using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Models;

namespace RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

public interface IDishesRepository
{
    Task Add(DishEntity dish, CancellationToken cancellationToken);
    Task<List<DishEntity>> Query(DishesQueryModel model, CancellationToken cancellationToken);
    Task<DishEntity> Query(int id, CancellationToken cancellationToken);
    Task Update(DishEntity dish, CancellationToken cancellationToken);
    Task Delete(int dishId, CancellationToken cancellationToken);
}