using System.Web.Mvc;
using CAHM.ViewModels;

namespace CAHM.UI.Controllers
{
    [Authorize]
    public partial class NewGameController : Controller
    {
        private readonly INewGameService _newGameService;
        private readonly ILocationService _locationService;
        private readonly ICurrentUserService _currentUserService;

        public NewGameController(
            INewGameService newGameService, 
            ILocationService locationService,
            ICurrentUserService currentUserService)
        {
            _newGameService = newGameService;
            _locationService = locationService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public virtual ViewResult List()
        {
            return View(new NewGameModel());
        }

        [HttpPost]
        public virtual RedirectToRouteResult Create()
        {
            var email = _currentUserService.Email;
            var game = _newGameService.Create(email);
            return RedirectToAction(MVC.NewGame.List());
        }

    }
}