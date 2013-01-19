using System.Linq;
using CAHM.ViewModels;
using Microsoft.AspNet.SignalR.Hubs;

namespace CAHM.UI.Hubs
{
    public class NewGame : Hub
    {

        private readonly INewGameService _newGameService;

        public NewGame(INewGameService newGameService)
        {
            _newGameService = newGameService;
        }

        public Page<NewGameModel> FindNearbyGames(Location location, int pageNumber)
        {
            var pageOfGames = _newGameService.FindNearby(location, pageNumber);
            return pageOfGames;
        }

        public void Join(string id)
        {
            var changedGames = _newGameService.JoinGame(id, Context.User.Identity.Name);
            Clients.All.UpdateBatch(changedGames);
        }

        public void Create()
        {
            var email = Context.User.Identity.Name;

            var result = _newGameService.Create(email);
            //var newGameId = result.Item1;
            var changedGames = result.Item2.ToList();
            Clients.All.UpdateBatch(changedGames);
        }

    }
}