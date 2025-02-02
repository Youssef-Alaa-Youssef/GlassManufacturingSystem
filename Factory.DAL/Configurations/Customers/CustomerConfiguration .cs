//using Factory.DAL.Models.Customers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Factory.DAL.Configurations
//{
//    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
//    {
//        public void Configure(EntityTypeBuilder<Customer> builder)
//        {
//            // Table Name
//            builder.ToTable("Customers");

//            // Primary Key
//            builder.HasKey(c => c.Id);

//            // Name Fields
//            builder.Property(c => c.FirstNameEnglish)
//                .IsRequired()
//                .HasMaxLength(50);

//            builder.Property(c => c.LastNameEnglish)
//                .IsRequired()
//                .HasMaxLength(50);

//            builder.Property(c => c.FirstNameArabic)
//                .HasMaxLength(50);

//            builder.Property(c => c.LastNameArabic)
//                .HasMaxLength(50);

//            // Contact Information
//            builder.Property(c => c.Email)
//                .IsRequired()
//                .HasMaxLength(150);

//            builder.Property(c => c.PhoneNumber)
//                .HasMaxLength(20);

//            // Address Fields
//            builder.Property(c => c.Address)
//                .HasMaxLength(200);

//            builder.Property(c => c.City)
//                .HasMaxLength(50);

//            builder.Property(c => c.State)
//                .HasMaxLength(50);

//            builder.Property(c => c.PostalCode)
//                .HasMaxLength(20);

//            builder.Property(c => c.Country)
//                .HasMaxLength(50);

//            // Other Fields
//            //builder.Property(c => c.DateOfBirth)
//            //    .IsRequired(false);

//            builder.Property(c => c.CustomerType)
//                .HasMaxLength(20);

//            builder.Property(c => c.RegistrationDate)
//                .HasDefaultValueSql("GETUTCDATE()");

//            builder.Property(c => c.IsActive)
//                .IsRequired()
//                .HasDefaultValue(true);

//            // Relationships
//            //builder.HasMany(c => c.Orders)
//            //    .WithOne(o => o.Customer)
//            //    .HasForeignKey(o => o.CustomerId);
//        }
//    }
//}
