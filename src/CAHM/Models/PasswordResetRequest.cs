namespace CAHM.Models
{
    public class PasswordResetRequest
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public bool Used { get; set; }

    }
}
