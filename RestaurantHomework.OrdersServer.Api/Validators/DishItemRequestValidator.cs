using FluentValidation;
using RestaurantHomework.OrdersServer.Api.Requests;

namespace RestaurantHomework.OrdersServer.Api.Validators;

public class DishItemRequestValidator : AbstractValidator<DishItemRequest>
{
    public DishItemRequestValidator()
    {
        RuleFor(x => x.DishId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
    }
}