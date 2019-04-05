using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.DAL.Entity
{
    public class FeedsCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }

        public ICollection<Feed> Feeds { get; set; }

        //public int? UserId { get; set; }
    }
}
