using System;

namespace CAHM
{
    public interface ISendAccountResetEmails
    {
        void SendResetEmail(string email, string passwordResetUrl);
    }
}