using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    public class FeedSource
    {
        public int Id { get; set; }
        public FeedType Type { get; set; }
        public string Link { get; set; }

        /// <summary>
        /// Using the tag is selection from the database
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// This is a not required id because not all of users want add current item in own collection
        /// </summary>
        public int? FeedsCollectionId { get; set; }
    }


    public enum FeedType
    {
        RSS,
        Atom
    }
}
