using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ProductImage ProductImage { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public virtual List<Review> Reviews { get; set; } = new List<Review>();


    }
}
