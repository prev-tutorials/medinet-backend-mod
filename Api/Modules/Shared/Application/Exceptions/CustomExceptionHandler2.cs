using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Shared.Application.Exceptions;

//archivo centraliza los exceptions

public class ConflictException(string message) : Exception(message){}


public class CustomExceptionHandler2(IProblemDetailsService problemDetailsService) 
: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken ct
    )
    {
        ProblemDetails? problemDetails = null;


        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key, 
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            problemDetails = new ValidationProblemDetails(errors)
            {
                Title  = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
            };
        }


        if (exception is KeyNotFoundException)
            problemDetails = new ProblemDetails
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = exception.Message,
            };
            

        if (exception is ConflictException)
            problemDetails = new ProblemDetails
            {
                Title  = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = exception.Message,
            };


        if(problemDetails == null)
            return false;

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext    = httpContext,
                ProblemDetails = problemDetails,
                Exception      = exception
            }
        );
    }
}