using CAHM.Models;
using CAHM.ViewModels;
using Raven.Abstractions.Exceptions;
using Raven.Client;

namespace CAHM.Raven
{
    public class RegisterAccounts : IRegisterAccounts
    {
        private readonly IDocumentSession _session;
        private readonly IChangeAccountPasswords _changeAccountPasswords;

        public RegisterAccounts(IDocumentSession session, IChangeAccountPasswords changeAccountPasswords)
        {
            _session = session;
            _changeAccountPasswords = changeAccountPasswords;
        }

        public void Register(string email, string password, Location location)
        {
            var account = new Account
                {
                    Email = email,
                    Location = location
                };
            
            _changeAccountPasswords.SetPassword(account, password);

            try
            {
                _session.StoreUnique(account, a => a.Email);
            }
            catch (ConcurrencyException)
            {
                throw new AccountAlreadyExistsException();
            }

        }
    }
}
