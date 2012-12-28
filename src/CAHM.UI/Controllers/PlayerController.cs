using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    public class PlayerController : Controller
    {

        [HttpGet]
        public ViewResult Setup()
        {
            return View(new Player());
        }

    }
}