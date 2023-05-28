using RestaurantHomework.OrdersServer.Dal.Entities;

namespace RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

public interface IOrdersRepository : IDbRepository
{
    Task<int> Add(OrderEntity order, CancellationToken cancellationToken);
    Task Update(OrderEntity order, CancellationToken cancellationToken);
    Task<OrderEntity?> Query(int orderId, CancellationToken cancellationToken);
    Task<OrderEntity[]> QueryByStatus(string status, CancellationToken cancellationToken);
}