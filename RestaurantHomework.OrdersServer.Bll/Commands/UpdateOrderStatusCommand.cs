using MediatR;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Bll.Commands;

public record UpdateOrderStatusCommand(int Id, string Status) : IRequest<Unit>;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
{
    private readonly IOrdersRepository _ordersRepository;

    public UpdateOrderStatusCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.Query(request.Id, cancellationToken);
        if (order is null)
        {
            throw new ArgumentException("Такого заказ не существует");
        }

        order.Status = request.Status;

        using var transaction = _ordersRepository.CreateTransactionScope();

        await _ordersRepository.Update(order, cancellationToken);
        
        transaction.Complete();

        return Unit.Value;
    }
}