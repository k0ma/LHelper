namespace LHelper.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LHelper.Services.Admin.Models;
    using AutoMapper.QueryableExtensions;
    using LHelper.Services.Forum.Models;

    public class AdminCategoryService : IAdminCategoryService
    {

        private readonly LHelperDbContext db;

        public AdminCategoryService(LHelperDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminCategoryBasicListingServiceModel>> AllAsync()
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new AdminCategoryBasicListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

        public async Task<IEnumerable<AdminCategoryAllListingModel>> AllCategoriesAsync()
            => await this.db
                .Categories
                .OrderBy(c => c.Id)
                .Select(c => new AdminCategoryAllListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UserNames = c.Trainers.Select(t => t.User.Name).ToList()
                })
            .ToListAsync();

        public async Task<CategoryDetailsServiceModel> ByIdAdminAsync(int id)
        {

            var category = this.db
                .Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDetailsServiceModel
                {
                    Id = c.Id,
                    Name= c.Name,
                    Description = c.Description,
                    SelectedTrainers = c.Trainers.Select(t => t.User.Name)
                })
                .FirstOrDefaultAsync();

            return await category;
        }

        public async Task<CategoryBasicDetailsServiceModel> ByIdAsync(int id)
        {
            var category = await this.db
                    .Categories
                    .Where(c => c.Id == id)
                    .ProjectTo<CategoryBasicDetailsServiceModel>()
                    .FirstOrDefaultAsync();

            return category;
        }
                    
        
            
        public async Task CreateAsync(string name, string description, IEnumerable<string> trainers)
        {
            var existingTrainerIds = await this.db
                .Users
                .Where(t => trainers.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync();

            var category = new Category
            {
                Name = name,
                Description = description
            };

            foreach (var trainerId in existingTrainerIds)
            {
                category.Trainers.Add(new UserCategory { UserId = trainerId });
            }

            this.db.Add(category);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await this.db
                .Categories
                .FindAsync(id);

            if (category == null)
            {
                return;
            }

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, string description)
        {
            var existingCategory = await this.db.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return;
            }

            existingCategory.Name = name;
            existingCategory.Description = description;

           await this.db.SaveChangesAsync();
        }
    }
}
