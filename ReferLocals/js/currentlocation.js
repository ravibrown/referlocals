
       var geocoder;
var isUseCurrentPositionclicked = 0;
function SetCurrentPosition()
{
    isUseCurrentPositionclicked = 1;
    GetCurrentPosition();
}
function GetCurrentPosition() {
    if (navigator.geolocation) {
                
        navigator.geolocation.getCurrentPosition(successFunction, errorFunction);
    }
}
//Get the latitude and the longitude;
function successFunction(position) {
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    codeLatLng(lat, lng)
    if (isUseCurrentPositionclicked == 1) {
        bootbox.alert("Location set to your current location");
        $("#responsive").modal("hide");
    }
}

function errorFunction() {
    //alert("Geocoder failed");
    if (isUseCurrentPositionclicked == 1) {
        bootbox.alert("Please allow to use your current location from your browser settings. Otherwise our default location will be set");
    }
    SetCurrentLocation("75034", "Frisco TX");
}

function initialize() {
    geocoder = new google.maps.Geocoder();
    GetCurrentPosition();
}

function codeLatLng(lat, lng) {

    var latlng = new google.maps.LatLng(lat, lng);
    //var latlng = new google.maps.LatLng("33.1505556", "-96.8233333");
             
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            console.log(results)
            if (results[1]) {
                //formatted address
                //alert(results[0].formatted_address)
                //find country name
                for (var i = 0; i < results[0].address_components.length; i++) {
                    for (var b = 0; b < results[0].address_components[i].types.length; b++) {

                        //there are different types that might hold a city admin_area_lvl_1 usually does in come cases looking for sublocality type will be more appropriate
                        if (results[0].address_components[i].types[b] == "administrative_area_level_1") {
                            //this is the object you are looking for
                            state = results[0].address_components[i];
                            break;
                        }
                        if (results[0].address_components[i].types[b] == "locality") {
                            //this is the object you are looking for
                            city = results[0].address_components[i];
                            break;
                        }
                        if (results[0].address_components[i].types[b] == "postal_code") {
                            //this is the object you are looking for
                                    
                            postal_code= results[0].address_components[i].long_name;
                            break;
                        }
                    }
                }
                //city data
                //alert(city.short_name+", " + state.short_name +", "+ postal_code);
                SetCurrentLocation(postal_code,city.short_name +" " + state.short_name);

            } else {
                //   alert("No results found");
                SetCurrentLocation("75034", "Frisco TX");
            }
        } else {
            //alert("Geocoder failed due to: " + status);
            SetCurrentLocation("75034", "Frisco TX");
        }
    });
}
function SetCurrentLocation(postalCode, locationName) {
    $.ajax({
        url: '/Profile.aspx/SetCurrentLocation',
        type: "POST",
        data: '{ "postalCode":"' + postalCode + '","locationName":"' + locationName + '","isUseCurrentPositionclicked":"' + isUseCurrentPositionclicked + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != "") {
                $("#lblLocation").text(result.d);
                //$(".btnLocationClose").trigger('click');



            }
        }
    });

}
$(function () {
    initialize();
});
