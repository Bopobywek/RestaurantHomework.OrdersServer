﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantHomework.OrdersServer.Dal.Infrastructure;
using RestaurantHomework.OrdersServer.Dal.Options;
using RestaurantHomework.OrdersServer.Dal.Repositories;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        return services;
    }
    
    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services, 
        IConfigurationRoot config)
    {
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));
        
        Postgres.MapCompositeTypes();
        
        Postgres.AddMigrations(services);
        
        return services;
    }
}