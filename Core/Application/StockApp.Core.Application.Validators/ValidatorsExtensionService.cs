using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.Core.Application.Validators;

/// <summary>
/// Clase estatica para agregar validadores
/// </summary>
public static class ValidatorsExtensionService
{
    /// <summary>
    /// Método extensivo para agregar validadores
    /// </summary>
    /// <param name="services">Servicios</param>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ValidatorsExtensionService).Assembly);
        return services;
    }
}