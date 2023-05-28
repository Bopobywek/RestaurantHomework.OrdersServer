using MediatR;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record CreateDishCommand(
    string Name,
    string Description,
    decimal Price,
    int Quantity) : IRequest<int>;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
{
    private readonly IDishesRepository _dishesRepository;

    public CreateDishCommandHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = new DishEntity
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };

        using var transaction = _dishesRepository.CreateTransactionScope();
        
        var id = await _dishesRepository.Add(dish, cancellationToken);
        
        transaction.Complete();

        return id;
    }
}