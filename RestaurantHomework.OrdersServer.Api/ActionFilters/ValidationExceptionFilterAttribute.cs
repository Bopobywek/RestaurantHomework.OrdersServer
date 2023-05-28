using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestaurantHomework.OrdersServer.Api.ActionFilters;

public class ValidationExceptionFilterAttribute: Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        ContentResult result;
        switch (context.Exception)
        {
            case ValidationException validationException:
                result = new ContentResult
                {
                    Content = validationException.Message,
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
                context.Result = result;
                return;
        }
    }
}