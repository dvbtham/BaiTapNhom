$(document).ready(function () {
    searchScroll();
});

function searchScroll() {
    $('#txtSearch').bind('keydown', function (event) {
        if (event.which == 13) {
            $('html, body').animate({
                scrollTop: $("#searchResult").offset().top
            }, 400);
        }

    });
}

