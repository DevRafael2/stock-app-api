using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Dtos.Entities.Security;
using StockApp.Core.Application.UseCases.Entities.User.Commands;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Web.Api.Controllers.V1;

/// <summary>
/// Controlador para autorización
/// </summary>
/// <param name="sender">Sender de mediatR</param>
[ApiController, Route("api/v1/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint para iniciar sesión
    /// </summary>
    /// <param name="credentials">Credenciales</param>
    /// <returns>Retorna respuesta relacionada al proceso, en caso tal
    /// con un token</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseApiData<OutSignIn>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignIn([FromBody] InSignIn credentials)
    {
        var result = await sender.Send(new SignInRequest(credentials));
        return StatusCode((int)result.StatusResponse, result);
    }
}