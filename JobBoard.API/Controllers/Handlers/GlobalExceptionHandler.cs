using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Application.Exceptions;

namespace JobBoard.API.Exceptions.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var code = exception switch
        {
            BusinessException be => be.Code,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = code,
            Title = exception.Message,
        };

        httpContext.Response.StatusCode = code;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}