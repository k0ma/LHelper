namespace LHelper.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {

        [Required]
        [MaxLength(DataConstants.UserNameMaxLength)]
        [MinLength(DataConstants.UserNameMinLength)]
        public string Name { get; set; }

        public string Password { get; set; }

        public List<Topic> Topics { get; set; } = new List<Topic>();

        public List<UserCategory> Categories { get; set; } = new List<UserCategory>();

        public List<Replay> Replies { get; set; } = new List<Replay>();        
    }
}
