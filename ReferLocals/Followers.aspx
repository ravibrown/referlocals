<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Followers.aspx.cs" Inherits="ReferLocals.Followers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Mobile Specific Meta -->
    <meta charset="utf-8" />
    <!-- Always force latest IE rendering engine or request Chrome Frame -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <!-- Page Title -->
    <title>Referlocals.com</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="requestProfile vc_profile">
        <div class="container">
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="row">
                                <div class="col-md-3">
                                    <ul class="ver-inline-menu tabbable margin-bottom-10">
                                        <li><a href="/favorites"><i class="fa fa-heart"></i>Favorites </a><span class="after"></span></li>
                                        <li class="active"><a href="/followers"><i class="fa fa-star"></i>Followers </a></li>
                                    </ul>
                                </div>
                                <div class="col-md-9">


                                    <div class="tabbable-line tabbable-custom-profile vsprofiletable5 fav_user_page">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_1_11" data-toggle="tab">Professionals </a></li>
                                            <li><a href="#tab_1_23" data-toggle="tab">User </a></li>

                                        </ul>
                                        <div class="tab-content">

                                            <div class="tab-pane active" id="tab_1_11">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Professionals List </h2>
                                                            <div id="divFollowerProfessionals" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreFollowerProfessionals" class="table_reqBtn">
                                                        <a onclick="ShowMoreFollowerProfessionals();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="tab_1_23">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">User List  </h2>
                                                            <div id="divFollowerUsers" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreFollowerUsers" class="table_reqBtn">
                                                        <a onclick="ShowMoreFollowerUsers();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script id="FollowerUserTemplate" type="text/html">
        <div class="desh_board1 user_fav_Page">
            <div class="col-md-2 textDah_board">
                <img src="${UserImage}" alt="">
            </div>
            <div class="col-md-7 textDah_board1 fav_messg_text">
                <h2 class="leakage">${Username}</h2>
                <p>${UserLocation}</p>
            </div>
            <div class="col-md-3 textDah_board3 fav_messg_ancher">
                {{if IsFavoriteByMe}}
                <div class="send_fav_messages"><a class="s_f_messages edit_sf">Added</a> </div>
                {{else}}
                <div class="send_fav_messages"><a id="aFavorite${UserID}" onclick="SetFavoriteUser(${UserID})" class="s_f_messages">Add To Favorites</a> </div>
                {{/if}}
                 
            </div>
        </div>
    </script>
    <script id="FollowerProfessionalsTemplate" type="text/html">
        <div class="desh_board1 user_fav_Page">
            <div class="col-md-2 textDah_board">
                <img src="${UserImage}" alt="">
            </div>
            <div class="col-md-7 textDah_board1 fav_messg_text">
                <h2 class="leakage">${Username}</h2>
                <p>
                    {{each(prop, val) Categories}}
                          ${val.SubCategoryName}
                    {{/each}} 
                </p>
                <p>${UserLocation}</p>
            </div>
            <div class="col-md-3 textDah_board3 fav_messg_ancher">
                {{if IsFavoriteByMe}}
                <div class="send_fav_messages"><a class="s_f_messages edit_sf">Added</a> </div>
                {{else}}
                <div class="send_fav_messages"><a id="aFavorite${UserID}" onclick="SetFavoriteProfessional(${UserID})" class="s_f_messages">Add To Favorites</a> </div>
                {{/if}}
                 
            </div>
        </div>
    </script>

    <script type="text/javascript">
        var pageIndexFollowerUsers = 0; var pageIndexFollowerProfessionals = 0;

        var pageSize = 10;

        $(function () {
            GetFollowerUsers(0);
            GetFollowerProfessionals(0);

        })

        function GetFollowerUsers(pageIndex) {

            $("#divShowMoreFollowerUsers").hide();
            if ($("#divFollowerUsers").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/Followers.aspx/GetMyFollowerUsers',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divFollowerUsers").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreFollowerUsers").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreFollowerUsers").show();
                        }

                        $("#FollowerUserTemplate").tmpl(result.d.Users).appendTo("#divFollowerUsers");

                    }
                    else {
                        $("#divFollowerUsers").html("No records found");
                    }

                }
            });
        }
        function ShowMoreFollowerUsers() {
            pageIndexFollowerUsers++;
            GetFollowerUsers(pageIndexFollowerUsers);
        }
        function ShowMoreFollowerUsers() {
            pageIndexFollowerJobs++;
            GetFollowerJobs(pageIndexFollowerJobs);
        }
        function GetFollowerProfessionals(pageIndex) {

            $("#divShowMoreFollowerProfessionals").hide();
            if ($("#divFollowerProfessionals").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/Followers.aspx/GetMyFollowerProfessionals',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divFollowerProfessionals").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreFollowerProfessionals").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreFollowerProfessionals").show();
                        }

                        $("#FollowerProfessionalsTemplate").tmpl(result.d.Professionals).appendTo("#divFollowerProfessionals");

                    }
                    else {
                        $("#divFollowerProfessionals").html("No records found");
                    }

                }
            });
        }
        function ShowMoreFollowerProfessionals() {
            pageIndexFollowerProfessionals++;
            GetFollowerProfessionals(pageIndexFollowerProfessionals);
        }
        function SetFavoriteUser(userID) {
            var confirmMsg = "Are your sure to add this User to your favorite list?";
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {

                    bootbox.alert("User added to your favorite list");
                    $("#aFavorite" + userID).removeAttr("onclick");
                    $("#aFavorite" + userID).addClass("edit_sf");
                    $("#aFavorite" + userID).html("Added");

                    $.ajax({
                        url: '/UserPublicProfile.aspx/SetFavoriteUser',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "userID":"' + userID + '"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }

        function SetFavoriteProfessional(professionalID) {
            var confirmMsg = "Are your sure to add this professional to your favorite list?";

            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {

                    bootbox.alert("Professional added to your favorite list");
                    $("#aFavorite" + professionalID).removeAttr("onclick");
                    $("#aFavorite" + professionalID).addClass("edit_sf");
                    $("#aFavorite" + professionalID).html("Added");

                    $.ajax({
                        url: '/searchresult.aspx/SetFavoriteProfessional',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "professionalID":"' + professionalID + '"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }

    </script>
</asp:Content>
