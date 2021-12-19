using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Web.ViewModels.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public interface IShoppingListsService
    {
        Task CreateAsync(CreateShoppingListInputModel input);

        Task UpdateAsync(EditShoppingListInputModel input);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByUserId<T>(string userId);

        IEnumerable<SelectListItem> GetAllByUserIdAsSelectListItems(string userId);
    }
}
