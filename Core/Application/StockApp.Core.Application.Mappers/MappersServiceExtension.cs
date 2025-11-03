using Mapster;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Core.Application.Mappers.Entities;
using StockApp.Core.Domain.Entities.Stock;

namespace StockApp.Core.Application.Mappers;

/// <summary>
/// Clase extensiva para agregar mappers
/// </summary>
public static class MappersServiceExtension
{
    /// <summary>
    /// Metodo para agregar maps
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        StockHistoryProductMapper.AddStockHistoryProductMapper();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddMapster();
        return services;
    }
}