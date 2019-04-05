using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    public class FeedsCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<Feed> Feeds { get; set; }
    }
}
