using Microsoft.AspNetCore.Mvc;

namespace RestaurantHomework.OrdersServer.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController
{
    [HttpPost]
    public async Task CreateOrder()
    {
    }
    
    [HttpGet("{id}")]
    public async Task GetInfo(int id)
    {
    }
}