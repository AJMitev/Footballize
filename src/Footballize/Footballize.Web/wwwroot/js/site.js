// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMap(latitude, longitude) {
    debugger;
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