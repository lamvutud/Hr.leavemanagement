using System.Net;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);

        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem;
        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ex.InnerException?.Message,
                    Title = ex.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails()
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.InnerException?.Message,
                    Title = ex.Message,
                    Type = nameof(NotFoundException)
                };
                break;

            default:
                problem = new CustomProblemDetails()
                {
                    Status = (int)statusCode,
                    Detail = ex.StackTrace,
                    Title = ex.Message,
                    Type = nameof(HttpStatusCode.InternalServerError)
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
