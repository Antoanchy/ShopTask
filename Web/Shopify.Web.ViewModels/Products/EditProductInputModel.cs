using Shopify.Data.Models;
using Shopify.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Web.ViewModels.Products
{
    public class EditProductInputModel : CreateProductInputModel, IMapFrom<Product>
    {
        public int Id { get; set; }
    }
}
