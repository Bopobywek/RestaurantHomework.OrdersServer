using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHomework.OrdersServer.BackgroundServices.Services;

namespace RestaurantHomework.OrdersServer.BackgroundServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<OrderMakerHostedService>();
        return services;
    }
}