using System.Collections.Generic;
using CAHM.Models;
using CAHM.ViewModels;

namespace CAHM
{
    public interface INewGameService
    {

        Page<NewGameModel> FindNearby(Location location, int pageNumber);
        Page<NewGameModel> Search(string search, int pageNumber);
        IEnumerable<NewGameModel> JoinGame(string gameId, string email);
        NewGameModel Get(string id);
        Game Create(string email);

    }
}
