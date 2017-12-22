namespace LHelper.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.ForumArea)]
    [Authorize]
    public abstract class BaseForumController : Controller
    {
    }
}
