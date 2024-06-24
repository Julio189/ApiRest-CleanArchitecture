
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModeloApi.Domain.Entities;

namespace ModeloApi.Infra.Data.EntitiesConfiguration;
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("pessoa");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("psa_id").UseIdentityColumn();

        builder.Property(x => x.Name).HasColumnName("psa_nome").HasMaxLength(150).IsRequired();

        builder.Property(x => x.Document).HasColumnName("psa_documento").HasMaxLength(11).IsRequired();
        builder.HasIndex(x => x.Document).IsUnique();

        builder.Property(x => x.Phone).HasColumnName("psa_telefone").HasMaxLength(13).IsRequired();
        builder.HasIndex(x => x.Phone).IsUnique();

        builder.HasMany(x => x.Purchases).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Restrict);
    }
}
