using Factory.DAL.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Support
{
    public class FAQSConfiguration : IEntityTypeConfiguration<FAQS>
    {
        public void Configure(EntityTypeBuilder<FAQS> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Question).HasMaxLength(200).IsRequired();
            builder.Property(f => f.Answer).IsRequired();
            builder.Property(f => f.IsArchived).HasDefaultValue(false);
            builder.Property(f => f.IsDeleted).HasDefaultValue(false);
            builder.Property(f => f.Views).HasDefaultValue(0);
            builder.Property(f => f.HelpfulVotes).HasDefaultValue(0);
            builder.Property(f => f.UnhelpfulVotes).HasDefaultValue(0);
        }
    }
}