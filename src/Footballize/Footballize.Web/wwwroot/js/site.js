// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMap(latitude, longitude) {
    var location =
    {

        lat: latitude,
        lng: longitude
    };

    var map = new google.maps.Map(
        document.getElementById('map'),
        {
            zoom: 14,
            center: location
        });

    var marker = new google.maps.Marker(
        {
            position: location,
            map: map
        });
}


function initDatetimepicker() {
    $('.datetimepicker').datetimepicker({
        minDate: new Date(),
            
        format: 'DD-MM-YYYY HH:mm',
        showTodayButton: true,
        sideBySide : true,
           
        showClose: true,
        showClear: true,
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-remove'
        }
    });
}


function startChat(gatherId) {
    var entityMap = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;',
        '/': '&#x2F;',
        '`': '&#x60;',
        '=': '&#x3D;'
    };

    function escapeHtml (string) {
        return String(string).replace(/[&<>"'`=\/]/g, function (s) {
            return entityMap[s];
        });
    }

    var connection =
        new signalR.HubConnectionBuilder()
            .withUrl("/chat")
            .build();

    connection.on("NewMessage",
        function (message) {
            var chatInfo = `<div><strong>${escapeHtml(message.user)}</strong>: ${escapeHtml(message.text)}</div>`;
            $("#messagesList").append(chatInfo);
        });


    connection.on("LoadChatHistory", function() {
        connection.invoke("GetHistory", gatherId);
    });

    connection.on("UserJoin",
        function (message) {
            var chatInfo = `<div class="font-italic">${escapeHtml(message.user)} is connected to chat room.</div>`;
            $("#messagesList").append(chatInfo);
        });

    connection.on("UserLeave",
        function (message) {
            var chatInfo = `<div class="font-italic">${escapeHtml(message.user)} has left the chat room.</div>`;
            $("#messagesList").append(chatInfo);
        });

    $("#sendButton").click(function() {
        var message = $("#messageInput").val();
        connection.invoke("Send", message, gatherId);
        $("#messageInput").val("");
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });
}