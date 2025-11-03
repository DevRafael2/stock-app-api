using Microsoft.Extensions.DependencyInjection;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Primitives;
using StockApp.Core.Infraestructure.Persistence.Context;

namespace StockApp.Core.Infraestructure.Persistence.Repositories;

/// <summary>
/// Unidad de trabajo
/// </summary>
internal class UnitOfWork(
    IServiceProvider serviceProvider,
    StockAppContext context) : IUnitOfWork
{
    /// <summary>
    /// Diccionario de repositorios
    /// </summary>
    private Dictionary<Type, object> Repositories { get; set; } = new Dictionary<Type, object>();

    /// <inheritDoc />
    public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
        where TEntity : EntityRoot<TId>, new()
    {
        if (Repositories.ContainsKey(typeof(TEntity)))
            return (IRepository<TEntity, TId>)Repositories[typeof(TEntity)];

        var repository = serviceProvider.GetRequiredService<IRepository<TEntity, TId>>();
        Repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    /// <inheritDoc />
    public TRepository GetRepository<TRepository, TEntity, TId>()
        where TEntity : EntityRoot<TId>, new()
        where TRepository : notnull, IRepository<TEntity, TId>
    {
        if (Repositories.ContainsKey(typeof(TRepository)))
            return (TRepository)Repositories[typeof(TRepository)];

        var repository = serviceProvider.GetRequiredService<TRepository>();
        Repositories.Add(typeof(TRepository), repository!);
        return repository;
    }

    /// <inheritDoc />
    public async Task CompleteAsync() =>
        await context.SaveChangesAsync();
}