namespace StockApp.Core.Application.Dtos.Entities.Security;

/// <summary>
/// Dto de entrada para inicio de sesión
/// </summary>
public class InSignIn
{
    /// <summary>
    /// Correo del usuario
    /// </summary>
    public string? Email { get; set; }    
    
    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    public string? Password { get; set; }
}

/// <summary>
/// Dto de salida para inicio de sesión
/// </summary>
public class OutSignIn
{
    /// <summary>
    /// Token 
    /// </summary>
    public string? Token { get; set; }
    
    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string? UserName { get; set; }
}