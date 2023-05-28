using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestaurantHomework.OrdersServer.Api.ActionFilters;

public class ArgumentExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        ContentResult result;
        switch (context.Exception)
        {
            case ArgumentException argumentException:
                var response = new {ErrorMessage = argumentException.Message};
                result = new ContentResult
                {
                    Content = JsonSerializer.Serialize(response),
                    ContentType = "application/json",
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
                context.Result = result;
                return;
        }
    }
}