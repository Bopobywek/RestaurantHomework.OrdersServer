namespace RestaurantHomework.OrdersServer.Bll.Models;

public record GetDishQueryResult(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt);