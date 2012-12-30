using System;
using Raven.Client;

namespace CAHM.Raven
{
    public class CreateAccountResetRequests : ICreateAccountResetRequests
    {
        private readonly ISendAccountResetEmails _sendAccountResetEmails;
        private readonly IDocumentSession _session;

        public CreateAccountResetRequests(ISendAccountResetEmails sendAccountResetEmails, IDocumentSession session)
        {
            _sendAccountResetEmails = sendAccountResetEmails;
            _session = session;
        }

        public void CreateAccountResetRequest(string email)
        {
            throw new NotImplementedException();
        }
    }
}
