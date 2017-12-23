namespace LHelper.Web.Areas.Forum.Controllers
{
    using Data.Models;
    using LHelper.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Topics;
    using Services.Admin;
    using Services.Forum;
    using Services.Html;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class TopicsController : BaseForumController
    {
        private readonly IAdminCategoryService categories;

        private readonly IForumTopicService topics;

        private readonly IHtmlService html;

        private readonly UserManager<User> userManager;


        public TopicsController(IAdminCategoryService categories, IForumTopicService topics, IHtmlService html, UserManager<User> userManager)
        {
            this.categories = categories;
            this.topics = topics;
            this.html = html;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
            => View(new PublishTopicFormModel
            {
                Categories = await this.GetCategories()
            });

        [HttpPost]
        public async Task<IActionResult> Create(PublishTopicFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.GetCategories();
                return View(model);
            }

            model.Description = this.html.Sanitize(model.Description);

            var userId = this.userManager.GetUserId(User);

            var details = await this.topics.CreateAsync(model.Title, model.Description, model.CategoryId, userId);

            if (!details.CategoryIdExist || details.TopicId < 0)
            {
                return BadRequest();
            }

            return RedirectToAction("Send", "Emails",
                        new { Area = "", topicId = details.TopicId, categoryId = model.CategoryId });
        }

        [Authorize(Roles = WebConstants.TrainerRole + "," + WebConstants.AdministratorRole)]
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await this.topics.ByIdForEditAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            var listOgCategories = await this.GetCategories();

            return View(new PublishTopicFormModel
            {
                Title = topic.Title,
                Description = topic.Description,
                Categories = listOgCategories
            });
        }

        [Authorize(Roles = WebConstants.TrainerRole + "," + WebConstants.AdministratorRole)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PublishTopicFormModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.ForEdit = true;
                return View(model);
            }

            model.Description = this.html.Sanitize(model.Description);

            await this.topics.EditAsync(
                id,
                model.Title,
                model.Description,
                model.CategoryId);

            return RedirectToAction("Details", "Topics",
                        new { Area = "", id = id });
        }

        public IActionResult Delete(int id)
            => View(id);

        public async Task<IActionResult> Destroy(int id)
        {
           var result = await this.topics.Delete(id);

            if (!result.IsExist)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            return RedirectToAction("Details", "Categories",
                         new { Area = "", id = result.CategoryId });

        }


        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categories = await this.categories.AllAsync();

            var categoriesList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return categoriesList;
        }
    }
}
