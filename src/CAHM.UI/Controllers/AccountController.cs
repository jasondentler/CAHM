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
            return View(new NewPlayerModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult Register(NewPlayerModel model)
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

    }
}