using Demo1.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo1.Infrastructure.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Product table
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasCheckConstraint("CK_Product_Title_MinLength", "LEN([Title]) >= 5");


            builder.Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            // Relationships
            builder.HasOne(x => x.Category)
                .WithMany(x => x.products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
