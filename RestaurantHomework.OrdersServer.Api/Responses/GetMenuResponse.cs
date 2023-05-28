namespace RestaurantHomework.OrdersServer.Api.Responses;

public record GetMenuResponse(MenuItemResponse[] MenuItems);

public record MenuItemResponse(
    int Id,
    string Name,
    string Description,
    decimal Price);