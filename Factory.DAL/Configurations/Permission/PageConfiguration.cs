using Factory.DAL.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Permission
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Controller)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(p => p.Submodule)
                .WithMany(s => s.Pages)
                .HasForeignKey(p => p.SubmoduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.SubmoduleId);

        }
    }
}