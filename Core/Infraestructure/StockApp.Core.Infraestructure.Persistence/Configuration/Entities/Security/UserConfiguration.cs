using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Core.Domain.Entities.Security;
using StockApp.Core.Infraestructure.Persistence.Configuration.Base;

namespace StockApp.Core.Infraestructure.Persistence.Configuration.Entities.Security;

/// <summary>
/// Configuración de entidad usuario
/// </summary>
public class UserConfiguration : BaseConfigurationEntity<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        base.Configure(builder);
        
        builder.Property(e =>  e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name")
            .HasComment("Nombre del usuario");
        
        builder.Property(e =>  e.Email)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("email")
            .HasComment("Correo del usuario");
        
        builder.Property(e =>  e.Password)
            .IsRequired()
            .HasMaxLength(355)
            .HasColumnName("password")
            .HasComment("Contraseña del usuario");

        builder.HasIndex(e => e.Email).IsUnique();

        builder.HasMany(e => e.StockHistoryProducts).WithOne(e => e.User)
            .HasForeignKey(e => e.UserId).HasPrincipalKey(e => e.Id);
    }
}