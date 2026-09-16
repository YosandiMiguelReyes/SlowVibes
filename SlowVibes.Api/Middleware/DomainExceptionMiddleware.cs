using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace SlowVibes.Api.Middleware;

public sealed class DomainExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException exception)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Regla de dominio no satisfecha",
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = problem.Status.Value;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
