﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantHomework.OrdersServer.Api.Requests;
using RestaurantHomework.OrdersServer.Api.Responses;
using RestaurantHomework.OrdersServer.Bll.Commands;
using RestaurantHomework.OrdersServer.Bll.Queries;

namespace RestaurantHomework.OrdersServer.Api.Controllers;

[ApiController]
[Route("dishes")]
public class DishController
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<string> CreateDish(CreateDishRequest request)
    {
        var command = new CreateDishCommand(request.Name,
            request.Description,
            request.Price,
            request.Quantity);
        await _mediator.Send(command);

        return "Ok";
    }
    
    [HttpGet("{id}")]
    public async Task<GetDishResponse> GetDish(int id)
    {
        var command = new GetDishQuery(id);
        var result = await _mediator.Send(command);

        return new GetDishResponse(
            result.Id,
            result.Name,
            result.Description,
            result.Price,
            result.Quantity);
    }
    
    [HttpGet]
    public void GetDishes([FromQuery] int take, [FromQuery] int skip)
    {
    }
    
    [HttpPut("{id}")]
    public void UpdateDish(int id)
    {
    }
    
    [HttpDelete("{id}")]
    public async Task<string> DeleteDish(int id)
    {
        var command = new DeleteDishCommand(id);
        await _mediator.Send(command);

        return "Ok";
    }
}