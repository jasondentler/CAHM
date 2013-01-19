using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
        private readonly ILocationService _locationService;

        public AccountController(
            IRegisterAccounts registerAccounts, 
            ILogInAccounts logInAccounts, 
            ICreateAccountResetRequests createAccountResetRequests,
            IChangeAccountPasswords changeAccountPasswords,
            ILocationService locationService)
        {
            _registerAccounts = registerAccounts;
            _logInAccounts = logInAccounts;
            _createAccountResetRequests = createAccountResetRequests;
            _changeAccountPasswords = changeAccountPasswords;
            _locationService = locationService;
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

            _locationService.UpdateLocation(model.Location);

            try
            {
                _registerAccounts.Register(model.Email, model.Password, model.Location);
            }
            catch (AccountAlreadyExistsException ex)
            {
                ModelState.AddModelError(" ", ex.Message);
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

            _locationService.UpdateLocation(model.Location);

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

            GeneratePasswordResetUrl urlFunc = (email, id, hash) =>
                {
                    var values = new RouteValueDictionary
                        {
                            {"email", email},
                            {"requestId", id},
                            {"requestHash", hash}
                        };

                    var url = Url.Action("ResetPassword", values) ?? "";
                    var appRoot = HttpRuntime.AppDomainAppVirtualPath ?? "";

                    if ((appRoot).EndsWith("/") && url.StartsWith("/"))
                        appRoot = appRoot.Substring(0, appRoot.Length - 1);

                    url = appRoot + url;

                    var authority = Request.Url.GetLeftPart(UriPartial.Authority).ToString();
                    if ((authority).EndsWith("/") && url.StartsWith("/"))
                        authority = authority.Substring(0, authority.Length - 1);

                    url = authority + url;

                    return url;
                };

            try
            {
                _createAccountResetRequests.CreateAccountResetRequest(model.Email, urlFunc);
            }
            catch (ResetRequestAlreadyExistsException ex)
            {
                ModelState.AddModelError(" ", ex.Message);
                return RedirectToAction(MVC.Account.ForgotPassword());
            }

            return RedirectToAction(MVC.Account.ForgotPasswordConfirmation());
        }


        [HttpGet]
        public virtual ViewResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult ResetPassword(string email, string requestId, string requestHash)
        {
            var isValid = _createAccountResetRequests.ValidateAccountResetRequest(email, requestId, requestHash);

            if (!isValid)
                return View("InvalidResetRequest");

            return View(new ResetPasswordModel
                {
                    Email = email,
                    RequestId = requestId,
                    RequestHash = requestHash
                });
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Account.ResetPassword(model.Email, model.RequestId, model.RequestHash));

            try
            {
                _changeAccountPasswords.ChangePassword(model.Email, model.RequestId, model.RequestHash, model.Password);
            }
            catch (InvalidResetRequestException)
            {
                ModelState.AddModelError(" ", "Sorry. This password reset request is no longer valid.");
                return RedirectToAction(MVC.Account.ResetPassword(model.Email, model.RequestId, model.RequestHash));
            }

            return RedirectToAction(MVC.Account.Login());
        }

    }
}