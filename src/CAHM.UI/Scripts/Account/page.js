///<reference path="~/Scripts/lib/" />
$(function () {

    $('#playerSetup #email')
        .change(updateGravatar)
        .keyup(updateGravatar);

    updateGravatar();

    function gravatarUrl() {
        var grav = "";
        var email = $('#playerSetup #email').val().toLowerCase().trim();
        var hash = CryptoJS.MD5(email);
        grav = "http://www.gravatar.com/avatar/" + hash + ".jpg?r=pg&s=28&d=mm";
        return grav;
    }

    function updateGravatar() {
        $('#playerSetup .gravatar').attr('src', gravatarUrl());
    }

});