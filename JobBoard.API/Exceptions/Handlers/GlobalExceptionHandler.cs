using Microsoft.AspNetCore.Diagnostics;       
using Microsoft.AspNetCore.Http;              
using Microsoft.AspNetCore.Mvc;              
using Microsoft.Extensions.Logging;         
using System.Threading;                      
using System.Threading.Tasks;

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
            NotFoundException => StatusCodes.Status404NotFound,
            BusinessException => StatusCodes.Status400BadRequest,
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