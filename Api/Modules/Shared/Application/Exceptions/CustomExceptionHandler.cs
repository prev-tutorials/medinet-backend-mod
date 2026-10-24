using Api.Modules.Shared.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Modules.Shared.Application.Exceptions;

public record ErrorResponse(
    bool Error,
    string Title,
    string[] Details,
    int Status
);

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken ct
    )
    {
        var (status, title, details) = exception switch
        {
            ValidationException v => (
                StatusCodes.Status400BadRequest,
                "One or more validation errors occurred.",
                v.Errors.Select(e => e.ErrorMessage).ToArray()),

            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                "Not Found",
                new[] { exception.Message }),

            ConflictException => (
                StatusCodes.Status409Conflict,
                "Conflict",
                new[] { exception.Message }),

            _ => (0, string.Empty, Array.Empty<string>())
        };

        if (status == 0)
            return false;

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(
            new ErrorResponse(
                true, 
                title, 
                details, 
                status
            ), ct
        );

        return true;
    }
}