using Shopify.Data.Models;
using Shopify.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Web.ViewModels.Products
{
    public class AllProductsViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }
    }
}
