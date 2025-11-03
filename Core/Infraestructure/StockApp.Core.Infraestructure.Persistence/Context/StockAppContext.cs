using Microsoft.EntityFrameworkCore;

namespace StockApp.Core.Infraestructure.Persistence.Context;

/// <summary>
/// Contexto de base de datos 
/// </summary>
/// <param name="options">Opciones de contexto de base de datos</param>
public class StockAppContext(DbContextOptions<StockAppContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockAppContext).Assembly);
    }
}