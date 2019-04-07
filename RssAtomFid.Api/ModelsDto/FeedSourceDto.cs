using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    public class FeedSourceDto
    {
        public string Type { get; set; }
        public string Link { get; set; }


        public int? FeedsCollectionId { get; set; }
    }
}
