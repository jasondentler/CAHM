namespace CAHM.ViewModels
{
    public class NewPlayerModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public Location Location { get; set; }
    }
}
