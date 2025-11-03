using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StockApp.Shared.Constants;

namespace StockApp.Web.Api.Extensions;

/// <summary>
/// Clase estatica para extender servicios de autenticación
/// </summary>
public static class AuthenticationServiceExtension
{
    /// <summary>
    /// Metodo para agregar servicios de autenticación
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    /// <param name="configuration">Configuraciones</param>
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration[ConfigurationConstants.TokenIssuer]!,
                        ValidAudience = configuration[ConfigurationConstants.TokenAudience]!,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateActor = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigurationConstants.TokenSecurityKey]!)),
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        return services;
    }
}