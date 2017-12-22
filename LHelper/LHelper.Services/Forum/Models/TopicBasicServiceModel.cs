namespace LHelper.Services.Forum.Models
{
    using System;

    public class TopicBasicServiceModel //: IMapFrom<Topic> , IHaveCustomMapping
    {

        public int Id { get; set; }
        
        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string User { get; set; }
        
        public int CategoryId { get; set; }

        public int Replies { get; set; }

        //public void ConfigureMapping(Profile mapper)
        //    => mapper
        //    .CreateMap<Topic, TopicBasicServiceModel>()
        //    .ForMember(c => c.User, cfg => cfg.MapFrom(c => c.User.UserName))
        //    .ForMember(c => c.Replies, cfg => cfg.MapFrom(c => c.Replies.Count));
    }
}
