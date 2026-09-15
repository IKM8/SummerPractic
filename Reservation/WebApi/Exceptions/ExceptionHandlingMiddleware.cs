using Domain.Exceptions;

namespace WebApi.Exceptions;

public class ExceptionHandlingMiddleware( RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger )
{
    public async Task InvokeAsync( HttpContext context )
    {
        try
        {
            await next( context );
        }
        catch ( EntityNotFoundException ex )
        {
            logger.LogWarning( ex, "Сущность не найдена" );
            await WriteResponse( context, StatusCodes.Status404NotFound, ex.Message );
        }
        catch ( BusinessRuleViolationException ex )
        {
            logger.LogWarning( ex, "Нарушение бизнес-правила" );
            await WriteResponse( context, StatusCodes.Status400BadRequest, ex.Message );
        }
        catch ( Exception ex )
        {
            logger.LogError( ex, "Непредвиденная ошибка" );
            await WriteResponse( context, StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера" );
        }
    }

    private static async Task WriteResponse( HttpContext context, int statusCode, string message )
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync( new { error = message } );
    }
}