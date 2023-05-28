﻿using MediatR;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Dal.Entities;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record CreateOrderCommand(
    int UserId,
    OrderDishModel[] Dishes,
    string SpecialRequests) : IRequest<Unit>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly IOrdersRepository _ordersRepository;

    public CreateOrderCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // TODO: УБРАТЬ СО СКЛАДА СООТВЕТСТВУЮЩЕЕ КОЛИЧЕСТВО ПРОДУКТОВ
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
        
        using var transaction = _ordersRepository.CreateTransactionScope();
        
        await _ordersRepository.Add(order, cancellationToken);
        
        transaction.Complete();
        
        return Unit.Value;
    }
}