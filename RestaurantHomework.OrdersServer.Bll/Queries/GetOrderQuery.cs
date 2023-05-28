using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Queries;

public record GetOrderQuery(int Id) : IRequest<GetOrderQueryResult>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResult>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrderQueryHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<GetOrderQueryResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.Query(request.Id, cancellationToken);

        return new GetOrderQueryResult(
            order.Id,
            order.Status,
            order.Dishes
                .Select(
                    x => new OrderQueryDishResult(
                        x.Id,
                        x.Name,
                        x.Quantity)
                )
                .ToArray(),
            order.SpecialRequests);
    }
}