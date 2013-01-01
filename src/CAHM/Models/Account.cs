using CAHM.ViewModels;

namespace CAHM.Models
{
    public class Account
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Location Location { get; set; }

    }
}
