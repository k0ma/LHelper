namespace LHelper.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class CategoryBasicDetailsServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }        
    }
}
