using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel input);

        Task UpdateAsync(EditCategoryInputModel input);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
