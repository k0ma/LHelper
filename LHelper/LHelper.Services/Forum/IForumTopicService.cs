namespace LHelper.Services.Forum
{
    using Forum.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumTopicService
    {
        Task<TopicEmailDetailsServiceModel> CreateAsync(string title, string description, int categoryId, string userId);

        Task<IEnumerable<TopicBasicServiceModel>> ByCategoryIdAsync(int id);

        Task<TopicDetailsServiceModel> ByTopicIdAsync(int id);

        Task<TopicEditServiceModel> ByIdForEditAsync(int id);

        Task EditAsync(int id, string title, string description, int categoryId);
    }
}
