
<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserPublicProfile.aspx.cs" Inherits="ReferLocals.UserPublicProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div id="divUserData"  runat="server" class="s_result">
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
                                    </div>
                        <%--<div class="search_tooltip jobdone1"><a href="#"><i class="fa fa-heart-o"></i></a><span class="tooltipsearch">Follow</span> </div>--%>
                        <div class="jobdone">
                            <p>( <asp:Literal ID="ltJobsDone" runat="server"></asp:Literal> jobs done ) </p>
                        </div>
                    </div>




                    <asp:Repeater ID="rptDetail" runat="server" OnItemDataBound="rptDetail_ItemDataBound" >
                        <ItemTemplate>
                            <div class="col-md-4">
                                <div class="searchcardImage">

                                    <img src="<%=DataAccess.HelperClasses.Common.UserImagesPath%><%#Eval("Image") %>" alt="#" />
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="fieldSearch">
                                    <h3 class="searchfsttext"><strong><%#Eval("FirstName") %> <%#Eval("LastName") %></strong> </h3>

                                    <div class="col-md-12" style="margin: 13px 0px 0px;">
                                        <div class="address_search">
                                            
                                            <p class="searchBold"><i class="fa fa-map-marker"></i><%#BindAddress(Convert.ToInt64(Eval("StateId")))%></p>
                                            <%--<p><i class="fa fa-at"></i><a href="#"><%#Eval("Email") %> </a></p>
                                            <p><i class="fa fa-phone"></i>+<%#Eval("CountryCode") %>-<%#Eval("PhoneNumber") %>  </p>--%>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <a class="btn blue" href="/user/jobs/<%#Eval("UniqueId")%>">View Jobs</a>
                                            <a id="aSendMsgModal" runat="server" clientidmode="Static" data-toggle="modal" class="btn blue">Send a Message</a>
                                            <a class="btn blue" href="/user/referrals/<%#Eval("UniqueId")%>">My Referrals</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


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
                <div class="row pull_1">
                    <div class="col-md-8 col-sm-12">
                        <div class="pullR">
                            <h4>Professionals Strongly Referred by <strong>
                                <asp:Label Text="" ID="lblFirstName" runat="server" />&nbsp;
                                <asp:Label Text="" ID="lblLastName" runat="server" /></strong> </h4>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <div class="pull-right pullR">
                            <p class="sort" style="margin: 0 8px 0 0;">Sort By </p>
                            <asp:DropDownList ID="drpStatus" onchange="GetUserReferals(0);" class="table-group-action-input form-control input-inline input-small input-sm" 
                                runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="All" Value="2" />
                                <asp:ListItem Text="Positive" Value="1" />
                                <asp:ListItem Text="Negative" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="searchdiscription">
                    <div id="divReferalList">
                    </div>

                     <div id="divShowMoreReferals" class="table_reqBtn">
                                                        <a class="btn blue btn-outline" onclick="ShowMoreUserReferals();" type="button">SHOW MORE</a>
                    </div>
                    

                </div>

            </div>
           
           
            <div class="col-md-3">
                <div class="advertise_bg">

                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img3.jpg" alt="advertise image" />
                        </a>
                    </div>
                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img2.jpg" alt="advertise image" />
                        </a>
                    </div>
                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img1.jpg" alt="advertise image" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script  id="referalListTemplate" type="text/html">
        <div class="col-md-12 s_professional">
            {{if (IsRefered)}}
            
            <h2 class="col-md-12"><a href="/referdetail?Id=${UserUniqueId}">${ProfessionalName}</a> from ${CityName}<strong> is Referred </strong>by <%=lblFirstName.Text %> <%=lblLastName.Text %> </h2>

            {{else}}
             <h2 class="col-md-12"><a href="/referdetail?Id=${UserUniqueId}">${ProfessionalName} </a> from ${CityName} <strong> is not Referred</strong>by <%=lblFirstName.Text %> <%=lblLastName.Text %> </h2>
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
        var pageIndexUserReferals = 0;
        function ShowMoreUserReferals() {
            pageIndexUserReferals++;
            GetUserReferals(pageIndexUserReferals);
        }
        $(document).ready(function () {
            //ShowReferList('No');
            GetUserReferals(0);
        });

        var pageSize = 10;
        var index = 0;
        var subIndex = 1;
        function GetUserReferals(pageIndex) {
            if (pageIndex == 0)
            {
                pageIndexUserReferals = 0;
            }
            $("#divShowMoreReferals").hide();
            if ($("#divReferalList").html() == "") {
                pageIndex = 0;
                pageIndexUserReferals = 0;
            }
            var isReferred =parseInt( $("#drpStatus").val());
            $.ajax({
                url: '/userpublicprofile.aspx/GetReferList',
                type: "POST",
                data: '{"index":"' + pageIndex + '","take":"' + pageSize + '","IsReferd":"' + isReferred + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $("#spanReferalCount").html(result.d.ReferalCount);
                    if (result.d.ReferalCount > 0) {
                        if (pageIndex == 0) {
                            $("#divReferalList").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreReferals").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreReferals").show();
                        }

                        $("#referalListTemplate").tmpl(result.d.Referals).appendTo("#divReferalList");

                    }
                    else {
                        $("#divReferalList").html("No records found");
                    }

                }
            });
        }

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

        function SetFlagUser(userID) {
            var confirmMsg = "";
            if ($(".aFlag" + userID + " i").hasClass("fa-flag-o")) {
                confirmMsg = "Are your sure to add this User to your flag list?";
            }
            else {
                confirmMsg = "Are your sure to remove this User from your flag list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($(".aFlag" + userID + " i").hasClass("fa-flag-o")) {
                        $(".aFlag" + userID).hide();
                        bootbox.alert("User added to your flag list");
                        $(".aFlag" + userID + " i").removeClass("fa fa-flag-o");
                        $(".aFlag" + userID + " i").addClass("fa fa-flag");
                    }
                    else {
                        $(".aFlag" + userID + " i").removeClass("fa fa-flag");
                        $(".aFlag" + userID + " i").addClass("fa fa-flag-o");
                        bootbox.alert("User removed from your flag list");
                    }

                    $.ajax({
                        url: '/UserPublicProfile.aspx/SetFlagUser',
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
        function SetFavoriteUser(userID) {
            var confirmMsg="";
            if ($(".aFavorite" + userID + " i").hasClass("fa-heart-o")) {
                confirmMsg = "Are your sure to add this User to your favorite list?";
            }
            else {
                confirmMsg = "Are your sure to remove this User from your favorite list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed)
                {
                    if ($(".aFavorite" + userID + " i").hasClass("fa-heart-o")) {
                        bootbox.alert("User added to your favorite list");
                        $(".aFavorite" + userID + " i").removeClass("fa fa-heart-o");
                        $(".aFavorite" + userID + " i").addClass("fa fa-heart");
                    }
                    else {
                        $(".aFavorite" + userID + " i").removeClass("fa fa-heart");
                        $(".aFavorite" + userID + " i").addClass("fa fa-heart-o");
                        bootbox.alert("User removed from your favorite list");
                    }

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
        function ShowLoginAlert(message) {
            bootbox.alert(message);
        }
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
    </script>
</asp:Content>
