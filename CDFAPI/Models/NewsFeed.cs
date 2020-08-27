using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class NewsFeed
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string days { get; set; }
    }

    public class NewsFeedNew
    {
        public int NewsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string NewsItem { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}