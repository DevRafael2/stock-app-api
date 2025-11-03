using Microsoft.Extensions.DependencyInjection;

namespace StockApp.Core.Application.UseCases;

/// <summary>
/// Clase estática para agregar casos de uso
/// </summary>
public static class UseCasesServiceExtension
{
    /// <summary>
    /// Método para agregar casos de uso al contenedor
    /// de dependencias
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(UseCasesServiceExtension).Assembly));
        return services;
    }
}