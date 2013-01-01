using System.Web.Mvc;

namespace CAHM.UI.Controllers
{
    [Authorize]
    public partial class SetupController : Controller
    {

        public virtual ActionResult ListGames()
        {
            return Content("Games list here");
        }

    }
}