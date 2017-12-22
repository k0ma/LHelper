namespace LHelper.Web.Areas.Forum.Models.Replies
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class PublishReplayFormModel
    {

        [Required]
        [MaxLength(DataConstants.ReplayDescriptionMaxLength)]
        [MinLength(DataConstants.ReplayDescriptionMinLength)]
        public string Content { get; set; }

        public int TopicId { get; set; }

    }
}
