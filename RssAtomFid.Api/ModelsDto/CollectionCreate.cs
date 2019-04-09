using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto
{
    public class CollectionCreate
    {
        [StringLength(15, ErrorMessage = "Name field must be contains 2-15 symbols", MinimumLength = 2)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Comment { get; set; }
        //public string TagName { get; set; }
    }
}
