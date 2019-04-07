using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    /// <summary>
    /// Feeds collection owned by user
    /// </summary>
    public class FeedsCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Comment { get; set; }

        public int TagId { get; set; }
        public int UserId { get; set; }
        public ICollection<FeedSource> FeedsSource { get; set; }

    }
}
