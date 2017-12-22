namespace LHelper.Services.Admin.Models
{
    using System.Collections.Generic;

    public class AdminCategoryAllListingModel : AdminCategoryBasicListingServiceModel
    {
        public string Description { get; set; }

        public List<string> UserNames { get; set; } = new List<string>();
    }
}
