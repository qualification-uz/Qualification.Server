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
            Message = exception.ToString(),
            StatusCode = 500
        };
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}