namespace RestaurantHomework.OrdersServer.Api.Requests;

public record CreateOrderRequest(int UserId, DishItemRequest[] Dishes, string SpecialRequests);

public record DishItemRequest(int DishId, int Quantity);