using CAHM.ViewModels;

namespace CAHM
{
    public interface IRegisterAccounts
    {
        void Register(string email, string password, Location location);
    }
}
