namespace CAHM
{
    public interface ISendAccountResetEmails
    {
        void SendResetEmail(string email, string requestHash);
    }
}