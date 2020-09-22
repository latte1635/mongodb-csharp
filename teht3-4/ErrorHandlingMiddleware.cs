using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class NotFoundException : Exception
{
    public NotFoundException()
    {

    }
}

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            if (e is NotFoundException)
            {
                context.Response.StatusCode = 404;
            }
        }
    }
}

public class DateValidator : ValidationAttribute
{
    public override bool IsValid(object date)
    {
        return DateTime.Compare((DateTime)date, DateTime.Now) < 0;
    }
}