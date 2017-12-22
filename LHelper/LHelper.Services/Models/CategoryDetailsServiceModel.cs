namespace LHelper.Services.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class CategoryDetailsServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Topic> Topics { get; set; } = new List<Topic>();
    }
}
