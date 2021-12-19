using Shopify.Data.Common.Repositories;
using Shopify.Data.Models;
using Shopify.Services.Mapping;
using Shopify.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task AddToShoppingListAsync(int productId, int shoppingListId)
        {
            Product product = this.productsRepository.All().FirstOrDefault(p => p.Id == productId);
            product.ShoppingListId = shoppingListId;
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateProductInputModel input)
        {
            Product product = new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                CategoryId = input.CategoryId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = this.productsRepository.All().FirstOrDefault(p => p.Id == id);
            this.productsRepository.Delete(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.productsRepository
                .All()
                .Where(p => !p.ShoppingListId.HasValue)
                .OrderBy(p => p.Id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int categoryId)
        {
            return this.productsRepository
                .All()
                .Where(p => !p.ShoppingListId.HasValue && p.CategoryId == categoryId)
                .OrderBy(p => p.Id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByShoppingListId<T>(int shoppingListId)
        {
            return this.productsRepository
                .All()
                .Where(p => p.ShoppingListId.HasValue && !p.IsBought && p.ShoppingListId.Value == shoppingListId)
                .OrderBy(p => p.Id)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.productsRepository
                .All()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetShoppingListIdByProductId(int productId)
        {
            return this.productsRepository
                .All()
                .FirstOrDefault(p => p.Id == productId)
                .ShoppingListId.Value;
        }

        public async Task MarkAsBoughtAsync(int productId)
        {
            Product product = this.productsRepository.All().FirstOrDefault(p => p.Id == productId);
            product.IsBought = true;
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditProductInputModel input)
        {
            Product product = this.productsRepository.All().FirstOrDefault(p => p.Id == input.Id);

            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.CategoryId = input.CategoryId;

            await this.productsRepository.SaveChangesAsync();
        }
    }
}
