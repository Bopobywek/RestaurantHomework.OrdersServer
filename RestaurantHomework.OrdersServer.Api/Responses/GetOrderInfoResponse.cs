namespace RestaurantHomework.OrdersServer.Api.Responses;

public record GetOrderInfoResponse(
    int Id,
    string Status,
    DishInfoResponse[] Dishes,
    string SpecialRequests);

public record DishInfoResponse(
    int Id, 
    string Name,
    string Description);