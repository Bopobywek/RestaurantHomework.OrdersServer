using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Queries;

public record GetOrdersQuery(string Status) : IRequest<GetOrderQueryResult[]>;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrderQueryResult[]>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrdersQueryHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<GetOrderQueryResult[]> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.QueryByStatus(request.Status, cancellationToken);

        return orders.Select(
            x => new GetOrderQueryResult(
                x.Id,
                x.Status,
                x.Dishes.Select(
                        y => new OrderQueryDishResult(
                            y.Id,
                            y.Name,
                            y.Description))
                    .ToArray(),
                x.SpecialRequests
            )).ToArray();
    }
}