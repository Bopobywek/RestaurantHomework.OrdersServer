namespace RestaurantHomework.OrdersServer.Api.Responses;

public record GetDishResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt);