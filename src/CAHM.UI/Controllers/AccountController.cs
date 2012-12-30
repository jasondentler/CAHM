using System.Web.Mvc;
using System.Web.Security;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IRegisterAccounts _registerAccounts;
        private readonly ILogInAccounts _logInAccounts;
        private readonly ICreateAccountResetRequests _createAccountResetRequests;
        private readonly IChangeAccountPasswords _changeAccountPasswords;

        public AccountController(
            IRegisterAccounts registerAccounts, 
            ILogInAccounts logInAccounts, 
            ICreateAccountResetRequests createAccountResetRequests,
            IChangeAccountPasswords changeAccountPasswords)
        {
            _registerAccounts = registerAccounts;
            _logInAccounts = logInAccounts;
            _createAccountResetRequests = createAccountResetRequests;
            _changeAccountPasswords = changeAccountPasswords;
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Account.Actions.Register());

            var errorMessage = _registerAccounts.Register(model.Email, model.Password, model.Location);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(" ", errorMessage);
                return RedirectToAction(MVC.Account.Register());
            }

            FormsAuthentication.RedirectFromLoginPage(model.Email, true);
            return null;
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Account.Login());
            var isValid = _logInAccounts.Login(model.Email, model.Password, model.Location);
            if (!isValid)
            {
                ModelState.AddModelError(" ", "The email / password combination you entered is incorrect.");
                return RedirectToAction(MVC.Account.Login());
            }

            FormsAuthentication.RedirectFromLoginPage(model.Email, true);
            return null;
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult ForgotPassword()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Account.ForgotPassword());

            _createAccountResetRequests.CreateAccountResetRequest(model.Email);
            return RedirectToAction(MVC.Account.ForgotPasswordConfirmation());
        }

        [HttpGet]
        public virtual ViewResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult ResetPassword(string email, string requestHash)
        {
            return View(new ResetPasswordModel
                {
                    Email = email,
                    RequestHash = requestHash
                });
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Account.ResetPassword(model.Email, model.RequestHash));

            _changeAccountPasswords.ChangePassword(model.Email, model.RequestHash, model.Password);

            return RedirectToAction(MVC.Account.Login());
        }

    }
}