using System;
using CAHM.ViewModels;
using Raven.Client;

namespace CAHM.Raven
{
    public class RegisterAccounts : IRegisterAccounts
    {
        private readonly IDocumentSession _session;

        public RegisterAccounts(IDocumentSession session)
        {
            _session = session;
        }

        public string Register(string email, string password, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
