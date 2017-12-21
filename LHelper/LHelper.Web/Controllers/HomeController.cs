namespace LHelper.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Home;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICategoryService categories;

        public HomeController(ICategoryService categories)
        {
            this.categories = categories;
        }

        public async Task<IActionResult> Index()
            => View(new HomeIndexViewModel
            {
                Categories = await this.categories.AllAsyn()
            });


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
