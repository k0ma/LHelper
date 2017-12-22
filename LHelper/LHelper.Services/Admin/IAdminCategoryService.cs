namespace LHelper.Services.Admin
{
    using LHelper.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminCategoryService
    {
        Task<CategoryBasicDetailsServiceModel> ByIdAsync(int id);

        Task<CategoryDetailsServiceModel> ByIdAdminAsync(int id);

        Task<IEnumerable<AdminCategoryBasicListingServiceModel>> AllAsync();

        Task<IEnumerable<AdminCategoryAllListingModel>> AllCategoriesAsync();        

        Task CreateAsync(string name, string description, IEnumerable<string> trainers);

        Task EditAsync(int id, string name, string description);

        Task Delete(int id);
    }
}
