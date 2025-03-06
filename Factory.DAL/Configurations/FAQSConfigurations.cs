//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Factory.DAL.Models;

//namespace Factory.DAL.Data
//{
//    internal class FAQSConfigurations : IEntityTypeConfiguration<FAQS>
//    {
//        public void Configure(EntityTypeBuilder<FAQS> builder)
//        {
//            builder.ToTable("FAQs");

//            builder.HasKey(f => f.Id);

//            builder.Property(f => f.Question)
//                   .IsRequired()
//                   .HasMaxLength(500); 

//            builder.Property(f => f.Answer)
//                   .IsRequired()
//                   .HasMaxLength(1000); 

//            builder.Property(f => f.CreatedAt)
//                   .IsRequired();

//            builder.Property(f => f.IsArchived);

//            builder.Property(f => f.ArchivedAt);

//            builder.Property(f => f.IsDeleted);

//            builder.Property(f => f.DeletedAt);

//            builder.Property(f => f.UpdatedAt);

//            builder.Property(f => f.UserId)
//                   .IsRequired(); 

//            builder.HasOne(f => f.User)
//                   .WithMany() 
//                   .HasForeignKey(f => f.UserId)
//                   .OnDelete(DeleteBehavior.Restrict); 

//            builder.HasIndex(f => f.UserId);
//            builder.HasIndex(f => f.IsDeleted);
//        }
//    }
//}
