using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHomework.OrdersServer.Bll.Options;

namespace RestaurantHomework.OrdersServer.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBll(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
        return services;
    }
}