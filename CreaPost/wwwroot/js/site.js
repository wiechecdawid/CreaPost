$(document).ready(function () {
    var Navy = $('.navbar').offset().top;

    var stickynav = function () {
        var Scrolly = $(window).scrollTop();
        if (Scrolly > Navy) {
            $('.navbar').addClass('.sticky');
        }
        else {
            $('.navbar').removeClass('.sticky');
        }

        stickynav();
        $(window).scroll(function () {
            stickynav();
        });
    }
});