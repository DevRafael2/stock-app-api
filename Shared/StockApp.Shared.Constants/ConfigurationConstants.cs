namespace StockApp.Shared.Constants;

/// <summary>
/// Constantes de configuración
/// </summary>
public static class ConfigurationConstants
{
    /// <summary>
    /// Audiencia de token
    /// </summary>
    public static string TokenAudience => "tokenSettings:audience";
    
    /// <summary>
    /// Issuer del token 
    /// </summary>
    public static string TokenIssuer => "tokenSettings:issuer";
    
    /// <summary>
    /// Security key del token
    /// </summary>
    public static string TokenSecurityKey => "tokenSettings:key";
    
    /// <summary>
    /// Conexión a la base de datos
    /// </summary>
    public static string DbConnection => "defaultConnection";
}