using MediatR;

namespace RestaurantHomework.OrdersServer.Bll.Models;

public record MenuItemModel(
    int Id,
    string Name,
    string Description,
    decimal Price);