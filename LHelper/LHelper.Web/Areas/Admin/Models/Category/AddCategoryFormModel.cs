namespace LHelper.Web.Areas.Admin.Models.Category
{
    using LHelper.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCategoryFormModel
    {
        [Required]
        [MaxLength(DataConstants.CategoryTitleMaxLength)]
        [MinLength(DataConstants.CategoryTitleMinLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.CategoryDescriptionMaxLength)]
        public string Description { get; set; }


        //[Required]
        //[Display(Name="Trainer")]
        //public string TrainerId { get; set; }

        public IEnumerable<string> SelectedTrainers { get; set; }
        
        [Display(Name = "Trainers")]
        public IEnumerable<SelectListItem> AllTrainers { get; set; }

        public bool ForEdit { get; set; }


    }
}
