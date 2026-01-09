using Microsoft.AspNetCore.Mvc;
//using Restaurant.Payment.Model;
using System.Net;

namespace Restaurant.Payment.WebApi.Middleware;

public class ErrorMiddleware(
    RequestDelegate next,
    ILogger<ErrorMiddleware> logger)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }



    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, title, detail) = SelectException(exception);

        if (status == (int)HttpStatusCode.InternalServerError)
            logger.LogError(exception, "Unhandled exception");
        else
            logger.LogWarning(exception, "Handled exception mapped to {Status}", status);

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    }

    private (int, string, string?) SelectException(Exception exception)
    {
        return exception switch
        {
            ArgumentException ex => ((int)HttpStatusCode.BadRequest, "Argumento inválido", ex.Message),
            //ValidationException ex => ((int)HttpStatusCode.BadRequest, "Erro de validação", ex.Message),
            //NotFoundException ex => ((int)HttpStatusCode.BadRequest, "Item não encontrado", ex.Message),
            //OrderStatusException ex => ((int)HttpStatusCode.BadRequest, "Operação não permitida", ex.Message),
            _ => ((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação, tente novamente mais tarde.", null)
        };
    }

}


public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorMiddleware>();
}