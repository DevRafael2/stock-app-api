using System.Reflection;
using Microsoft.OpenApi.Models;

namespace StockApp.Web.Api.Extensions;

/// <summary>
/// Extensión para servicio de swagger
/// </summary>
public static class SwaggerServiceExtension
{
    /// <summary>
    /// Metodo para agregar servicio de swagger
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock App Api", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingresa el token JWT con el formato: Bearer {tu token}"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            
            options.IncludeXmlComments(xmlPath);
        });
        
        return services;
    }
}