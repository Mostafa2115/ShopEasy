using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public CustomerProfile CustomerProfile { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
