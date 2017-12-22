namespace LHelper.Services
{
    using LHelper.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListingServiceModel>> AllAsyn();
    }
}
