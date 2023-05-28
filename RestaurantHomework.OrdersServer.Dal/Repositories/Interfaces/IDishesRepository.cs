using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Models;

namespace RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

public interface IDishesRepository : IDbRepository
{
    Task<int> Add(DishEntity dish, CancellationToken cancellationToken);
    Task<DishEntity[]> Query(DishesQueryModel model, CancellationToken cancellationToken);
    Task<DishEntity[]> Query(int[] dishIds, CancellationToken cancellationToken);
    Task<DishEntity?> Query(int id, CancellationToken cancellationToken);
    Task Update(DishEntity dish, CancellationToken cancellationToken);
    Task Delete(int dishId, CancellationToken cancellationToken);
}