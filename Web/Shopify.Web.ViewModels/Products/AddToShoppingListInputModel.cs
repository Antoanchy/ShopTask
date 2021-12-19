using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Web.ViewModels.Products
{
    public class AddToShoppingListInputModel
    {
        public int ShoppingListId { get; set; }

        public IEnumerable<SelectListItem> ShoppingListItems { get; set; }
    }
}
