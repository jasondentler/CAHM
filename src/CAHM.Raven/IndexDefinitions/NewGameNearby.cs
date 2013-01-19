using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAHM.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace CAHM.Raven.IndexDefinitions
{
    public class NewGameNearby : AbstractIndexCreationTask<Game, NewGameSearch.Result>
    {

        public const string TheIndexName = "Games/Nearby";

        public override string IndexName
        {
            get { return TheIndexName; }
        }

        public class Result
        {
            public string Id { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public IEnumerable<string> Players { get; set; }
            public object _ { get; set; }
        }

        public NewGameNearby()
        {

            Map = games => from game in games
                           where !game.GameStarted
                           from playerId in game.Players.Select(a => a.Id)
                           let player = LoadDocument<Account>(playerId)
                           where player.Location != null &&
                                 player.Location.Latitude != null &&
                                 player.Location.Longitude != null &&
                                 player.Location.Latitude != 0 &&
                                 player.Location.Longitude != 0
                           select new Result
                               {
                                   Id = game.Id,
                                   Latitude = player.Location.Latitude ?? 0,
                                   Longitude = player.Location.Longitude ?? 0,
                                   Players = game.Players.Select(a => a.Email),
                                   _ = SpatialGenerate("Location", 
                                                       player.Location.Latitude ?? 0,
                                                       player.Location.Longitude ?? 0)
                               };

        }


    }
}
