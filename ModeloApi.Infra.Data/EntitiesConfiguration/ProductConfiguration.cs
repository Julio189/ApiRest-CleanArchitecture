
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModeloApi.Domain.Entities;

namespace ModeloApi.Infra.Data.EntitiesConfiguration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("produto");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("pdt_id").UseIdentityColumn();

        builder.Property(x => x.Name).HasColumnName("pdt_nome").HasMaxLength(150).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.CodErp).HasColumnName("pdt_codErp").HasMaxLength(20).IsRequired();
        builder.HasIndex(x => x.CodErp).IsUnique();

        builder.Property(x => x.Price).HasColumnName("pdt_preco").IsRequired();

        builder.HasMany(x => x.Purchases).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        
    }
}
