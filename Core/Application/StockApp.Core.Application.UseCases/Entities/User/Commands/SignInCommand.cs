using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockApp.Core.Application.Dtos.Entities.Security;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Primitives;
using StockApp.Shared.Constants;
using StockApp.Shared.Helpers;

namespace StockApp.Core.Application.UseCases.Entities.User.Commands;

using Domain.Entities.Security;

/// <summary>
/// Request para inicio de sesión
/// </summary>
/// <param name="Credentials">Credenciales del usuario</param>
public record SignInRequest(InSignIn Credentials) : IRequest<ResponseApi>;

/// <summary>
/// Comando para iniciar sesión
/// </summary>
/// <param name="repository">Repositorio para interactuar con los usuarios</param>
public class SignInCommand(
    IConfiguration configuration,
    IRepository<User, Guid> repository) : IRequestHandler<SignInRequest, ResponseApi>
{
    /// <summary>
    /// Handler para iniciar sesión
    /// </summary>
    /// <param name="request">Credenciales del usuario</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    public async Task<ResponseApi> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var passCrypt = StringHelper.ComputeSha256Hash(request.Credentials.Password!);
        var userExist = await repository
            .GetFirstAsync(
                firstOrDefault: e => e.Email!.ToLower() == request.Credentials.Email!.ToLower() &&
                                    e.Password == passCrypt,
                select: e => new() { Id = e.Id, Name = e.Name });
        
        if (userExist is null)
            return new ResponseApi { Message = "Credenciales incorrectas", StatusResponse = StatusResponse.UnAuthorize };

        var token = CreateToken(userExist.Id, request.Credentials.Email!);

        return new ResponseApiData<OutSignIn> { 
            StatusResponse = StatusResponse.Ok, 
            Message = "Usuario autenticado correctamente", 
            Data = new()
            {
                Token = token,
                UserName = userExist.Name!,
            }
        };
    }

    /// <summary>
    /// Metodo para generar un token de autenticación
    /// </summary>
    /// <param name="userId">Id del usuario</param>
    /// <param name="email">Correo del usuario</param>
    /// <returns>Retorna token del usuario</returns>
    private string CreateToken(Guid userId, string email)
    {
        var createdAt = DateTime.UtcNow.ToString("dd-MM-yyyy");
        var claims = new List<Claim> {
            new (ClaimTypes.Name, userId.ToString()),
            new ("email", email),
            new ("userId", userId.ToString()),
            new ("createdAt", createdAt),
        };

        var keySymmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigurationConstants.TokenSecurityKey]!));
        var creds = new SigningCredentials(keySymmetric, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration[ConfigurationConstants.TokenIssuer]!,
            audience: configuration[ConfigurationConstants.TokenAudience]!,
            claims: claims,
            expires: DateTime.Now.AddDays(10),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}