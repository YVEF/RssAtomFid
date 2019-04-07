using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    public class DiscoverFeed
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ArticleCount { get; set; }
    }
}
