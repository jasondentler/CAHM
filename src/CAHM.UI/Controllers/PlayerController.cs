using System;
using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public partial class PlayerController : Controller
    {

        [HttpGet, ModelStateToTempData]
        public virtual ViewResult Setup(string id)
        {
            return View(new Player());
        }

        [HttpPost, ModelStateToTempData]
        public virtual RedirectToRouteResult Setup(Player model)
        {
            throw new NotImplementedException();
        }

    }
}