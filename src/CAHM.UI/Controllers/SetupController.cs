using System;
using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public partial class SetupController : Controller
    {

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult NewPlayer(string id)
        {
            return View(new NewPlayerModel());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult NewPlayer(NewPlayerModel model)
        {
            throw new NotImplementedException();
        }

    }
}