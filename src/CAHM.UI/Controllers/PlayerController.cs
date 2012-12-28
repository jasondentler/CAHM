using System.Web.Mvc;

namespace CAHM.UI.Controllers
{
    public class PlayerController : Controller
    {

        [HttpGet]
        public ViewResult Setup()
        {
            return View();
        }

    }
}