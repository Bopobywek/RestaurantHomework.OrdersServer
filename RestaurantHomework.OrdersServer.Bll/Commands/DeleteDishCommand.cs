using MediatR;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record DeleteDishCommand(int DishId) : IRequest<Unit>;

public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, Unit>
{
    private readonly IDishesRepository _dishesRepository;

    public DeleteDishCommandHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        await _dishesRepository.Delete(request.DishId, cancellationToken);
        return Unit.Value;
    }
}