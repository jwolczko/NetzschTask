
$(document).ready(function () {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("/message")
        .build();
    connection.start();

    connection.on("RecievedMessage", function (data) {
        $("#recievedMessage").val(data);
    });    

    $("#sendMessageBtn").click(function () {
        var message = $("#messageToSend").val();
        connection.invoke("SendMessage", message);
    });
});