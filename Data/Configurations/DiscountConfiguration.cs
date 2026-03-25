using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;

namespace ShopEasy.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {

        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts", "ShopEasy");

            builder.HasKey(d => d.DiscountId);

            //builder.HasSequence<int>("DiscountSeq", "ShopEasy")
            //       .StartsAt(1000)
            //       .IncrementsBy(1);

            builder.Property(d => d.DiscountId)
                   .HasDefaultValueSql("NEXT VALUE FOR ShopEasy.DiscountSeq");

            builder.Property(d => d.Code)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(d => d.Code)
                   .IsUnique()
                   .HasDatabaseName("IX_Discounts_Code");

            builder.Property(d => d.Percentage)
                   .HasColumnType("decimal(5,2)");

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

            builder.Property(d => d.MaxUses)
                   .HasDefaultValue(100);
        }
        

    }
}
