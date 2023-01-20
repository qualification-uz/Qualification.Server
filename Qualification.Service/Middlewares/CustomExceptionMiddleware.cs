using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Qualification.Service.Exceptions;

namespace Qualification.Service.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await next(context);
        }
        catch (AlreadyExistsException alreadyExistsException)
        {
            var httpStatusCodeException =
                new HttpStatusCodeException(400, alreadyExistsException.Message);

            await HandleExceptionAsync(context, httpStatusCodeException);
        }
        catch (NotFoundException notFoundException)
        {
            var httpStatusCodeException =
                new HttpStatusCodeException(404, notFoundException.Message);

            await HandleExceptionAsync(context, httpStatusCodeException);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            var httpStatusCodeException =
                new HttpStatusCodeException(400, invalidOperationException.Message);

            await HandleExceptionAsync(context, httpStatusCodeException);
        }
        catch (HttpStatusCodeException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
    {
        context.Response.ContentType = "application/json";

        var result = new ErrorDetails()
        {
            Message = exception.Message,
            StatusCode = exception.StatusCode
        };
        context.Response.StatusCode = exception.StatusCode;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var result = new ErrorDetails()
        {
            Message = exception.Message,
            StatusCode = 500
        };
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}