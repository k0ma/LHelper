namespace LHelper.Services.Forum
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumReplayService
    {
        Task<IEnumerable<ReplayDetailsServiceModel>> ByTopicIdAsync(int id);

        Task<bool> CreateReplayAsync(int topicId, string userId, string content);
    }
}
