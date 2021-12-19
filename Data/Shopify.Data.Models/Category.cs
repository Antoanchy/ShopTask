using Shopify.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
