namespace LHelper.Web.Models.Home
{
    using LHelper.Services.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel
    {
        public IEnumerable<CategoryListingServiceModel> Categories { get; set; }
    }
}
