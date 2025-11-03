namespace StockApp.Core.Domain.Primitives;

/// <summary>
/// Entidad de respuesta para api
/// </summary>
/// <typeparam name="TResponse">Tipo de dato que devuelve</typeparam>
public class ResponseApiData<TResponse> : ResponseApi
{
    /// <summary>
    /// Respuesta
    /// </summary>
    public TResponse? Data { get; set; }
}
public class ResponseApiDataPaginate<TResponse> : ResponseApiData<List<TResponse>>
{
    /// <summary>
    /// Pagina actual
    /// </summary>
    public int ActualPage { get; set; }
    /// <summary>
    /// Cantidad de páginas
    /// </summary>
    public int CountPages { get; set; }
    /// <summary>
    /// Cantidad de datos almacenados
    /// </summary>
    public int CountData { get; set; }
}

public class ResponseApi
{
    /// <summary>
    /// Mensaje que entrega la aplicación
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Errores que puedan surgir en la aplicación
    /// </summary>
    public List<string> Errors { get; set; } = new List<string>();
    /// <summary>
    /// Estado de la respuesta
    /// </summary>
    public StatusResponse StatusResponse { get; set; }
}


public enum StatusResponse
{
    /// <summary>
    /// Estado Ok
    /// </summary>
    Ok = 200,
    /// <summary>
    /// Estado Ok sin contenido
    /// </summary>
    NoContent = 202,
    /// <summary>
    /// No autorizado (acceso)
    /// </summary>
    UnAuthorize = 401,
    /// <summary>
    /// Sin permisos (protegido)
    /// </summary>
    Forbbiden = 403,
    /// <summary>
    /// Ocurre un erro controlado
    /// </summary>
    BadRequest = 400,
    /// <summary>
    /// Codigo para registros no encontrados
    /// </summary>
    NotFound = 404,
    /// <summary>
    /// Ocurre un error desconocido
    /// </summary>
    Error = 500
}

/// <summary>
/// Clase extensiva de response
/// </summary>
public static class ResponseExtension
{
    /// <summary>
    /// Devuelve la misma entidad en Not Found
    /// </summary>
    /// <param name="response">Respuesta</param>
    /// <param name="error">Error de not found</param>
    /// <returns>Retorna una respuesta NotFound</returns>
    public static ResponseApi NotFound(this ResponseApi response, string error)
    {
        response.Message = error;
        response.StatusResponse = StatusResponse.NotFound;
        return response;
    }

    /// <summary>
    /// Devuelve la misma entidad en bad request
    /// </summary>
    /// <param name="response">Respuesta</param>
    /// <param name="error">Error de bad request</param>
    /// <returns>Retorna una respuesta BadRequest</returns>
    public static ResponseApi BadRequest(this ResponseApi response, string error)
    {
        response.Message = error;
        response.StatusResponse = StatusResponse.BadRequest;
        return response;
    }
    
    /// <summary>
    /// Devuelve la respuesta en success
    /// </summary>
    /// <param name="response">Objeto base</param>
    /// <param name="data">Datos de la respuesta</param>
    /// <typeparam name="TResponse">Tipo del ResponseData</typeparam>
    /// <returns>Retorna el mismo objeto en success</returns>
    public static ResponseApiData<TResponse> Success<TResponse>(this ResponseApiData<TResponse> response, TResponse data, string message)
    {
        response.Message = message;
        response.StatusResponse = StatusResponse.Ok;
        return response;
    }
    
    /// <summary>
    /// Metodo que retorna el mismo response con un nuevo error en la lista
    /// </summary>
    /// <param name="response">Objeto base</param>
    /// <param name="newError">Error a agregar</param>
    /// <returns>Retorna el mismo objeto con un error nuevo</returns>
    public static ResponseApi AddNewError(this ResponseApi response, string newError)
    {
        response.Errors.Add(newError);
        return response;
    }
    
    /// <summary>
    /// Metodo que retorna el mismo response con un nuevo error en la lista
    /// </summary>
    /// <param name="response">Objeto base</param>
    /// <param name="newError">Error a agregar</param>
    /// <returns>Retorna el mismo objeto con un error nuevo</returns>
    public static ResponseApi AddNewErrors(this ResponseApi response, IEnumerable<string> newError)
    {
        response.Errors.AddRange(newError);
        return response;
    }
    
    /// <summary>
    /// Metodo que retorna el mismo response con un nuevo error en la lista
    /// </summary>
    /// <param name="response">Objeto base</param>
    /// <param name="newError">Error a agregar</param>
    /// <typeparam name="TResponse">Tipo del ResponseData</typeparam>
    /// <returns>Retorna el mismo objeto con un error nuevo</returns>
    public static ResponseApiData<TResponse> AddNewError<TResponse>(this ResponseApiData<TResponse> response, string newError)
    {
        response.Errors.Add(newError);
        return response;
    }
}