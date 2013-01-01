using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    [Authorize]
    public partial class NewGameController : Controller
    {

        [HttpGet]
        public virtual ViewResult List()
        {
            return View(new NewGameModel());
        }

        public virtual ActionResult Create()
        {
            return Content("Create a new game here");
        }

    }
}