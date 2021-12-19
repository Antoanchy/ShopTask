using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Data.Common.Repositories;
using Shopify.Data.Models;
using Shopify.Services.Mapping;
using Shopify.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CreateCategoryInputModel input)
        {
            Category category = new Category
            {
                Name = input.Name,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = this.categoriesRepository.All().FirstOrDefault(c => c.Id == id);
            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoriesRepository
                .All()
                .OrderBy(c => c.Id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.categoriesRepository
                .All()
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                })
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.categoriesRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditCategoryInputModel input)
        {
            Category category = this.categoriesRepository.All().FirstOrDefault(c => c.Id == input.Id);
            category.Name = input.Name;
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
