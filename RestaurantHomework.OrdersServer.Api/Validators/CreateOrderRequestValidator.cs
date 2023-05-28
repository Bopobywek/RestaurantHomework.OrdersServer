using FluentValidation;
using RestaurantHomework.OrdersServer.Api.Requests;

namespace RestaurantHomework.OrdersServer.Api.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleForEach(x => x.Dishes).SetValidator(new DishItemRequestValidator());
    }
}