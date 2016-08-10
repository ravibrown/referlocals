var jobID = 0;
var shareUrl = "";
function ShowShareModal(ID, title) {
    jobID = ID;
    shareUrl = "https://www.referlocals.com/jobdetail/" + ID + "/" + title;
    $("#divShareThisJobModal").modal("show");
}

function SendTextForJobLink() {
    var phonenumber;
    var countryCode;
    bootbox.alert("Text Sent Successfully");
    phonenumber = $("#txtPhone").val();
    countryCode = $("#drpCountryCode").val();

    if (ValidateUSPhoneNumber(phonenumber)) {

        $.ajax({
            url: '/JobDetail.aspx/SendTextJobLink',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{ "countryCode":"' + countryCode + '","phoneNumber":"' + phonenumber + '","url":"' + shareUrl + '"}',
            success: function (data) {

            }
        });
    }
    else {
        bootbox.alert("Please enter valid phone number");
    }
}
function SendEmailForJobLink() {
    bootbox.alert("Email Sent Successfully");
    var email;
    
        email = $("#txtEmail").val().trim();
    
    
    if (IsEmail(email)) {

        $.ajax({
            url: '/JobDetail.aspx/SendEmailJobLink',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{ "email":"' + email + '","url":"' + shareUrl + '"}',
            success: function (data) {

            }
        });
    }
    else {
        bootbox.alert("Pleae enter valid email");
    }
}
function SendTextForProfileLink() {
    bootbox.alert("Text Sent Successfully");
    var phonenumber = $("#txtPhone").val();
    var countryCode = $("#drpCountryCode").val();
    var url = window.location.href;
    if (ValidateUSPhoneNumber(phonenumber)) {

        $.ajax({
            url: '/referDetail.aspx/SendTextProfileLink',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{ "countryCode":"' + countryCode + '","phoneNumber":"' + phonenumber + '","url":"' + url + '"}',
            success: function (data) {

            }
        });
    }
    else {
        bootbox.alert("Please enter valid phone number");
    }
}
function SendEmailForProfileLink() {
    bootbox.alert("Email Sent Successfully");
    var email = $("#txtEmail").val().trim();
    var url = window.location.href;
    if (IsEmail(email)) {

        $.ajax({
            url: '/JobDetail.aspx/SendEmailProfileLink',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{ "email":"' + email + '","url":"' + url + '"}',
            success: function (data) {

            }
        });
    }
    else {
        bootbox.alert("Pleae enter valid email");
    }
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function ValidateUSPhoneNumber(phoneNumber) {
    var regExp = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    var phone = phoneNumber.match(regExp);
    if (phone) {
        return true;
    }
    return false;
}
function ShowDivTextBox() {
   
    $("#txtEmail").val("");
    $("#txtPhone").val("");

        $("#divEmailBox").hide();
        $("#divTextBox").show();
    
}
function ShowDivEmailBox(ID) {
    
    $("#divEmailBox").show();
    $("#txtEmail").val("");
    $("#txtPhone").val("");
        $("#divTextBox").hide();
        
    
}



function ShareThisJob() {
    
    FB.ui({
        method: 'share',
        display: 'popup',
        href: shareUrl,
    }, function (response) { });
}
function ShareThisProfile() {
    FB.ui({
        method: 'share',
        display: 'popup',
        href: window.location.href,
    }, function (response) { });
}
function ShowLoginAlert(message) {
    bootbox.alert(message);
}
$(document).ready(function () {
    $.ajaxSetup({ cache: true });
    $.getScript('//connect.facebook.net/en_US/sdk.js', function () {
        FB.init({
            appId: '257490291284449',
            version: 'v2.5' // or v2.0, v2.1, v2.2, v2.3
        });

    });
    
});
!function (d, s, id)
{
    var js, fjs = d.getElementsByTagName(s)[0],
      p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); }
}(document, 'script', 'twitter-wjs');
