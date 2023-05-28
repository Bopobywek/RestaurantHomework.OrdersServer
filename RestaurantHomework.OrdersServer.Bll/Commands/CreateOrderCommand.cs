using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record CreateOrderCommand(
    int UserId,
    OrderDishModel[] Dishes,
    string SpecialRequests) : IRequest<int>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IDishesRepository _dishesRepository;

    public CreateOrderCommandHandler(IOrdersRepository ordersRepository, IDishesRepository dishesRepository)
    {
        _ordersRepository = ordersRepository;
        _dishesRepository = dishesRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new OrderEntity
        {
            UserId = request.UserId,
            Dishes = request.Dishes
                .Select(
                    x => new DishEntity
                    {
                        Id = x.Id,
                        Quantity = x.Quantity
                    }
                )
                .ToArray(),
            SpecialRequests = request.SpecialRequests
        };

        var dishIds = request.Dishes.Select(x => x.Id).OrderBy(x => x).ToArray();
        
        using var transaction = _ordersRepository.CreateTransactionScope();

        var dishes = (await _dishesRepository.Query(dishIds, cancellationToken))
            .ToDictionary(x => x.Id);
        
        foreach (var dish in request.Dishes)
        {
            if (!dishes.ContainsKey(dish.Id) || dishes[dish.Id].Quantity < dish.Quantity)
            {
                throw new ArgumentException();
            }

            dishes[dish.Id].Quantity -= dish.Quantity;
            await _dishesRepository.Update(dishes[dish.Id], cancellationToken);
        }
        
        var id = await _ordersRepository.Add(order, cancellationToken);
        
        transaction.Complete();
        
        return id;
    }
}