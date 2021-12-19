using Shopify.Data.Models;
using Shopify.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Web.ViewModels.Categories
{
    public class EditCategoryInputModel : CreateCategoryInputModel, IMapFrom<Category>
    {
        public int Id { get; set; }
    }
}
