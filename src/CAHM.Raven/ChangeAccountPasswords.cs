using System;
using Raven.Client;

namespace CAHM.Raven
{
    public class ChangeAccountPasswords : IChangeAccountPasswords
    {
        private readonly IDocumentSession _session;

        public ChangeAccountPasswords(IDocumentSession session)
        {
            _session = session;
        }

        public void ChangePassword(string email, string requestHash, string password)
        {
            throw new NotImplementedException();
        }
    }
}
