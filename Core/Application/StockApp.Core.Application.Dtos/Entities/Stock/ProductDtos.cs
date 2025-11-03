namespace StockApp.Core.Application.Dtos.Entities.Stock;

/// <summary>
/// Dto de entrada para agregar productos
/// </summary>
public class InProduct
{
    /// <summary>
    /// Nombre del producto
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Cantidad del producto
    /// </summary>
    public int Amount { get; set; }
}

/// <summary>
/// Dto de salida del producto
/// </summary>
public class OutProduct : InProduct
{
    /// <summary>
    /// Id del producto 
    /// </summary>
    public int Id { get; set; }
}

/// <summary>
/// Dto de entrada para historial de productos
/// </summary>
public class InStockHistoryProduct
{
    /// <summary>
    /// Id del producto
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Cantidad a sumar o restar
    /// </summary>
    public int Amount { get; set; }
}

/// <summary>
/// Dto simple de salida para stock
/// </summary>
public class OutSampleStockHistoryProduct : InStockHistoryProduct
{
    /// <summary>
    /// Id del usuario
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Dto de salida para datos paginados
/// </summary>
public class OutStockHistoryProduct
{
    /// <summary>
    /// Producto
    /// </summary>
    public string? Product { get; set; }
    
    /// <summary>
    /// Operación entrada | salida
    /// </summary>
    public string? Operation { get; set; }
    
    /// <summary>
    /// Cantidad del movimiento
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// Fecha de realización
    /// </summary>
    public DateOnly CreatedAt { get; set; }
}