using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Core.Domain.Entities.Stock;
using StockApp.Core.Infraestructure.Persistence.Configuration.Base;

namespace StockApp.Core.Infraestructure.Persistence.Configuration.Entities.Stock;

/// <summary>
/// Configuración de productos
/// </summary>
public class ProductConfiguration : BaseConfigurationEntity<Product, int>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        base.Configure(builder);
        
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("Nombre del product");

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnName("amount")
            .HasComment("Cantidad actual del productoo");
        
        builder.HasMany(e => e.StockHistoryProducts).WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId).HasPrincipalKey(e => e.Id);
    }
}