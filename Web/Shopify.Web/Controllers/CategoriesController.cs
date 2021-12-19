using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Services.Data;
using Shopify.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult All()
        {
            IEnumerable<AllCategoriesViewModel> viewModel = this.categoriesService.GetAll<AllCategoriesViewModel>();
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.CreateAsync(input);
            this.TempData["Message"] = "Category successfully created!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditCategoryInputModel input = this.categoriesService.GetById<EditCategoryInputModel>(id);
            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.UpdateAsync(input);
            this.TempData["Message"] = "Category updated successfully!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.categoriesService.DeleteAsync(id);
            this.TempData["Message"] = "Category deleted successfully!";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
