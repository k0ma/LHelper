namespace LHelper.Web.Models.Topics
{
    using Services.Forum.Models;
    using System.Collections.Generic;

    public class TopicDetailsViewModel
    {

        public TopicDetailsServiceModel Topic { get; set; }

        public IEnumerable<ReplayDetailsServiceModel> Replies { get; set; }
    }
}
