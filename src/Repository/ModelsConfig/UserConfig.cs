using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.ModelsConfig;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(x => x.Id)
            .HasName("PK_Users");

        #region Table Attributes Configuration

        builder.Property(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsUnicode();

        builder.Property(e => e.Email)
            .IsRequired();

        builder.Property(e => e.Password)
            .IsRequired();

        builder.Property(e => e.Token)
           .IsRequired();

        #endregion Table Attributes Configuration
    }
}