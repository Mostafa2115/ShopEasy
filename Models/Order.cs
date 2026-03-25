using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Completed,
        Cancelled
    }
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; } = DateTime.MinValue;

        public Customer Customer { get; set; }
        public Payment Payment { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
