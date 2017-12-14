namespace LHelper.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(DataConstants.TopicTitleMaxLength)]
        [MinLength(DataConstants.TopicTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.TopicDescriptionMaxLength)]
        [MinLength(DataConstants.TopicDescriptionMinLength)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Replay> Replies { get; set; }
    }
}
