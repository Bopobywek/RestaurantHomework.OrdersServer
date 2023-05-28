using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Queries;

public record GetMenuQuery(int Take, int Skip) : IRequest<GetMenuQueryResult>;

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, GetMenuQueryResult>
{
    private readonly IDishesRepository _dishesRepository;

    public GetMenuQueryHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<GetMenuQueryResult> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var model = new DishesQueryModel(request.Take, request.Skip);
        var queryResult = await _dishesRepository.Query(model, cancellationToken);

        return new GetMenuQueryResult(
            queryResult
                .Where(x => x.Quantity > 0)
                .Select(x => new MenuItemModel(
                    x.Id,
                    x.Name,
                    x.Description,
                    x.Price)
                )
                .ToArray());
    }
}