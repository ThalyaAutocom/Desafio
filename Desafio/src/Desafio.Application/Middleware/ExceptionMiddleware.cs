using Desafio.Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Desafio.Application;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            var errorId = Guid.NewGuid().ToString();
            ErrorResult errorResult = new()
            {
                Exception = exception.Message,
                ErrorId = errorId
            };

            if(exception is FluentValidation.ValidationException fluentException)
            {
                errorResult.Exception = "One or more validations failed";
                foreach(var error in fluentException.Errors)
                {
                    errorResult.Messages.Add($"{error.ErrorMessage}");
                }
            }

            errorResult.StatusCode = (int)ReturnStatusCode(exception);
            response.StatusCode = errorResult.StatusCode;

            await response.WriteAsync(JsonSerializer.Serialize(errorResult));
        }
    }
    private HttpStatusCode ReturnStatusCode(Exception exception)
    {
        switch (exception)
        {
            case KeyNotFoundException:
                return HttpStatusCode.NotFound;
            case ValidationException:
                return HttpStatusCode.BadRequest;
            case FluentValidation.ValidationException:
                return HttpStatusCode.UnprocessableEntity;
            case CustomException e:
                return e.StatusCode;
            default:
               return HttpStatusCode.InternalServerError;
        }
    }
}
