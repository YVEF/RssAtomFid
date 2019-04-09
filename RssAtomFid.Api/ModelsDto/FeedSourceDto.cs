using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    public class FeedSourceDto
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Link { get; set; }


        public int? FeedsCollectionId { get; set; }
    }
}
