namespace LHelper.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Topics;
    using Services.Forum;
    using System.Threading.Tasks;

    public class TopicsController : Controller
    {
        private readonly IForumTopicService topic;

        private readonly IForumReplayService replies;

        public TopicsController(IForumTopicService topic, IForumReplayService replies)
        {
            this.topic = topic;
            this.replies = replies;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new TopicDetailsViewModel
            {
                Topic = await this.topic.ByTopicIdAsync(id),
                Replies = await this.replies.ByTopicIdAsync(id)
            };

            if (model.Topic == null || model.Replies == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
