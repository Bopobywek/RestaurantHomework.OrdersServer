using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Queries;

public record GetDishQuery(int Id) : IRequest<GetDishQueryResult>;

public class GetDishQueryHandler : IRequestHandler<GetDishQuery, GetDishQueryResult>
{
    private readonly IDishesRepository _dishesRepository;

    public GetDishQueryHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<GetDishQueryResult> Handle(GetDishQuery request, CancellationToken cancellationToken)
    {
        var dish = await _dishesRepository.Query(request.Id, cancellationToken);
        return new GetDishQueryResult(
            dish.Id,
            dish.Name,
            dish.Description,
            dish.Price,
            dish.Quantity);
    }
}