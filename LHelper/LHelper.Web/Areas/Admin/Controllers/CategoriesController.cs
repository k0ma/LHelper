namespace LHelper.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Category;
    using Services.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Controllers;
    using System;

    public class CategoriesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;

        private readonly IAdminCategoryService categories;

        private readonly IAdminCategoryService allCategories;

        public CategoriesController(UserManager<User> usermanager, IAdminCategoryService categories, IAdminCategoryService allCategories)
        {
            this.userManager = usermanager;
            this.categories = categories;
            this.allCategories = allCategories;
        }
        public async Task <IActionResult> Create()
        {           
            return View(new AddCategoryFormModel
            {
                AllTrainers = await this.GetAllTrainers()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllTrainers = await this.GetAllTrainers();
                return View(model);
            }

            await this.categories.CreateAsync(
                model.Name,
                model.Description,
                model.SelectedTrainers);

            TempData.AddSuccessMessage($"Category {model.Name} was created successfully");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty});
        }

        public async Task<IActionResult> All()
        {
            var listingCategories = await this.allCategories.AllCategoriesAsync();
            return View(listingCategories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await this.allCategories.ByIdAdminAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(new AddCategoryFormModel
            {
                Name = category.Name,
                Description = category.Description,
                ForEdit = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ForEdit = true;
                return View(model);
            }

            await this.categories.EditAsync(
                id,
                model.Name,
                model.Description);

            TempData.AddSuccessMessage($"Category {model.Name} was edited successfully");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public IActionResult Delete(int id)
            => View(id);

        public async Task<IActionResult> Destroy(int id)
        {
           await this.categories.Delete(id);

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });

        }

        private async Task<IEnumerable<SelectListItem>> GetAllTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

            var trainersListItems = trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainersListItems;
        }
       
    }
}
