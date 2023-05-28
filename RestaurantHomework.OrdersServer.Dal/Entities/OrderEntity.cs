namespace RestaurantHomework.OrdersServer.Dal.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = "В ожидании";
    public string SpecialRequests { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}