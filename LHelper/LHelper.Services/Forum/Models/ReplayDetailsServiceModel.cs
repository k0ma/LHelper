namespace LHelper.Services.Forum.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ReplayDetailsServiceModel : IMapFrom<Replay>, IHaveCustomMapping
    {
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string User { get; set; }


        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Replay, ReplayDetailsServiceModel>()
            .ForMember(c => c.User, cfg => cfg.MapFrom(c => c.User.UserName));
    }
}
