using Shopify.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Data.Models
{
    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int? ShoppingListId { get; set; }

        public virtual ShoppingList ShoppingList { get; set; }

        public bool IsBought { get; set; }
    }
}
