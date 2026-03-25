using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }

        public string InternalNotes { get; set; }

        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }

        public List<Product> Products { get; set; }
    }
}
