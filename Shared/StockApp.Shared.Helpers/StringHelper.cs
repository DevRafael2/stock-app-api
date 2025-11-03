using System.Security.Cryptography;
using System.Text;

namespace StockApp.Shared.Helpers;

/// <summary>
/// Clase estática que contiene helpers (extensiones)
/// para string's
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// Metodo mara encriptar cadenas de texto en SHA256
    /// </summary>
    public static string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}