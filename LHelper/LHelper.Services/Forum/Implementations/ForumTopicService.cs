namespace LHelper.Services.Forum.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ForumTopicService : IForumTopicService
    {
        private readonly LHelperDbContext db;

        public ForumTopicService(LHelperDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TopicBasicServiceModel>> ByCategoryIdAsync(int id)
        {
            var topics = await this.db
               .Topics
               .Where(t => t.CategoryId == id)
               .OrderByDescending(t => t.PublishDate)
               .Select(t => new TopicBasicServiceModel
               {
                   Id = t.Id,
                   Title = t.Title,
                   PublishDate = t.PublishDate,
                   User = t.User.Name,
                   Replies = t.Replies.Count
               })
               .ToListAsync();
                //.ProjectTo<TopicBasicServiceModel>()
                //.ToListAsync();

            return topics;
        }

        public async Task<TopicEditServiceModel> ByIdForEditAsync(int id)
            => await this.db
                .Topics
                .Where(t => t.Id == id)
                .Select(t => new TopicEditServiceModel
                {
                    Title = t.Title,
                    Description = t.Description
                })
                .FirstOrDefaultAsync();
        

        public async Task<TopicDetailsServiceModel> ByTopicIdAsync(int id)
        {
            var topic = await this.db
               .Topics
               .Where(t => t.Id == id)
               .Select(t => new TopicDetailsServiceModel
               {
                   Id = t.Id,
                   Title = t.Title,
                   Description = t.Description,
                   Category = t.Category.Name,
                   PublishDate = t.PublishDate,
                   User = t.User.Name,
                   Replies = t.Replies.Count
               })
               .FirstOrDefaultAsync();

            return topic;
        }

        public async Task<TopicEmailDetailsServiceModel> CreateAsync(string title, string description, int categoryId, string userId)
        {
            var details = new TopicEmailDetailsServiceModel { };

            var existingCategoryId = await this.db
                .FindAsync<Category>(categoryId);


            if (existingCategoryId == null)
            {
                return details;
            }

            details.CategoryIdExist = true;

            var topic = new Topic
            {
                Title = title,
                Description = description,
                PublishDate = DateTime.UtcNow,
                UserId = userId,
                CategoryId = categoryId
            };

            this.db.Add(topic);
            await this.db.SaveChangesAsync();

            details.TopicId = topic.Id;

            return details;
        }

        public async Task EditAsync(int id, string title, string description, int categoryId)
        {

            var existingTopic = await this.db.Topics.FindAsync(id);

            if (existingTopic == null)
            {
                return;
            }

            existingTopic.Title = title;
            existingTopic.Description = description;
            existingTopic.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }
    }
}
