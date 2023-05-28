using RestaurantHomework.OrdersServer.Dal.Entities;

namespace RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task Add(OrderEntity order, CancellationToken cancellationToken);
    Task<OrderEntity> Query(int orderId, CancellationToken cancellationToken);
}