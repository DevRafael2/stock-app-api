using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Core.Domain.Primitives;

namespace StockApp.Core.Infraestructure.Persistence.Configuration.Base;

/// <summary>
/// Clase base de configuración para entidades
/// </summary>
/// <typeparam name="TEntity">Tipo de entidad</typeparam>
/// <typeparam name="TId">Tipo de id de la entidad</typeparam>
public class BaseConfigurationEntity<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : EntityRoot<TId>
{
    /// <summary>
    /// Método que implementa configuraciones basicas
    /// </summary>
    /// <param name="builder">Model Builder</param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        List<Type> numericIds = [typeof(byte), typeof(short), typeof(int), typeof(long)];
        builder.HasKey(x => x.Id);
        if (numericIds.Contains(typeof(TId)))
            builder.Property(x => x.Id).UseSerialColumn();
        else if (typeof(TId) == typeof(Guid))
            builder.Property(x => x.Id).HasColumnType("UUID")
                .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Id).HasColumnName("id").HasComment("Id del registro");
    }
}