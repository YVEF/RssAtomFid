using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto.Account
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="The Field must be filled")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Field must be filled")]
        public string Password { get; set; }
    }
}
