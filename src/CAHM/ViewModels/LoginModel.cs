using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace CAHM.ViewModels
{
    public class LoginModel 
    {

        [Required, Email]
        public string Email { get; set; }

        [Required, StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Minimum password length is 5.")]
        public string Password { get; set; }

        public Location Location { get; set; }

    }
}
