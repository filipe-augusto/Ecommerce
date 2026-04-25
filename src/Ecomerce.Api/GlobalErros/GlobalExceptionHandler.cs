using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //para devs
       logger.LogError(exception, "Erro inesperado: {Message}", exception.Message);
       
        var problemDetails = new ProblemDetails
        {
            Status = 500,
            Title = "Ocorreu um erro inesperado",
            Detail = "Por favor, tente novamente mais tarde ou contate o suporte."
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync( problemDetails, cancellationToken );
        return true;
    }
}