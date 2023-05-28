using FluentValidation;

namespace RestaurantHomework.OrdersServer.Api.Validators;

public class IdValidator : AbstractValidator<int>
{
    public IdValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0);
    }
}