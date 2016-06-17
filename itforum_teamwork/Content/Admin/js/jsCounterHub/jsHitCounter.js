$(function () {
    var counterHub = $.connection.counterHub;
    $.connection.hub.start().done(function () {

    });
    counterHub.client.UpdateCount = function (count) {
        $('#counter').text(count);
    }
});