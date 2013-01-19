namespace CAHM.Models
{
    public class AccountReference
    {

        public string Id { get; set; }
        public string Email { get; set; }

        public static implicit operator AccountReference(Account account)
        {
            if (account == null)
                return null;
            return new AccountReference
                {
                    Id = account.Id,
                    Email = account.Email
                };
        }

    }
}
