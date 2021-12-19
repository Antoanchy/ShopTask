using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Data.Common.Repositories;
using Shopify.Data.Models;
using Shopify.Services.Mapping;
using Shopify.Web.ViewModels.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public class ShoppingListsService : IShoppingListsService
    {
        private readonly IDeletableEntityRepository<ShoppingList> shoppingListsRepository;

        public ShoppingListsService(IDeletableEntityRepository<ShoppingList> shoppingListsRepository)
        {
            this.shoppingListsRepository = shoppingListsRepository;
        }

        public async Task CreateAsync(CreateShoppingListInputModel input)
        {
            ShoppingList shoppingList = new ShoppingList
            {
                Name = input.Name,
                UserId = input.UserId,
            };

            await this.shoppingListsRepository.AddAsync(shoppingList);
            await this.shoppingListsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            ShoppingList shoppingList = this.shoppingListsRepository.All().FirstOrDefault(s => s.Id == id);
            this.shoppingListsRepository.Delete(shoppingList);
            await this.shoppingListsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            return this.shoppingListsRepository
                .All()
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.Id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllByUserIdAsSelectListItems(string userId)
        {
            return this.shoppingListsRepository
                .All()
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                })
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.shoppingListsRepository
                .All()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditShoppingListInputModel input)
        {
            ShoppingList shoppingList = this.shoppingListsRepository.All().FirstOrDefault(s => s.Id == input.Id);
            shoppingList.Name = input.Name;
            await this.shoppingListsRepository.SaveChangesAsync();
        }
    }
}
