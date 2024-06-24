
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModeloApi.Domain.Entities;

namespace ModeloApi.Infra.Data.EntitiesConfiguration;
public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("compra");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("cmp_id").UseIdentityColumn();

        builder.Property(x => x.ProductId).HasColumnName("pdt_id");

        builder.Property(x => x.PersonId).HasColumnName("psa_id");

        builder.Property(x => x.Date).HasColumnName("cmp_data");

        builder.HasOne(x => x.Person).WithMany(x => x.Purchases).HasForeignKey(x => x.PersonId);

        builder.HasOne(x => x.Product).WithMany(x => x.Purchases).HasForeignKey(x => x.ProductId);
    }
}
