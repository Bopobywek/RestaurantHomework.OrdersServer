using FluentValidation;
using RestaurantHomework.OrdersServer.Api.Requests;

namespace RestaurantHomework.OrdersServer.Api.Validators;

public class UpdateDishRequestValidator : AbstractValidator<UpdateDishRequest>
{
    public UpdateDishRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Price)
            .GreaterThan(0m);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);
    }
}