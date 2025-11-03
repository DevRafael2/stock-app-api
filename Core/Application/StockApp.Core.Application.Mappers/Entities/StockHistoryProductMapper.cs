using Mapster;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Core.Application.Dtos.Entities.Stock;
using StockApp.Core.Domain.Entities.Stock;

namespace StockApp.Core.Application.Mappers.Entities;

/// <summary>
/// Perfil de mapeos para el historial de movimientos
/// </summary>
public static class StockHistoryProductMapper
{
    /// <summary>
    /// Metodo para agregar las configuraciones de mapeos
    /// </summary>
    public static void AddStockHistoryProductMapper()
    {
        TypeAdapterConfig.GlobalSettings.NewConfig<StockHistoryProduct, OutStockHistoryProduct>()
            .Map(e => e.Operation, e => e.Amount <= -1 ? "Salida" : "Entrada")
            .Map(e => e.Product, e => e.Product == null ? "" : e.Product.Name);
    }
}