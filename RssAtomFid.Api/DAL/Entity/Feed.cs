using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    public class Feed
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public string Media { get; set; }

        //public int? FeedsCollectionId { get; set; }
    }
}
