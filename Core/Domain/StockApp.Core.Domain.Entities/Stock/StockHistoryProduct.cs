using StockApp.Core.Domain.Entities.Security;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Domain.Entities.Stock;

/// <summary>
/// Entidad de historial de movimiento del producto
/// </summary>
public class StockHistoryProduct : EntityRoot<int>
{
    /// <summary>
    /// Id del usuario que realizo el movimiento
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Id del producto al que se realiza movimiento
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Cantidad a resutar o sumar
    /// </summary>
    public int? Amount { get; set; }
    
    /// <summary>
    /// Fecha de creación del movimiento
    /// </summary>
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    /// <summary>
    /// Usuario que realizó el movimiento
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Producto al que se le realizó movimiento
    /// </summary>
    public virtual Product? Product { get; set; }
}