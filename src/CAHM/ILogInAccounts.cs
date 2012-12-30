using CAHM.ViewModels;

namespace CAHM
{
    public interface ILogInAccounts
    {
        bool Login(string email, string password, Location location);        
    }
}