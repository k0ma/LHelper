namespace LHelper.Data.Models
{
    public class UserTopic
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
