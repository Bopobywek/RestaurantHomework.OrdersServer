using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Models;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Queries;

public record GetDishesQuery(int Take, int Skip) : IRequest<GetDishesQueryResult>;

public class GetDishesQueryHandler : IRequestHandler<GetDishesQuery, GetDishesQueryResult>
{
    private readonly IDishesRepository _dishesRepository;

    public GetDishesQueryHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<GetDishesQueryResult> Handle(GetDishesQuery request, CancellationToken cancellationToken)
    {
        var filter = new DishesQueryModel(request.Take, request.Skip);
        var dishes = await _dishesRepository.Query(filter, cancellationToken);
        if (dishes is null)
        {
            throw new ArgumentException("Не удалось получить блюда");
        }

        return new GetDishesQueryResult(
            dishes.Select(
                    x => new GetDishQueryResult(
                        x.Id,
                        x.Name,
                        x.Description,
                        x.Price,
                        x.Quantity,
                        x.CreatedAt,
                        x.UpdatedAt)
                )
                .ToArray()
        );
    }
}