﻿using MediatR;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record CreateDishCommand(
    string Name,
    string Description,
    decimal Price,
    int Quantity) : IRequest<Unit>;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Unit>
{
    private readonly IDishesRepository _dishesRepository;

    public CreateDishCommandHandler(IDishesRepository dishesRepository)
    {
        _dishesRepository = dishesRepository;
    }

    public async Task<Unit> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = new DishEntity
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };

        using var transaction = _dishesRepository.CreateTransactionScope();
        
        await _dishesRepository.Add(dish, cancellationToken);
        
        transaction.Complete();

        return Unit.Value;
    }
}