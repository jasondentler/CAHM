using System.Collections.Generic;
using System.Linq;
using CAHM.Models;
using CAHM.Raven.IndexDefinitions;
using CAHM.ViewModels;
using Raven.Abstractions.Indexing;
using Raven.Client;

namespace CAHM.Raven
{
    public class NewGameService : INewGameService
    {
        private readonly IDocumentSession _session;
        private readonly IGravatarService _gravatarService;

        public NewGameService(IDocumentSession session, IGravatarService gravatarService)
        {
            _session = session;
            _gravatarService = gravatarService;
        }

        public Page<NewGameModel> FindNearby(Location location, int pageNumber)
        {
            const int pageSize = 10;

            if (location == null ||
                location.Latitude == null || location.Latitude == 0 ||
                location.Longitude == null || location.Longitude == 0)
                return new Page<NewGameModel>(new NewGameModel[0], pageSize, pageNumber, 0);
            
            var skip = (pageNumber - 1)*pageSize;
            RavenQueryStatistics stats;

            var point = string.Format("POINT({0} {1})", location.Latitude, location.Longitude);

            var games = _session.Query<Game, NewGameNearby>()
                                .Customize(x => x.RelatesToShape("Location", point, SpatialRelation.Nearby))
                                .Statistics(out stats)
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList()
                                .Select(Convert);

            return new Page<NewGameModel>(games, pageSize, pageNumber, stats.TotalResults);

        }

        public Page<NewGameModel> Search(string search, int pageNumber)
        {
            const int pageSize = 10;
            var skip = (pageNumber - 1)*pageSize;
            RavenQueryStatistics stats;
            var games = _session.Advanced.LuceneQuery<Game>(NewGameSearch.TheIndexName)
                                .Statistics(out stats)
                                .Search("Search", search)
                                .Skip(skip)
                                .Take(pageSize)
                                .ToList()
                                .Select(Convert);

            return new Page<NewGameModel>(games, pageSize, pageNumber, stats.TotalResults);
        }

        public IEnumerable<NewGameModel> JoinGame(string gameId, string email)
        {
            var account = _session.Query<Account>().SingleOrDefault(a => a.Email == email);

            var unjoinedGames = _session.Query<Game>()
                                        .Where(g => g.Players.Any(a => a.Email == email))
                                        .ToList();

            var joinedGame = unjoinedGames.SingleOrDefault(g => g.Id == gameId) ?? _session.Load<Game>(gameId);

            unjoinedGames.ForEach(g => g.Players.RemoveAll(a => a.Email == email));

            joinedGame.Players.Add(account);

            _session.SaveChanges();

            return unjoinedGames.Concat(new[] {joinedGame}).Distinct().Select(Convert).ToList();
        }

        public NewGameModel Get(string id)
        {
            return Convert(_session.Load<Game>(id));
        }

        public Game Create(string email)
        {
            var account = _session.Query<Account>().SingleOrDefault(a => a.Email == email);
            var game = new Game();
            game.Players.Add(account);
            _session.Store(game);
            return game;
        }

        private NewGameModel Convert(Game model)
        {
            var viewModel = new NewGameModel
                {
                    Id = model.Id,
                    GameStarted = model.GameStarted
                };

            model.Players
                 .Where(account => account != null)
                 .Select(account => account.Email)
                 .Where(email => !string.IsNullOrWhiteSpace(email))
                 .Select(email => _gravatarService.GetGravatarHash(email))
                 .ToList()
                 .ForEach(hash => viewModel.GravatarHashes.Add(hash));

            return viewModel;
        }

    }
}
