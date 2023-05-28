namespace RestaurantHomework.OrdersServer.Api.Requests;

public record UpdateDishRequest(
    string Name,
    string Description,
    decimal Price,
    int Quantity);