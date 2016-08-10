
<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReferDetail.aspx.cs" Inherits="ReferLocals.ReferDetail" %>
<%@ Import Namespace="DataAccess.HelperClasses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        #twitter-widget-0 {
            position: relative !important;
            opacity: 0;
            z-index: 150;
        }

        .xl .btn, btn-o, widget {
            width: 100%;
            height: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Search Result Mid HTML ====================================-->

    <div id="divDataNotFound" runat="server" visible="false">
        <div class="container">
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="row">
                                <h3 style="color: #0094ff">User not found</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divUserData" runat="server" class="s_result">
        <div class="container">
            <div class="col-md-9">
                <div class="searchcard">
                    <div class="linktextsearch">
                          <div class="flag_tooltip jobdone1"><a id="aFlag" runat="server">
                                        <i id="iFlag" runat="server" class="fa fa-flag-o"></i></a><span class="tooltipflag">Flag</span> </div>
                                    <div class="search_tooltip jobdone1">
                                        <a id="aFavorite" runat="server">
                                            <i id="iFavorite" runat="server" class="fa fa-heart-o"></i>
                                        </a><span class="tooltipsearch">Follow</span>
                                    </div><div class="jobdone">
                            <p>( <asp:Literal ID="ltJobsDone" runat="server"></asp:Literal> jobs done ) </p>
                        </div>
                    </div>




                    <asp:Repeater ID="rptDetail"  runat="server" OnItemDataBound="rptDetail_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-md-4">
                                <div class="searchcardImage">

                                    <img src="<%#!string.IsNullOrEmpty(Convert.ToString(Eval("Image")))?Common.UserImagesPath+Eval("Image"):Common.NoImageIcon%>?width=169&height=169&mode=crop&scale=both" alt="<%#Eval("Image") %>"  />
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="fieldSearch">
                                    <h3 class="searchfsttext"><strong><%#Eval("FirstName") %> <%#Eval("LastName") %></strong> </h3>

                                    <div class="col-md-12" style="margin: 13px 0px 0px;">
                                        <div class="address_search">
                                            <p class="searchBold"><i class="fa fa-briefcase"></i><%#Eval("CompanyName") %>  </p>
                                            <p class="searchBold"><i class="fa fa-map-marker"></i><%#BindAddress(Convert.ToInt64(Eval("StateId")))%></p>
                                            <p><i class="fa fa-at"></i><a href="#"><%#Eval("Email") %> </a></p>
                                            <p><i class="fa fa-phone"></i>+<%#Eval("CountryCode") %>-<%#Eval("PhoneNumber") %></p>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <a id="aInviteToViewJob" runat="server" clientidmode="Static"  data-toggle="modal" class="btn blue">Invite to view your Job </a>
                                            <%--<button class="btn blue" type="button">Send a Message</button>--%>
                                            <a id="aSendMsgModal" runat="server" clientidmode="Static" data-toggle="modal" class="btn blue">Send a Message</a>
                                            <a id="aShareThisProfile" runat="server" clientidmode="Static" class="btn blue" href="#divShareThisProfileModal" data-toggle="modal">Share this Profile </a>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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
                    <div id="JobModal" class="modal fade estimateFade" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Invite to view your Job  </h4>
                            </div>
                            <div class="modal-body">
                                <div class="scroller" style="height: 320px" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="form_c_checkbox">
                                                <div id="divJobs" runat="server" clientidmode="Static" class="col-md-12 form-group Popupjob_c1">
                                                    <asp:Repeater ID="rptJobs" runat="server">
                                                        <ItemTemplate>
                                                            <div class="check_Box">
                                                                <input class="chkJob" type="checkbox" name="vehicle" value="/jobdetail/<%#Eval("Id") %>/<%#Eval("UrlFriendlyTitle") %>"><%#Eval("Title") %><br>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </div>
                                            </div>
                                            <!--<div class="form-group groupestimate" style="background:#fff; padding: 10px 0px; margin: 0px;"> <button type="button" class="btn green">Invite</button>  </div>-->


                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group groupestimate" style="background: #fff; padding: 10px 0px; margin: 0px;">
                                <a id="aInvite" runat="server" clientidmode="Static" class="btn green">Invite</a>
                            </div>


                        </div>
                    </div>
                </div>

                </div>
                <div id="divShareThisProfileModal" class="modal fade estimateFade" aria-hidden="true">
                    <div class="modal-dialog modal-dialogSocial">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Share This Profile</h4>
                            </div>
                            <div class="modal-body">
                                <div class="scroller" style="height: 350px; padding: 0;" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="btnsocialBtnDiv">
                                                <div class="btn_n_social_btn">

                                                    <a href="#" onclick="ShareThisProfile();" class="popup_share">
                                                        <img src="/images/a1.png" alt="">
                                                    </a>
                                                    <div style="position: relative; float: none; clear: both; overflow: hidden; height: 57px; margin: 0 0 8px; margin-top: 0px;">
                                                        <a style="z-index: 100;position:relative;" href="https://twitter.com/share" target="_blank" class="twitter-share-button popup_share" data-size="large">
                                                            <img src="/images/a2.png" alt=""></a>
                                                        <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>
                                                        <a href="#" class="popup_share" style="position: absolute; top: -1px; left: 0px; z-index: 1;">
                                                            <img src="/images/a2.png" alt="">
                                                        </a>
                                                    </div>
                                                    <a onclick="ShowDivEmailBox();" class="popup_share">
                                                        <img src="/images/a3.png" alt="">
                                                    </a>
                                                    <div id="divEmailBox" style="display: none;height:110px;" class="input_pop_field">
                                                        <input id="txtEmail" type="text" name="lname" placeholder="Email" class="p_inputPopup1"><br /><br />
                                                        <a class="submitPopupBtn" onclick="SendEmailForProfileLink();">Submit</a>
                                                    </div>
                                                    <a onclick="ShowDivTextBox()" class="popup_share t_popupshare">
                                                        <img src="/images/a4.png" alt="">
                                                    </a>

                                                    <div id="divTextBox" style="display: none;height:110px;width:100%;" class="input_pop_field">
                                                        <div style="width:212px;margin:0 auto;text-align:center">
                                                        <asp:DropDownList ID="drpCountryCode" ClientIDMode="Static" class="form-control form-control-solid placeholder-no-fix" Style="height:40px;width: 33%; float: left;" runat="server">
                                                            </asp:DropDownList>
                                                        <input id="txtPhone" style="float:left;width: 65%;margin-left: 1%;" type="text" onkeypress="return isNumberKey(event)" maxlength="16" name="lname" placeholder="Phone" class="p_inputPopup1"><br /><br /><br />
                                                        <a class="submitPopupBtn" onclick="SendTextForProfileLink();">Submit</a>
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
                <div class="row pull_1">
                    <div class="col-md-8 col-sm-12">
                        <div class="pullR">
                            <h4>People who Provided Strong <strong>Referral </strong>for <strong>
                                <asp:Label Text="" ID="lblFirstName" runat="server" />&nbsp;
                                <asp:Label Text="" ID="lblLastName" runat="server" /></strong> </h4>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="pull-right pullR">
                            <p class="sort">Sort By </p>
                            <asp:DropDownList ID="drpStatus" onchange="ShowReferList('Change');" class="table-group-action-input form-control input-inline input-small input-sm" runat="server">
                                <asp:ListItem Text="All" Value="2" />
                                <asp:ListItem Text="Positive" Value="1" />
                                <asp:ListItem Text="Negative" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="searchdiscription">
                    <div id="divRefer">
                    </div>

                    <div class="showsearch_professional" id="btnShowMore"><a onclick="return ShowReferList('No');" style="cursor: pointer;">Show More </a></div>

                </div>

            </div>
            <div class="col-md-3">
                <div class="advertise_header_background">
                    <a onclick="ReferMe('/AddReferal?Id=<%=UniqueId %>');">+ Refer Me </a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="advertise_text">
                    <h4>Cities Serving </h4>
                    <ul>
                        <asp:Repeater ID="rptCities" runat="server">
                            <ItemTemplate>
                                <li><a href="#"><%#Eval("CityName") %> <%#Eval("State") %>  </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>

            <div class="col-md-3">
                <div class="advertise_bg">

                    <div class="Add_advertise">

                        <div class="videolink">
                            <iframe width="100%" height="185" src="https://www.youtube.com/embed/JSKm7homfxw" frameborder="0" allowfullscreen></iframe>
                        </div>

                    </div>
                    <div class="Add_advertise app_bg_img">

                        <p class="textapp_text1">Post a Job faster </p>
                        <p class="textapp_text2">Install the <span>App </span>today </p>
                        <div class="col-md-10 col-md-offset-1 btnappimg"><a href="<%=DataAccess.HelperClasses.Common.IOSAppLink %>" target="_blank">
                            <img src="/images/image_app2.jpg" alt="">
                        </a></div>
                      <%--  <div class="col-md-10 col-md-offset-1 btnappimg"><a href="#">
                            <img src="/images/image_app3.jpg" alt="">
                        </a></div>--%>
                    </div>
                  
                </div>
            </div>
        </div>
    </div>
    <div id="divFlagModal" class="modal fade" aria-hidden="true">
       <div class="modal-dialog">
         <div class="modal-content">
           <div class="modal-header backgroundtitle">
             <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
             <h4 class="modal-title">Why you want to flag this Professional?</h4>
           </div>
           <div class="modal-body">
             <div class="scroller" style="height:150px" data-always-visible="1" data-rail-visible1="1">
               <div class="row">
                 <div class="col-md-12">
                   <div class="form-group">
                     <div class="col-md-12">
                        <textarea class="form-control" rows="5" id="txtReason" placeholder="Kindly tell us your reason"></textarea>
                     </div>
                   </div>
                 </div>
               </div>
             </div>
           </div>
           <div class="modal-footer">
              <button type="button" class="btn blue" data-dismiss="modal" aria-hidden="true" value="Cancel">Cancel</button><a onclick="FlagProfessional();"  class="btn green">Save changes</a>
           </div>
         </div>
       </div>
     </div>
    <script id="ReferScript" type="text/html">
        <div class="col-md-12 s_professional">
            {{if (IsRefered)}}
                {{if (SenderType==<%=(int)DataAccess.HelperClasses.HelperEnums.UserType.Professional%>)}}
                <h2 class="col-md-12"><a href="/referdetail?Id=${SenderUniqueId}">${SenderName} </a>from ${UserCityName},${UserZipName} <strong>will Refer </strong>${UserName} </h2>
            {{else}}
                <h2 class="col-md-12"><a href="/user/profile/${SenderUniqueId}">${SenderName} </a>from ${UserCityName},${UserZipName} <strong>will Refer </strong>${UserName} </h2>
            {{/if}}
            {{else}}
                 {{if (SenderType==<%=(int)DataAccess.HelperClasses.HelperEnums.UserType.Professional%>)}}
                <h2 class="col-md-12"><a href="/referdetail?Id=${SenderUniqueId}">${SenderName} </a>from ${UserCityName},${UserZipName} <strong>will not Refer </strong>${UserName} </h2>
            {{else}}
                <h2 class="col-md-12"><a href="/user/profile/${SenderUniqueId}">${SenderName} </a>from ${UserCityName},${UserZipName} <strong>will not Refer </strong>${UserName} </h2>
            {{/if}}
             <%--<h2 class="col-md-12"><a href="/user/profile/${SenderUniqueId}">${SenderName} </a>from ${UserCityName},${UserZipName} <strong>will not Refer </strong>${UserName} </h2>--%>
            {{/if}}
            <div class="col-md-3">
                <div class="professionalprofile">
                    <a href="#">{{if (Image!="")}}
                        <img src="<%=DataAccess.HelperClasses.Common.UserImagesPath %>${Image}" alt="${Image}" />
                        {{else}}
                        <img src="<%=DataAccess.HelperClasses.Common.NoImageIcon%>" alt="${Image}" />
                        {{/if}}
                    </a>
                </div>
            </div>
            <div class="col-md-9">
                <div class="text_professionalprofile">
                    <p>${Comment}  </p>
                </div>
            </div>
        </div>

    </script>

    <!--==================================== End Search Result Mid HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ShowReferList('No');
        });
        var take = 10;
        var index = 0;
        var subIndex = 1;
        function ShowReferList(type) {
            if (type == 'Change') {
                index = 0;
            }
            var refer = $('#<%=drpStatus.ClientID%> option:selected').val();
            index = index + 1;
            $.ajax({
                url: '/ReferDetail.aspx/GetReferList',
                type: "POST",
                data: '{ "index":"' + parseInt(index - 1) + '","take":"' + parseInt(take) + '","IsReferd":"' + parseInt(refer) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.length > 0) {
                        if (index - 1 == 0 || type == 'Change') {
                            $("#divRefer").empty();
                        }
                        $("#ReferScript").tmpl(result.d).appendTo("#divRefer");
                        if (result.d.length == 3) {
                            $('#btnShowMore').css("display", "block");
                        }
                        else {
                            $('#btnShowMore').css("display", "none");
                        }

                    }
                    else {
                        if (index - 1 == 0 || type == 'Change') {
                            $("#divRefer").html('<div style="width:100%;text-align:left;font-size:14px;font-weight:bold"><font color="black">No Record.</font></div>');
                            $('#btnShowMore').css("display", "none");
                        }
                    };
                }
            });
            return false;
        }

        function ReferMe(urlreferrer) {
            if (!IsLoggedIn) {

                window.location.href = "/Login?ReturnUrl=" + (location.pathname + location.search).substr(1);
            }
            else {
                if (urlreferrer != "");
                window.location.href = urlreferrer;
            }
        }

        function SetFavoriteProfessional(professionalID) {
            var confirmMsg = "";
            if ($(".aFavorite" + professionalID + " i").hasClass("fa-heart-o")) {
                confirmMsg = "Are your sure to add this professional to your favorite list?";
            }
            else {
                confirmMsg = "Are your sure to remove this professional from your favorite list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($(".aFavorite" + professionalID + " i").hasClass("fa-heart-o")) {
                        bootbox.alert("Professional added to your favorite list");
                        $(".aFavorite" + professionalID + " i").removeClass("fa fa-heart-o");
                        $(".aFavorite" + professionalID + " i").addClass("fa fa-heart");
                    }
                    else {
                        $(".aFavorite" + professionalID + " i").removeClass("fa fa-heart");
                        $(".aFavorite" + professionalID + " i").addClass("fa fa-heart-o");
                        bootbox.alert("Professional removed from your favorite list");
                    }

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
        var selectedProfessionalID = 0;

        function OpenFlagProfessionalModal(ID) {
            $("#divFlagModal").modal("show");
            selectedProfessionalID = ID;
        }
        function FlagProfessional() {
            SetFlagProfessional(selectedProfessionalID);
        }

        function SetFlagProfessional(professionalID) {
            var confirmMsg = "";
            if ($(".aFlag" + professionalID + " i").hasClass("fa-flag-o")) {
                confirmMsg = "Are your sure to add this professional to your flag list?";
            }
            else {
                confirmMsg = "Are your sure to remove this professional from your flag list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($(".aFlag" + professionalID + " i").hasClass("fa-flag-o")) {
                        $(".aFlag" + professionalID).hide();
                        bootbox.alert("Professional added to your flag list");
                        $(".aFlag" + professionalID + " i").removeClass("fa fa-flag-o");
                        $(".aFlag" + professionalID + " i").addClass("fa fa-flag");
                    }
                    else {
                        $(".aFlag" + professionalID + " i").removeClass("fa fa-flag");
                        $(".aFlag" + professionalID + " i").addClass("fa fa-flag-o");
                        bootbox.alert("Professional removed from your flag list");
                    }
                    var reason = $("#txtReason").val();
                    $.ajax({
                        url: '/searchresult.aspx/SetFlagProfessional',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "professionalID":"' + professionalID + '","reason":"' + reason + '"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }

        var inviteeID;
        var msgReceiverID;
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
        function ShowInviteJobModal(userID) {
            inviteeID = userID;
            $("#aInvite").show();
            $("#JobModal").modal("show");
        }

        function SendJobInvite() {
            bootbox.alert("Your invitation has been sent.");
            $("#aInvite").hide();
            $("#JobModal").modal("hide");
            var jobUrls = [];
            $.each($("input[class='chkJob']:checked"), function () {
                jobUrls.push($(this).val());
            });


            $.ajax({
                url: '/searchresult.aspx/SendJobInvite',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{ "inviteeID":"' + inviteeID + '","jobUrls":"' + jobUrls.join(",") + '"}',
                success: function (data) {

                }
            });

        }

        $(function () {

            var userID = '<%=UserId%>';
            if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
                //window.location = "referLocalsUser://profile/userType=2&userId=" + userID + "";
                window.location = "referLocalsUser://profile/userType=2&userId=" + userID + "";

                //setTimeout(function () {
                //    window.location = 'itms-apps://itunes.apple.com/app/browze-app/id1049738179?mt=8'
                //}, 250);

            }
            else if (navigator.userAgent.match(/Android/i)) {
              //  window.location = 'intent://#Intent;scheme=browze;package=com.browze;end;';
            }
            //else {
            //    createCookie("IsMobileSiteOpen", "0", 1);
            //    window.location = 'https://www.​browze.​co/';
            //}

        });
    </script>
    <script src="/js/share.js" type="text/javascript"></script>
</asp:Content>
