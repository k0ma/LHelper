namespace LHelper.Services.Admin.Models
{
    using System.Collections.Generic;

    public class AdminUserListingServiceModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IList<string> Role { get; set; } = new List<string>();
    }
}
