using System.Linq;
using CAHM.Models;
using CAHM.ViewModels;
using Raven.Client;

namespace CAHM.Raven
{
    public class LogInAccounts : ILogInAccounts
    {
        private readonly IDocumentSession _session;
        private readonly IHasher _hasher;

        public LogInAccounts(IDocumentSession session, IHasher hasher)
        {
            _session = session;
            _hasher = hasher;
        }

        public bool Login(string email, string password, Location location)
        {
            email = email.ToLowerInvariant().Trim();
            var passwordHash = _hasher.Hash(email, password);

            var account = _session.Query<Account>()
                                  .SingleOrDefault(a => a.Email == email && a.PasswordHash == passwordHash);

            if (account == null)
                return false;

            account.Location = location;

            return true;
        }
    }
}
