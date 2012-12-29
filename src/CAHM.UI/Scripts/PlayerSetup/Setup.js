///<reference path="~/Scripts/lib/" />
///<reference path="~/signalr" />
$(function () {

    function gravatarUrl() {
        var grav = "";
        var email = $('#player-email input').val().toLowerCase().trim();
        var hash = CryptoJS.MD5(email);
        grav = "http://www.gravatar.com/avatar/" + hash + ".jpg?r=pg&s=50&d=mm";
        return grav;
    }

    function updateGravatar() {
        $('#player-email img').attr('src', gravatarUrl());
    }

    $('#player-email input')
        .change(updateGravatar)
        .keyup(updateGravatar);
    
});