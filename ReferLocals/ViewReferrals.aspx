<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewReferrals.aspx.cs" Inherits="ReferLocals.ViewReferrals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="s_result">
        <div class="container">

            <div class="col-md-9">

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

                    <div class="about_referals" style="float: left; width: 100%; margin: 10px 0px;">
                         <a id="aShareMyReferrals" runat="server" style="float: right; margin: 0px 8px 0px 0px;" clientidmode="Static" 
                             class="btn blue" href="#divShareThisProfileModal" data-toggle="modal">Share Referrals </a>
                                           
                        <%--<button class="btn blue" type="button" style="float: right; margin: 0px 8px 0px 0px;">Share My Referrals</button>--%>
                    </div>
                    <div id="divReferalList">
                    </div>

                    <div id="divShowMoreReferals" class="table_reqBtn">
                        <a class="btn blue btn-outline" onclick="ShowMoreUserReferals();" >SHOW MORE</a>
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
    <div id="divShareThisProfileModal" class="modal fade estimateFade" aria-hidden="true">
                    <div class="modal-dialog modal-dialogSocial">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Share Referrals</h4>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script id="referalListTemplate" type="text/html">
        <div class="col-md-12 s_professional">
            {{if (IsRefered)}}
            
            <h2 class="col-md-12"><a href="/referdetail?Id=${UserUniqueId}">${ProfessionalName}</a> from ${CityName}<strong> is Referred </strong>by <%=lblFirstName.Text %> <%=lblLastName.Text %> </h2>

            {{else}}
             <h2 class="col-md-12"><a href="/referdetail?Id=${UserUniqueId}">${ProfessionalName} </a>from ${CityName} <strong>is not Referred</strong>by <%=lblFirstName.Text %> <%=lblLastName.Text %> </h2>
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
            if (pageIndex == 0) {
                pageIndexUserReferals = 0;
            }
            $("#divShowMoreReferals").hide();
            if ($("#divReferalList").html() == "") {
                pageIndex = 0;
                pageIndexUserReferals = 0;
            }
            var isReferred = parseInt($("#drpStatus").val());
            $.ajax({
                url: '/viewreferrals.aspx/GetReferList',
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

    </script>
<script src="/js/share.js" type="text/javascript"></script>
</asp:Content>
