using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Domain.Entities.Stock;

/// <summary>
/// Entidad de productos
/// </summary>
public class Product : EntityRoot<int>
{
    /// <summary>
    /// Nombre del producto
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Cantidad del producto
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// Movimientos de stock del producto
    /// </summary>
    public virtual ICollection<StockHistoryProduct>? StockHistoryProducts { get; set; } 
}