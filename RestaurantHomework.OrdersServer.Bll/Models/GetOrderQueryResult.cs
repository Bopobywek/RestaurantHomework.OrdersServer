namespace RestaurantHomework.OrdersServer.Bll.Models;

public record GetOrderQueryResult(
    int Id,
    string Status,
    OrderQueryDishResult[] Dishes,
    string SpecialRequests);

public record OrderQueryDishResult(
    int Id,
    string Name,
    string Description);