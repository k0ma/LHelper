namespace LHelper.Web.Controllers
{
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EmailsController : Controller
    {
        private readonly IEmailService emails;

        public EmailsController(IEmailService emails)
        {
            this.emails = emails;
        }

        public async Task<IActionResult> Send(int topicId, int categoryId)
        {
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            

            await this.emails.SendEmails(topicId, categoryId, url);

            //var details = await this.topics.CreateAsync(model.Title, model.Description, model.CategoryId, userId);

            //if (!details.CategoryIdExist || details.TopicId < 0)
            //{
            //    return BadRequest();
            //}


            return RedirectToAction("Details", "Categories",
                        new { Area = "", id = categoryId });
        }

    }
}
