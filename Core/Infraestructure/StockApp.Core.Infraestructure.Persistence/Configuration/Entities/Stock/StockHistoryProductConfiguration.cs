using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Core.Domain.Entities.Stock;
using StockApp.Core.Infraestructure.Persistence.Configuration.Base;

namespace StockApp.Core.Infraestructure.Persistence.Configuration.Entities.Stock;

/// <summary>
/// Configuración de entidad que contiene historial de movimientos de producto
/// </summary>
public class StockHistoryProductConfiguration : BaseConfigurationEntity<StockHistoryProduct, int>
{
    public override void Configure(EntityTypeBuilder<StockHistoryProduct> builder)
    {
        builder.ToTable("stock_history_products");
        base.Configure(builder);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("UUID")
            .HasComment("Id del usuario que realizó el movimiento");

        builder.Property(e => e.ProductId)
            .IsRequired()
            .HasColumnName("product_id")
            .HasComment("Id del producto al que se le realizó movimiento");

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnName("amount")
            .HasComment("Cantidad a restar o sumar");
            
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasColumnType("date")
            .HasComment("Fecha en la que se realizó el movimiento");
        
        builder.HasOne(e => e.Product).WithMany(e => e.StockHistoryProducts)
            .HasForeignKey(e => e.ProductId).HasPrincipalKey(e => e.Id);
        
        builder.HasOne(e => e.User).WithMany(e => e.StockHistoryProducts)
            .HasForeignKey(e => e.UserId).HasPrincipalKey(e => e.Id);
    }
}