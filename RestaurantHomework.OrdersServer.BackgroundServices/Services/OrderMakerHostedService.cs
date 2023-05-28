using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantHomework.OrdersServer.Bll.Commands;
using RestaurantHomework.OrdersServer.Bll.Queries;

namespace RestaurantHomework.OrdersServer.BackgroundServices.Services;

public class OrderMakerHostedService : BackgroundService
{
    private readonly IServiceProvider _services;

    public OrderMakerHostedService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _services.CreateScope();
            var command = new GetOrdersQuery("в ожидании");
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var results = await mediator.Send(command, cancellationToken);

            foreach (var order in results)
            {
                var updateCommand = new UpdateOrderStatusCommand(order.Id, "в работе");
                await mediator.Send(updateCommand, cancellationToken);
                    
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                    
                updateCommand = new UpdateOrderStatusCommand(order.Id, "выполнен");
                await mediator.Send(updateCommand, cancellationToken);
            }
            
            await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
        }
    }
}