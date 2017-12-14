using System;
using System.ComponentModel.DataAnnotations;

namespace LHelper.Data.Models
{
    public class Replay
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ReplayDescriptionMaxLength)]
        [MinLength(DataConstants.ReplayDescriptionMinLength)]
        public string Content { get; set; }
        
        public DateTime PublishDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
