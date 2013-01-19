using System.Linq;
using CAHM.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace CAHM.Raven.IndexDefinitions
{
    public class NewGameSearch : AbstractIndexCreationTask<Game, NewGameSearch.Result>
    {

        public const string TheIndexName = "Games/Search";

        public override string IndexName
        {
            get { return TheIndexName; }
        }

        public class Result
        {
            public string[] Search { get; set; }
        }

        public NewGameSearch()
        {

            Map = games => from game in games
                           where !game.GameStarted
                           select new {search = game.Players.Select(p => p.Email)};

            Index(r => r.Search, FieldIndexing.Analyzed);

        }


    }
}
