//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Factory.DAL.Configurations.Warehouses
//{
//    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
//    {
//        public void Configure(EntityTypeBuilder<Settings> builder)
//        {
//            builder.ToTable("Settings");

//            builder.HasKey(s => s.Id);

//            builder.Property(s => s.UserId)
//                .IsRequired()
//                .HasMaxLength(450);

//            builder.Property(s => s.ShowDesign1)
//                .IsRequired()
//                .HasDefaultValue(true);
//            builder.Property(s => s.ShowDesign2)
//                .IsRequired()
//                .HasDefaultValue(true);

//            builder.Property(s => s.ShowDesign3)
//                .IsRequired()
//                .HasDefaultValue(true);

//            builder.Property(s => s.CreatedAt)
//                .IsRequired()
//                .HasDefaultValueSql("GETUTCDATE()");
//            builder.Property(s => s.UpdatedAt)
//                .IsRequired()
//                .HasDefaultValueSql("GETUTCDATE()");

//            builder.HasIndex(s => s.UserId)
//                .IsUnique();
//        }
//    }

//}
