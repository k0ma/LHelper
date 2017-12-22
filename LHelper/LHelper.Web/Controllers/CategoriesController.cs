namespace LHelper.Web.Controllers
{
    using LHelper.Services.Forum;
    using Microsoft.AspNetCore.Mvc;
    using Models.Categories;
    using Services.Admin;
    using System.Threading.Tasks;

    public class CategoriesController : Controller
    {
        private readonly IAdminCategoryService categories;

        private readonly IForumTopicService topics;

        public CategoriesController(IAdminCategoryService categories, IForumTopicService topics)
        {
            this.categories = categories;
            this.topics = topics;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new CategoryDetailsViewModel
            {
                Category = await this.categories.ByIdAsync(id),
                Topics = await this.topics.ByCategoryIdAsync(id)
            };

            if (model.Category == null || model.Topics == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
