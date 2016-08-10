<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Favorites.aspx.cs" Inherits="ReferLocals.Favorites" %>

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
                                        <li class="active"><a href="/favorites"><i class="fa fa-heart"></i>Favorites </a><span class="after"></span></li>
                                        <li><a href="/followers"><i class="fa fa-star"></i>Followers </a></li>
                                    </ul>
                                </div>
                                <div class="col-md-9">


                                    <div class="tabbable-line tabbable-custom-profile vsprofiletable5 fav_user_page">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_1_11" data-toggle="tab">Professionals </a></li>
                                            <li><a href="#tab_1_23" data-toggle="tab">User </a></li>
                                            <li><a href="#tab_1_24" data-toggle="tab">Jobs </a></li>
                                        </ul>
                                        <div class="tab-content">

                                            <div class="tab-pane active" id="tab_1_11">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Professionals List </h2>
                                                            <div id="divFavoriteProfessionals" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreFavoriteProfessionals" class="table_reqBtn">
                                                        <a onclick="ShowMoreFavoriteProfessionals();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="tab_1_23">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">User List  </h2>
                                                            <div id="divFavoriteUsers" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreFavoriteUsers" class="table_reqBtn">
                                                        <a onclick="ShowMoreFavoriteUsers();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="tab_1_24">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Jobs </h2>
                                                            <div id="divFavoriteJobs" class="deah_board_full1">

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreFavoriteJobs" class="table_reqBtn">
                                                        <button type="button" class="btn blue btn-outline">SHOW MORE</button>
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
    <div id="SendMsgModal" class="modal fade estimateFade" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Send Message  </h4>
                            </div>
                            <div class="modal-body">
                                <div class="box_z_textarea">
                                    <div class="form-group popup_z_teztarea_4">
                                        <textarea  class="form-control" rows="5" id="txtMsg" placeholder="Type your Message"></textarea>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group groupestimate" style="background: #fff; padding: 10px 0px; margin: 0px;"><a onclick="SendMessage();" class="btn green">Send Message</a>  </div>

                        </div>
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script id="FavoriteUserTemplate" type="text/html">
        <div class="desh_board1 user_fav_Page">
            <div class="col-md-2 textDah_board">
                <img src="${UserImage}" alt="">
            </div>
            <div class="col-md-7 textDah_board1 fav_messg_text">
                <h2 class="leakage">${Username}</h2>
                <p>${UserLocation}</p>
            </div>
            <div class="col-md-3 textDah_board3 fav_messg_ancher">
                 
                <div  class="send_fav_messages"><a id="aSendMsgModal${UserID}" onclick="ShowSendMsgModal(${UserID})" class="s_f_messages">Send Message</a> </div>
            </div>
        </div>
    </script>
    <script id="FavoriteProfessionalsTemplate" type="text/html">
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
                <div class="send_fav_messages"><a onclick="ShowSendMsgModal(${UserID})" class="s_f_messages">Send Message</a> </div>
            </div>
        </div>
    </script>
    <script id="FavoriteJobTemplate" type="text/html">
        <div class="desh_board1 user_fav_Page">
            <div class="col-md-2 textDah_board">
                <img src="${JobImage}" alt="">
            </div>
            <div class="col-md-7 textDah_board1 fav_messg_text">
                <h2 class="leakage">${JobTitle}
                </h2>
                <p>${JobLocation}</p>
                {{if JobStatus==1}}
                Job is Open
                {{else JobStatus==2}}
                Job is done
                {{else JobStatus==3}}
                Job is cancelled
                {{/if}}

            </div>
            <div class="col-md-3 textDah_board3 fav_messg_ancher">
                <div class="send_fav_messages">
                    {{if JobStatus==1}}
                <a href="/jobdetail/${JobID}/${UrlFriendlyTitle}" class="s_f_messages">View Job</a>

                    {{/if}}
                    

                </div>
            </div>
        </div>
    </script>
    <script type="text/javascript">
        var pageIndexFavoriteUsers = 0; var pageIndexFavoriteProfessionals = 0;
        var pageIndexFavoriteJobs = 0;
        var pageSize = 10;
        var msgReceiverID;
        $(function () {
            GetFavoriteUsers(0);
            GetFavoriteProfessionals(0);
            GetFavoriteJobs(0);
        })
        function ShowSendMsgModal(userID) {
            msgReceiverID = userID;
            $("#SendMsgModal").modal("show");
        }
        function SendMessage() {


            $("#SendMsgModal").modal("hide");
            var msg = $("#txtMsg").val();
            if (msg == "") {
                bootbox.alert("Please type your message");
            }
            else {
                bootbox.alert("Your message has been sent.");
                $("#txtMsg").val("");
                $.ajax({
                    url: '/searchresult.aspx/SendMessage',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "msgReceiverID":"' + msgReceiverID + '","msg":"' + msg + '"}',
                    success: function (data) {

                    }
                });
            }
        }
        function GetFavoriteJobs(pageIndex) {

            $("#divShowMoreFavoriteJobs").hide();
            if ($("#divFavoriteJobs").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/favorites.aspx/GetMyFavoriteJobs',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divFavoriteJobs").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreFavoriteJobs").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreFavoriteJobs").show();
                        }

                        $("#FavoriteJobTemplate").tmpl(result.d.Jobs).appendTo("#divFavoriteJobs");

                    }
                    else {
                        $("#divFavoriteJobs").html("No records found");
                    }

                }
            });
        }
        function GetFavoriteUsers(pageIndex) {

            $("#divShowMoreFavoriteUsers").hide();
            if ($("#divFavoriteUsers").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/favorites.aspx/GetMyFavoriteUsers',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divFavoriteUsers").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreFavoriteUsers").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreFavoriteUsers").show();
                        }

                        $("#FavoriteUserTemplate").tmpl(result.d.Users).appendTo("#divFavoriteUsers");

                    }
                    else {
                        $("#divFavoriteUsers").html("No records found");
                    }

                }
            });
        }
        function ShowMoreFavoriteUsers() {
            pageIndexFavoriteUsers++;
            GetFavoriteUsers(pageIndexFavoriteUsers);
        }
        function ShowMoreFavoriteUsers() {
            pageIndexFavoriteJobs++;
            GetFavoriteJobs(pageIndexFavoriteJobs);
        }
        function GetFavoriteProfessionals(pageIndex) {

            $("#divShowMoreFavoriteProfessionals").hide();
            if ($("#divFavoriteProfessionals").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/favorites.aspx/GetMyFavoriteProfessionals',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divFavoriteProfessionals").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreFavoriteProfessionals").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreFavoriteProfessionals").show();
                        }

                        $("#FavoriteProfessionalsTemplate").tmpl(result.d.Professionals).appendTo("#divFavoriteProfessionals");

                    }
                    else {
                        $("#divFavoriteProfessionals").html("No records found");
                    }

                }
            });
        }
        function ShowMoreFavoriteProfessionals() {
            pageIndexFavoriteProfessionals++;
            GetFavoriteProfessionals(pageIndexFavoriteProfessionals);
        }
    </script>
</asp:Content>
