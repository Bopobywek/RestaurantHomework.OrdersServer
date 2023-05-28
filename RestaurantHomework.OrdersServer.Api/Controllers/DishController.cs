using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHomework.OrdersServer.Api.ActionFilters;
using RestaurantHomework.OrdersServer.Api.Requests;
using RestaurantHomework.OrdersServer.Api.Responses;
using RestaurantHomework.OrdersServer.Api.Validators;
using RestaurantHomework.OrdersServer.Bll.Commands;
using RestaurantHomework.OrdersServer.Bll.Queries;

namespace RestaurantHomework.OrdersServer.Api.Controllers;

[ApiController]
[Route("/api/dishes")]
[Authorize(Roles = "manager")]
[ValidationExceptionFilter]
public class DishController
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateDishResponse> CreateDish(CreateDishRequest request)
    {
        var validator = new CreateDishRequestValidator();
        await validator.ValidateAndThrowAsync(request);
        
        var command = new CreateDishCommand(request.Name,
            request.Description,
            request.Price,
            request.Quantity);
        var id = await _mediator.Send(command);

        return new CreateDishResponse(id);
    }

    [HttpGet("{id}")]
    public async Task<GetDishResponse> GetDish(int id)
    {
        var validator = new IdValidator();
        await validator.ValidateAndThrowAsync(id);
        
        var command = new GetDishQuery(id);
        var result = await _mediator.Send(command);

        return new GetDishResponse(
            result.Id,
            result.Name,
            result.Description,
            result.Price,
            result.Quantity,
            result.CreatedAt,
            result.UpdatedAt);
    }

    [HttpGet]
    public async Task<GetDishesResponse> GetDishes([FromQuery] int take = 100, [FromQuery] int skip = 0)
    {
        var command = new GetDishesQuery(take, skip);
        var result = await _mediator.Send(command);

        return new GetDishesResponse(
            result.DishQueryResults
                .Select(x => new GetDishResponse(
                    x.Id,
                    x.Name,
                    x.Description,
                    x.Price,
                    x.Quantity,
                    x.CreatedAt,
                    x.UpdatedAt))
                .ToArray()
            );
    }

    [HttpPut("{id}")]
    public async Task PutDish(int id, UpdateDishRequest request)
    {
        var validator = new IdValidator();
        await validator.ValidateAndThrowAsync(id);
        
        var requestValidator = new UpdateDishRequestValidator();
        await requestValidator.ValidateAndThrowAsync(request);
        
        var command = new UpdateDishCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.Quantity);
        await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task DeleteDish(int id)
    {
        var validator = new IdValidator();
        await validator.ValidateAndThrowAsync(id);
        
        var command = new DeleteDishCommand(id);
        await _mediator.Send(command);
    }
}