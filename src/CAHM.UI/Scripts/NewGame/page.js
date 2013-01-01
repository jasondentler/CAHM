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

    $(document).bind('location-set', function (position) {
        geolocationPromise.resolve(position);
    });

    $(function () {
        var hub = $.connection.newGame;

        var startPromise = $.connection.hub.start().fail(function () {
            alert('Error connecting to the game server.');
        });

        $.when(geolocationPromise, startPromise).done(function (position) {
            hub.server.findNearbyGames(position).done(function(data) {
                $.each(data, function() {
                    console.log(this);
                    viewModel.PendingGames.push({
                        Id: ko.observable(this.Id),
                        Gravatars: ko.observableArray(this.Gravatars),
                        Name: ko.observable('')
                    });
                });
            });
        });
    });

})();

