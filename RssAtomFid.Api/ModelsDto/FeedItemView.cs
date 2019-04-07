﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    /// <summary>
    /// Simple feed of rss or atom item
    /// </summary>
    public class FeedItemView
    {
        //[JsonIgnore]
        //public int Id { get; set; }
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public string Media { get; set; }        
    }
}
