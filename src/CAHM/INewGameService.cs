using System;
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
        Tuple<string, IEnumerable<NewGameModel>> Create(string email);

    }
}
