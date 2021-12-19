using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopify.Data.Models;
using Shopify.Services.Data;
using Shopify.Web.ViewModels.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Web.Controllers
{
    public class ShoppingListsController : Controller
    {
        private readonly IShoppingListsService shoppingListsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingListsController(IShoppingListsService shoppingListsService, UserManager<ApplicationUser> userManager)
        {
            this.shoppingListsService = shoppingListsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> AllByUser()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<AllShoppingListsViewModel> viewModel = this.shoppingListsService.GetAllByUserId<AllShoppingListsViewModel>(user.Id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateShoppingListInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.shoppingListsService.CreateAsync(input);
            this.TempData["Message"] = "Shopping list successfully created!";

            return this.RedirectToAction(nameof(this.AllByUser));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            EditShoppingListInputModel input = this.shoppingListsService.GetById<EditShoppingListInputModel>(id);
            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditShoppingListInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.shoppingListsService.UpdateAsync(input);
            this.TempData["Message"] = "Shopping list successfully updated!";

            return this.RedirectToAction(nameof(this.AllByUser));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.shoppingListsService.DeleteAsync(id);
            this.TempData["Message"] = "Shopping list successfully deleted!";

            return this.RedirectToAction(nameof(this.AllByUser));
        }
    }
}
