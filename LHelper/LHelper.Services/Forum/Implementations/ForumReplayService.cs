namespace LHelper.Services.Forum.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using LHelper.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class ForumReplayService : IForumReplayService
    {
        private readonly LHelperDbContext db;

        public ForumReplayService(LHelperDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateReplayAsync(int topicId, string userId, string content)
        {
            var existingТopicId = await this.db
                .FindAsync<Topic>(topicId);

            if (existingТopicId == null)
            {
                return false;
            }

            var replay = new Replay
            {
                Content = content,
                PublishDate = DateTime.UtcNow,
                UserId = userId,
                TopicId = topicId
            };

            this.db.Add(replay);
            await this.db.SaveChangesAsync();

            return true;
        }    

        public async Task<IEnumerable<ReplayDetailsServiceModel>> ByTopicIdAsync(int id)
        {
            var replies = await this.db
                .Replies
                .Where(r => r.TopicId == id)
                .ProjectTo<ReplayDetailsServiceModel>()
                .ToListAsync();

            return replies;
        }
    }
}
