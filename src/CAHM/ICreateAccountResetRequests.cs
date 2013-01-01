using System;
using CAHM.Models;

namespace CAHM
{

    public delegate string GeneratePasswordResetUrl(string email, string requestId, string requestHash);

    public interface ICreateAccountResetRequests
    {
        void CreateAccountResetRequest(string email, GeneratePasswordResetUrl passwordResetUrl);
        bool ValidateAccountResetRequest(string email, string requestId, string requestHash);
        PasswordResetRequest GetAccountResetRequest(string email, string requestId, string requestHash);
    }
}
