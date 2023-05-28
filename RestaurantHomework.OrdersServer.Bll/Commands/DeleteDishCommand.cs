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
        try
        {
            using var transaction = _dishesRepository.CreateTransactionScope();

            await _dishesRepository.Delete(request.DishId, cancellationToken);

            transaction.Complete();
        }
        catch (Exception)
        {
            throw new ArgumentException("Не удалось удалить блюдо");
        }



        return Unit.Value;
    }
}