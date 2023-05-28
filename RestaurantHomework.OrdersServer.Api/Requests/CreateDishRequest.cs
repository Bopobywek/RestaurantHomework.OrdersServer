namespace RestaurantHomework.OrdersServer.Api.Requests;

public record CreateDishRequest(
    string Name,
    string Description,
    decimal Price,
    int Quantity);