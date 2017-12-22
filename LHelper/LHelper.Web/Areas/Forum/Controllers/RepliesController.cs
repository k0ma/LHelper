namespace LHelper.Web.Areas.Forum.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Replies;
    using Services.Forum;
    using Services.Html;
    using System.Threading.Tasks;

    public class RepliesController : BaseForumController
    {


        private readonly IHtmlService html;

        private readonly UserManager<User> userManager;

        private readonly IForumReplayService replay;

        public RepliesController(IHtmlService html, UserManager<User> userManager, IForumReplayService replay)
        {
            this.html = html;
            this.userManager = userManager;
            this.replay = replay;
        }

        public IActionResult Create(int id)
            => View(new PublishReplayFormModel
            {
                TopicId = id
            });

        [HttpPost]
        public async Task<IActionResult> Create(PublishReplayFormModel model, int id)
        {
            model.TopicId = id;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            var success = await this.replay.CreateReplayAsync(model.TopicId, userId, model.Content);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", "Topics",
                        new { Area = "", id = id });
        }
    }
}
