namespace LHelper.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CategoryTitleMaxLength)]
        [MinLength(DataConstants.CategoryTitleMinLength)]
        public string Name { get; set; }


        [Required]
        [MaxLength(DataConstants.CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        public List<Topic> Topics { get; set; } = new List<Topic>();

        public List<UserCategory> Trainers { get; set; } = new List<UserCategory>();

    }
}
