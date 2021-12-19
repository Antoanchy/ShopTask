using Shopify.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public interface IProductsService
    {
        Task CreateAsync(CreateProductInputModel input);

        Task UpdateAsync(EditProductInputModel input);

        Task DeleteAsync(int id);

        Task AddToShoppingListAsync(int productId, int shoppingListId);

        Task MarkAsBoughtAsync(int productId);

        int GetShoppingListIdByProductId(int productId);

        T GetById<T>(int id);

        IEnumerable<T> GetAllByCategoryId<T>(int categoryId);

        IEnumerable<T> GetAllByShoppingListId<T>(int shoppingListId);

        IEnumerable<T> GetAll<T>();
    }
}
