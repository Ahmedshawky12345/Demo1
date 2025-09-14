using Demo1.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Infrastructure.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Product Constraint
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(maxLength: 100);
            builder.Property(x => x.Descrption).IsRequired();
            builder.HasCheckConstraint("CK_Product_Name_MinLength", "LEN([Name]) >= 5");
            builder.Property(x => x.Price).HasPrecision(18, 2).IsRequired();

            // product Relationships
            builder.HasOne(x=>x.Category).WithMany(x=>x.products).
                HasForeignKey(x=>x.CategoryId).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            

            


        }
    }
}
