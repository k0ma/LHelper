namespace LHelper.Web.Areas.Forum.Models.Topics
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PublishTopicFormModel
    {
        [Required]
        [MaxLength(DataConstants.TopicTitleMaxLength)]
        [MinLength(DataConstants.TopicTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.TopicDescriptionMaxLength)]
        [MinLength(DataConstants.TopicDescriptionMinLength)]
        public string Description { get; set; }
        
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
