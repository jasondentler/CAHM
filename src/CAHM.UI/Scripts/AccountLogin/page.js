///<reference path="~/Scripts/app/" />
$(document).bind('location-set', function (e, location) {
    $('#latitude').val(location.latitude);
    $('#longitude').val(location.longitude);
});
