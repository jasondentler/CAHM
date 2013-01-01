using System.Collections.Generic;
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

        public IEnumerable<object> FindNearbyGames(Location location)
        {
            yield return new {Id = "asdf1", Gravatars = new[] {"1234567890"}};
            yield return new {Id = "asdf2", Gravatars = new[] {"1234567890", "1234567890"}};
            yield return new {Id = "asdf3", Gravatars = new[] {"1234567890", "1234567890", "1234567890"}};
            yield return new {Id = "asdf4", Gravatars = new[] {"1234567890", "1234567890", "1234567890", "1234567890"}};
        }

    }
}