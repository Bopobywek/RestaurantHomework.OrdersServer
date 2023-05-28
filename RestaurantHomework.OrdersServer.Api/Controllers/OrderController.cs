using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantHomework.OrdersServer.Api.ActionFilters;
using RestaurantHomework.OrdersServer.Api.Requests;
using RestaurantHomework.OrdersServer.Api.Responses;
using RestaurantHomework.OrdersServer.Api.Validators;
using RestaurantHomework.OrdersServer.Bll.Commands;
using RestaurantHomework.OrdersServer.Bll.Models;
using RestaurantHomework.OrdersServer.Bll.Queries;

namespace RestaurantHomework.OrdersServer.Api.Controllers;

[ApiController]
[Route("/api/orders")]
[Authorize(Roles = "customer,manager,chef")]
[ValidationExceptionFilter]
[ArgumentExceptionFilter]
public class OrderController
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
    {
        var validator = new CreateOrderRequestValidator();
        await validator.ValidateAndThrowAsync(request);
        
        var command = new CreateOrderCommand(
            request.UserId,
            request.Dishes
                .Select(
                    x => new OrderDishModel(
                        x.DishId,
                        x.Quantity)
                )
                .ToArray(),
            request.SpecialRequests);

        var id = await _mediator.Send(command);

        return new CreateOrderResponse(id);
    }

    [HttpGet("{id}")]
    public async Task<GetOrderInfoResponse> GetOrderInfo(int id)
    {
        var validator = new IdValidator();
        await validator.ValidateAndThrowAsync(id);
        
        var command = new GetOrderQuery(id);
        var result = await _mediator.Send(command);

        return new GetOrderInfoResponse(
            result.Id,
            result.Status,
            result.Dishes
                .Select(
                    x => new DishInfoResponse(
                        x.Id,
                        x.Name,
                        x.Description)
                )
                .ToArray(),
            result.SpecialRequests);
    }
}