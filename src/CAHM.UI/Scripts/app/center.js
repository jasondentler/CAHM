///<reference path="~/Scripts/lib/" />
$(function () {

    function center(element) {
        $(element).css({ position: 'absolute' });

        var windowWidth = $(window).width();
        var windowHeight = $(window).height();

        var width = $(element).outerWidth(true);
        var height = $(element).outerHeight(true);

        var css = {
            left: windowWidth / 2 - width / 2,
            top: windowHeight / 2 - height / 2
        };

        if (css.top < 0)
            css.top = 0;
        if (css.left < 0)
            css.left = 0;

        $(element).css(css);
    }

    $(window).resize(function () {
        center('#content');
        center('h2.ghost');
    });

    center('#content');
    center('h2.ghost');

});