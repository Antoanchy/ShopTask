using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopify.Data.Models;
using Shopify.Services.Data;
using Shopify.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IShoppingListsService shoppingListsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IShoppingListsService shoppingListsService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.shoppingListsService = shoppingListsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            IEnumerable<AllProductsViewModel> viewModel = this.productsService.GetAll<AllProductsViewModel>();
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllByCategory(int categoryId)
        {
            IEnumerable<AllProductsViewModel> viewModel = this.productsService.GetAllByCategoryId<AllProductsViewModel>(categoryId);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllByShoppingList(int shoppingListId)
        {
            IEnumerable<AllProductsViewModel> viewModel = this.productsService.GetAllByShoppingListId<AllProductsViewModel>(shoppingListId);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            CreateProductInputModel input = new CreateProductInputModel
            {
                CategoryItems = this.categoriesService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            await this.productsService.CreateAsync(input);
            this.TempData["Message"] = "Product successfully created!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> AddToShoppingList(int productId)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AddToShoppingListInputModel input = new AddToShoppingListInputModel
            {
                ShoppingListItems = this.shoppingListsService.GetAllByUserIdAsSelectListItems(user.Id),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToShoppingList(int productId, AddToShoppingListInputModel input)
        {
            await this.productsService.AddToShoppingListAsync(productId, input.ShoppingListId);
            this.TempData["Message"] = "Successfully added product to list!";

            return this.RedirectToAction(nameof(this.AllByShoppingList), new { shoppingListId = input.ShoppingListId });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditProductInputModel input = this.productsService.GetById<EditProductInputModel>(id);
            input.CategoryItems = this.categoriesService.GetAllAsSelectListItems();
            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            await this.productsService.UpdateAsync(input);
            this.TempData["Message"] = "Product updated successfully!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.productsService.DeleteAsync(id);
            this.TempData["Message"] = "Product deleted successfully!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MarkAsBought(int id)
        {
            int shoppingListId = this.productsService.GetShoppingListIdByProductId(id);
            await this.productsService.MarkAsBoughtAsync(id);
            this.TempData["Message"] = "Successfully marked product as bought!";

            return this.RedirectToAction(nameof(this.AllByShoppingList), new { shoppingListId = shoppingListId });
        }
    }
}
