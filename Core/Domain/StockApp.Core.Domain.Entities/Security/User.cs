using StockApp.Core.Domain.Entities.Stock;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Domain.Entities.Security;

/// <summary>
/// Entidad de usuarios
/// </summary>
public class User : EntityRoot<Guid>
{
    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Correo del usuario
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Stock modificado por el usuario
    /// </summary>
    public virtual ICollection<StockHistoryProduct>? StockHistoryProducts { get; set; } 
}