namespace CAHM
{
    public interface IChangeAccountPasswords
    {
        void ChangePassword(string email, string requestHash, string password);
    }
}