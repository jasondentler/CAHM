using System;
using CAHM.ViewModels;
using Raven.Client;

namespace CAHM.Raven
{
    public class LogInAccounts : ILogInAccounts
    {
        private readonly IDocumentSession _session;

        public LogInAccounts(IDocumentSession session)
        {
            _session = session;
        }

        public bool Login(string email, string password, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
