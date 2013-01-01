using System.Linq;
using CAHM.Models;
using Raven.Client;

namespace CAHM.Raven
{
    public class ChangeAccountPasswords : IChangeAccountPasswords
    {
        private readonly IDocumentSession _session;
        private readonly ICreateAccountResetRequests _createAccountResetRequests;
        private readonly IHasher _hasher;

        public ChangeAccountPasswords(IDocumentSession session, ICreateAccountResetRequests createAccountResetRequests, IHasher hasher)
        {
            _session = session;
            _createAccountResetRequests = createAccountResetRequests;
            _hasher = hasher;
        }

        public void ChangePassword(string email, string requestId, string requestHash, string password)
        {
            var request = _createAccountResetRequests.GetAccountResetRequest(email, requestId, requestHash);
            if (request == null || request.Used)
                throw new InvalidResetRequestException();

            request.Used = true;

            SetPassword(email, password);
        }

        public void SetPassword(string email, string password)
        {
            var account = _session.Query<Account>()
                                  .SingleOrDefault(a => a.Email == email);
            SetPassword(account, password);
        }

        public void SetPassword(Account account, string password)
        {
            if (account == null)
                throw new AccountNotFoundException();

            account.Email = account.Email.ToLowerInvariant().Trim();
            account.PasswordHash = _hasher.Hash(account.Email, password);
        }
    }
}
