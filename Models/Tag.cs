using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEasy.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
