namespace LHelper.Web.Models.Categories
{
    using Services.Forum.Models;
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class CategoryDetailsViewModel
    {
        public CategoryBasicDetailsServiceModel Category { get; set; }

        public IEnumerable<TopicBasicServiceModel> Topics { get; set; }
    }
}
