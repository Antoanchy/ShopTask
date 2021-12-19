using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopify.Web.Infrastructure.ValidationAttributes
{
    public class ProductPriceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is decimal price)
            {
                if (price <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
