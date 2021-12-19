using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Web.Infrastructure.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopify.Web.ViewModels.Products
{
    public class CreateProductInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [ProductPrice(ErrorMessage = "Price should be positive!")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryItems { get; set; }
    }
}
