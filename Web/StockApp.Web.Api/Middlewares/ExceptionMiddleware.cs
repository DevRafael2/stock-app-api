using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Web.Api.Middlewares;

/// <summary>
/// Middlewares para excepciones
/// </summary>
/// <param name="next">Delegado de tubería http</param>
/// <param name="logger">ILogger</param>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{

    /// <summary>
    /// Metodo que contiene la logica del middleware
    /// </summary>
    /// <param name="context">Contexto http</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(
                $"Se dispara una excepción desde {context.Request.Path} en metodo {context.Request.Method}");
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = JsonConvert.SerializeObject(new ResponseApi
            {
                StatusResponse = StatusResponse.Error,
                Errors =
                [
                    ex.Message, ex.InnerException?.Message ?? "Sin InnerException",
                    ex.StackTrace ?? "Sin StackTrace"
                ],
                Message = $"Se ha presentado un error desconocido",
            });

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }
    }
    
    /// <summary>
    /// Metodo para serialziar la respuesta
    /// </summary>
    /// <param name="response">Respuesta</param>
    /// <returns>Retorna JsonString</returns>
    private string Serialize(ResponseApi response) =>
        JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
}



/// <summary>
/// Clase de extension para inyectar el middleware a la tuberia http
/// </summary>
public static class MiddlewareExtension
{
    /// <summary>
    /// Metodo que agrega el iddleware
    /// </summary>
    /// <param name="builder">Constructor de la aplicación</param>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}