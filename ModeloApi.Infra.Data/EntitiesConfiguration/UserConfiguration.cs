
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModeloApi.Domain.Entities;

namespace ModeloApi.Infra.Data.EntitiesConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("usuario");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("usr_id").UseIdentityColumn();

        builder.Property(x => x.Name).HasColumnName("usr_nome").HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Password).HasColumnName("usr_senha").HasMaxLength(500).IsRequired();

        builder.Property(x => x.Group).HasColumnName("usr_grupo").HasMaxLength(50).IsRequired();

        builder.Property(x => x.Status).HasColumnName("usr_status").IsRequired();
    }
}
