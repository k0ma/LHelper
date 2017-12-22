namespace LHelper.Services.Models
{
    using LHelper.Common.Mapping;
    using LHelper.Data.Models;

    public class CategoryListingServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        //public List<UserCategory> Trainers { get; set; } = new List<UserCategory>();
    }
}
