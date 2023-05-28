using MediatR;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record UpdateDishCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity) : IRequest<Unit>;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Unit>
{
    private readonly IDishesRepository _dishesRepository;

    public UpdateDishCommandHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = new DishEntity
        {
            Id = request.Id,
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity
        };

        using var transaction = _dishesRepository.CreateTransactionScope();
        
        await _dishesRepository.Update(dish, cancellationToken);
        
        transaction.Complete();
        
        return Unit.Value;
    }
}