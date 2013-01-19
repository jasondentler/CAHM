///<reference path="~/Scripts/lib/knockout-2.1.0.js" />
///<reference path="~/Scripts/app/"/>
///<reference path="~/Signalr"/>
(function () {

    var viewModel = (function () {
        var self = {};
        self.PendingGames = ko.observableArray();
        self.SelectedGame = ko.observable().extend({ required: true });
        return self;
    })();

    ko.applyBindings(viewModel);

    var geolocationPromise = $.Deferred();

    $(document).bind('location-set', function (e, position) {
        geolocationPromise.resolve(position);
    });

    function updateGame(gameModel) {
        var matches = $.map(viewModel.PendingGames(), function (item) {
            return item.Id() == gameModel.Id ? item : null;
        });

        $.each(matches, function () {
            var match = this;
            match.GameStarted(gameModel.GameStarted);
            var existingHashes = match.GravatarHashes();

            var toRemove = $.map(existingHashes, function (item) {
                return $.inArray(item, gameModel.GravatarHashes) == -1 ? item : null;
            });

            var toAdd = $.map(gameModel.GravatarHashes, function (item) {
                return $.inArray(item, existingHashes) == -1 ? item : null;
            });

            $.each(toRemove, function () {
                var idx = $.inArray(this, match.GravatarHashes());
                match.GravatarHashes.splice(idx, 1);
            });

            $.each(toAdd, function () {
                match.GravatarHashes.push(this);
            });

            if (match.GravatarHashes().length == 0) {
                // No players? Remove it.
                var idx = $.inArray(match, viewModel.PendingGames());
                viewModel.PendingGames.splice(idx, 1);
            }
        });

        if (matches.length == 0) {
            //New game
            viewModel.PendingGames.push({
                Id: ko.observable(gameModel.Id),
                GravatarHashes: ko.observableArray(gameModel.GravatarHashes),
                GameStarted: ko.observable(gameModel.GameStarted)
            });
        }
    }

    $(function () {
        var hub = $.connection.newGame;

        hub.client.update = updateGame;

        hub.client.updateBatch = function (games) {
            $.each(games, function () {
                updateGame(this);
            });
        };

        viewModel.SelectedGame.subscribe(function (gameId) {
            hub.server.join(gameId);
        });

        $('#newGame').click(function () {
            hub.server.create();
        });

        var startPromise = $.connection.hub.start().fail(function () {
            alert('Error connecting to the game server.');
        });

        $.when(geolocationPromise, startPromise).done(function (position) {
            function loadNearbyGames(pageNumber) {
                hub.server.findNearbyGames(position, pageNumber).done(function (data) {
                    if (data.MorePages)
                        loadNearbyGames(pageNumber + 1);
                    parseResults(data);
                });
            }

            function parseResults(data) {
                console.log(data);
                $.each(data.Items, function () {
                    updateGame(this);
                });
            }

            loadNearbyGames(1);
        });


    });

})();

