namespace StockApp.Core.Domain.Primitives;

/// <summary>
/// Entidad base
/// </summary>
/// <typeparam name="TId">Tipo de id de la entidad</typeparam>
public class EntityRoot<TId>
{
    /// <summary>
    /// Id del registro
    /// </summary>
    public TId? Id { get; set; }
}