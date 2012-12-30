using System;
using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public partial class AccountController : Controller
    {

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
            throw new NotImplementedException();
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
                return RedirectToAction(MVC.Account.Actions.Login());
            throw new NotImplementedException();
        }

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult ForgotPassword()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult ForgotPassword(ForgotPasswordModel model)
        {
            throw new NotImplementedException();
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
            return View(new ResetPasswordModel()
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

            throw new NotImplementedException();

            return RedirectToAction(MVC.Account.Login());
        }

    }
}