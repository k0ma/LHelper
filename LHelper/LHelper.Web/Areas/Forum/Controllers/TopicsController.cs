﻿namespace LHelper.Web.Areas.Forum.Controllers
{
    using Data.Models;
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
                        new { Area = "", topicId = details.TopicId, categoryId = model.CategoryId});
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
