///<reference path="~/Scripts/lib/" />
$(function () {

    $('#playerSetup #email')
        .change(updateGravatar)
        .keyup(updateGravatar);

    updateGravatar();

    function gravatarUrl() {
        var emailField = $('#playerSetup #email');
        if (emailField.length == 0)
            return null;
        
        var grav = "";
        var email = emailField.val().toLowerCase().trim();
        var hash = CryptoJS.MD5(email);
        grav = "http://www.gravatar.com/avatar/" + hash + ".jpg?r=pg&s=28&d=retro";
        return grav;
    }

    function updateGravatar() {
        var gravatar = $('#playerSetup .gravatar');
        if (gravatar.length == 0)
            return;
        gravatar.attr('src', gravatarUrl());
    }

});