namespace LHelper.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly LHelperDbContext db;

        private readonly UserManager<User> userManager;

        public AdminUserService(LHelperDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            var allUsers = new List<AdminUserListingServiceModel>();

            foreach (var user in this.db.Users)
            {
                var roles = await this.userManager.GetRolesAsync(user);
                allUsers.Add(new AdminUserListingServiceModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Role = roles
                });
            }

            return allUsers;
        }
    }
}

