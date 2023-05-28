using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantHomework.OrdersServer.Api.Responses;
using RestaurantHomework.OrdersServer.Bll.Queries;

namespace RestaurantHomework.OrdersServer.Api.Controllers;

[ApiController]
[Route("menu")]
public class MenuController
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<GetMenuResponse> GetMenu([FromQuery] int take = 100, [FromQuery] int skip = 0)
    {
        var command = new GetMenuQuery(take, skip);
        var result = await _mediator.Send(command);

        return new GetMenuResponse(result.MenuItems
            .Select(x => new MenuItemResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price)
            )
            .ToArray());
    }
}