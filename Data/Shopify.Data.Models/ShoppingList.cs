using Shopify.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Data.Models
{
    public class ShoppingList : BaseDeletableModel<int>
    {
        public ShoppingList()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
