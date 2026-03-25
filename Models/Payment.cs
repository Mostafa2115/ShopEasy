using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        public string Method { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; } = DateTime.MinValue;
        public decimal Amount { get; set; }

        public Order Order { get; set; }
    }
}
