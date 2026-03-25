using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;

namespace ShopEasy.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages", "ShopEasy");

            builder.HasKey(pi => pi.ProductImageId);

            builder.Property(pi => pi.Url)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(pi => pi.AltText)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(pi => pi.IsPrimary)
                   .HasDefaultValue(false);

            builder.HasOne(pi => pi.Product)
                   .WithOne(p => p.ProductImage)
                   .HasForeignKey<ProductImage>(pi => pi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}