using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{

    public class FeedItem
    {
        public string Title { get; set; }
        public DateTime PubDate { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        //public string Media { get; set; }        
    }
}
