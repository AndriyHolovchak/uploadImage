$(function() {

    var apphub = $.connection.appHub;

    apphub.client.displayMessage = function(message) {


        $("#name").html(message.Name);
        $("#statusCode").html(message.StatusCode);

        $("#img").attr("src", "data:image/;base64," + message.ImageData);

    };

    $.connection.hub.start();
});