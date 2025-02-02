//using Factory.DAL.Models.Customers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Factory.DAL.Configurations
//{
//    public class OrderConfiguration : IEntityTypeConfiguration<Order>
//    {
//        public void Configure(EntityTypeBuilder<Order> builder)
//        {
//            builder.ToTable("Orders");

//            builder.HasKey(o => o.Id);

//            builder.Property(o => o.OrderNumber)
//                .IsRequired()
//                .HasMaxLength(100);

//            builder.Property(o => o.OrderDate)
//                .IsRequired();

//            builder.Property(o => o.GlassType)
//                .IsRequired()
//                .HasMaxLength(50);

//            builder.Property(o => o.Height)
//                .IsRequired();

//            builder.Property(o => o.Width)
//                .IsRequired();

//            builder.Property(o => o.Quantity)
//                .IsRequired();

//            builder.Property(o => o.EdgeFinish)
//                .HasMaxLength(50);

//            builder.Property(o => o.AdditionalNotes)
//                .HasMaxLength(500);

//            builder.Property(o => o.ProductionStatus)
//                .IsRequired()
//                .HasMaxLength(20);

//            builder.Property(o => o.EstimatedCompletionDate)
//                .IsRequired(false);

//            builder.Property(o => o.DeliveryMethod)
//                .HasMaxLength(50);

//            builder.Property(o => o.DeliveryDate)
//                .IsRequired(false);

//            builder.Property(o => o.DeliveryAddress)
//                .HasMaxLength(200);

//            builder.Property(o => o.TotalCost)
//                .HasColumnType("decimal(18,2)")
//                .IsRequired();

//            builder.Property(o => o.IsPaid)
//                .IsRequired()
//                .HasDefaultValue(false);

//            builder.Property(o => o.CreatedDate)
//                .IsRequired()
//                .HasDefaultValueSql("GETUTCDATE()");

//            builder.Property(o => o.ModifiedDate)
//                .IsRequired(false);

//            //builder.HasOne(o => o.Customer)
//            //    .WithMany(c => c.Orders)
//            //    .HasForeignKey(o => o.CustomerId);
//        }
//    }
//}
