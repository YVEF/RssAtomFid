using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    public class CollectionCreate
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string TagName { get; set; }
    }
}
