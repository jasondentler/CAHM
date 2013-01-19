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

            pageOfGames.Items
                       .Select(g => g.Id)
                       .ToList()
                       .ForEach(gameId => Groups.Add(Context.ConnectionId, gameId));

            return pageOfGames;
        }

        public void Join(string id)
        {
            var changedGames = _newGameService.JoinGame(id, Context.User.Identity.Name);

            changedGames
                .ToList()
                .ForEach(g => Clients.Group(g.Id).Update(g));
        }

    }
}