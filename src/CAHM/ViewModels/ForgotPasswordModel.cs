using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace CAHM.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required, Email]
        public string Email { get; set; }
    }
}
