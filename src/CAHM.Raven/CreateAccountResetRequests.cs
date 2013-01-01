using System;
using System.Linq;
using CAHM.Models;
using Raven.Abstractions.Exceptions;
using Raven.Client;

namespace CAHM.Raven
{
    public class CreateAccountResetRequests : ICreateAccountResetRequests
    {

        private readonly ISendAccountResetEmails _sendAccountResetEmails;
        private readonly IDocumentSession _session;
        private readonly IHasher _hasher;

        public CreateAccountResetRequests(
            ISendAccountResetEmails sendAccountResetEmails, 
            IDocumentSession session,
            IHasher hasher
            )
        {
            _sendAccountResetEmails = sendAccountResetEmails;
            _session = session;
            _hasher = hasher;
        }

        public void CreateAccountResetRequest(string email, GeneratePasswordResetUrl passwordResetUrl)
        {
            var hash = ComputeHash(email);

            var requestDocument = new PasswordResetRequest()
                {
                    Email = email,
                    Hash = hash
                };
            try
            {
                _session.StoreUniqueWithExpiration(requestDocument, doc => doc.Email, DateTime.UtcNow.AddHours(1));
            }
            catch (ConcurrencyException ex)
            {
                throw new ResetRequestAlreadyExistsException();
            }

            var url = passwordResetUrl(email, requestDocument.Id, hash);

            _sendAccountResetEmails.SendResetEmail(email, url);
        }

        public bool ValidateAccountResetRequest(string email, string requestId, string requestHash)
        {
            return _session.Query<PasswordResetRequest>()
                           .Any(doc => doc.Id == requestId && doc.Email == email && doc.Hash == requestHash && !doc.Used);
        }

        public PasswordResetRequest GetAccountResetRequest(string email, string requestId, string requestHash)
        {
            var doc = _session.Load<PasswordResetRequest>(requestId);
            if (doc == null)
                return null;
            if (doc.Email != email || doc.Hash != requestHash)
                return null;
            return doc;
        }

        private string ComputeHash(string email)
        {
            return _hasher.Hash(DateTime.UtcNow.ToLongDateString(), email);
        }

    }
}
