using CAHM.Models;

namespace CAHM
{
    public interface IChangeAccountPasswords
    {
        void ChangePassword(string email, string requestId, string requestHash, string password);
        void SetPassword(string email, string password);
        void SetPassword(Account account, string password);
    }
}