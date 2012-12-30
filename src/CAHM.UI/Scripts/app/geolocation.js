///<reference path="~/Scripts/lib/" />
$(function () {

    function success(position) {
        var location = {
            latitude: position.coords.latitude,
            longitude: position.coords.longitude
        };

        document.latitude = location.latitude;
        document.longitude = location.longitude;
        $(document).trigger('location-set', location);
    }

    navigator.geolocation.watchPosition(success, $.noop, { enableHighAccuracy: true, maximumAge: 30000, timeout: 27000 });

});