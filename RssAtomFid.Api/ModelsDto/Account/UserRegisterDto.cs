using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RssAtomFid.Api.ModelsDto.Account
{
    public class UserRegisterDto
    {
        [StringLength(30, ErrorMessage = "UserName must be from 3 to 30 symbols", MinimumLength = 4)]
        [Required(ErrorMessage = "The Field must be filled")]
        public string UserName { get; set; }

        [StringLength(30, ErrorMessage = "Password must be from 3 to 30 symbols", MinimumLength = 2)]
        [Required(ErrorMessage = "The Field must be filled")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "The Field must be filled")]
        public string Email { get; set; }
    }
}
