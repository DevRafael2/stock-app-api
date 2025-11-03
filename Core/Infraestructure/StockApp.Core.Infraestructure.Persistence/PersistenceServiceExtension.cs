using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Infraestructure.Persistence.Context;
using StockApp.Core.Infraestructure.Persistence.Repositories;
using StockApp.Shared.Constants;

namespace StockApp.Core.Infraestructure.Persistence;

/// <summary>
/// Clase estatica para agregar persistencia al
/// contenedor de dependencias
/// </summary>
public static class PersistenceServiceExtension
{
    /// <summary>
    /// Método extensivo para agregar persistencia
    /// al contenedor de dependencias
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    /// <param name="configuration">Configuración</param>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockAppContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(ConfigurationConstants.DbConnection));
        });
        
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}