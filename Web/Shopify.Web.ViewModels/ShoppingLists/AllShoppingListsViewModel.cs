using Shopify.Data.Models;
using Shopify.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Web.ViewModels.ShoppingLists
{
    public class AllShoppingListsViewModel : IMapFrom<ShoppingList>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
