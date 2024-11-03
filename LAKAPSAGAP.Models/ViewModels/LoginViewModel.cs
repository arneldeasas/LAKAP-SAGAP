using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
