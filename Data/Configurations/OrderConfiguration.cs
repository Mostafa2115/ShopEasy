using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Models;

namespace ShopEasy.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "ShopEasy");

            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .HasDefaultValue(OrderStatus.Pending.ToString());

            builder.Property(o => o.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.PlacedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(o => o.Status)
                   .HasDatabaseName("IX_Orders_PendingStatus")
                   .HasFilter("[Status] = 'Pending'");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
