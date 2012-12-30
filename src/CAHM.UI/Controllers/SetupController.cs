using System;
using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public partial class SetupController : Controller
    {

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult NewPlayer()
        {
            return View(new NewPlayerModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult NewPlayer(NewPlayerModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(MVC.Setup.Actions.NewPlayer());
            throw new NotImplementedException();
        }

    }
}