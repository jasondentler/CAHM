using CAHM.ViewModels;

namespace CAHM
{
    public interface IRegisterAccounts
    {
        string Register(string email, string password, Location location);
    }
}
