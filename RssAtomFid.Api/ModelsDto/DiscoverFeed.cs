using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    public class DiscoverFeed
    {
        public int SourceId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ArticleCount { get; set; }
        public string Icon { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
