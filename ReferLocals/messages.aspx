<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="ReferLocals.messages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="requestProfile">
        <div class="container">
            <div class="profile ed_profile">
                <div class="row profile-account">
                    <div class="col-md-3">
                        <ul class="ver-inline-menu tabbable margin-bottom-10">
                            <li><a href="/inbox"><i class="fa fa-cog"></i>Inbox </a><span class="after"></span></li>
                            <li><a href="/jobinbox"><i class="fa fa-picture-o"></i>Job Messages </a></li>
                        </ul>
                    </div>
                    <div class="col-md-9">
                        <div class="messgeProfile">
                            <div class="messge_head_bg">
                                <h2>Chats Messages </h2>
                            </div>

                            <div class="portlet light bordered">

                                <div class="portlet-title">

                                    <div class="caption"><a href="/inbox" class="backmessage"><i class="icon-action-undo font-red-sunglo"></i><span class="caption-subject font-red-sunglo bold uppercase">Back</span> </a></div>

                                </div>

                                <div class="portlet-body" id="chats">
                                    <div id="divShowMoreChat" style="clear: both; overflow: hidden; text-align: center; width: 100%;">
                                        <a onclick="ShowMoreChat();">view more</a>
                                    </div>
                                    <div class="scroller" style="height: 525px;" data-always-visible="1" data-rail-visible1="1">

                                        <ul id="ulChat" class="chats">
                                        </ul>

                                    </div>

                                    <div id="divReply" style="display: none;position:relative" class="chat-form">

                                        <div class="input-cont">
                                            <input id="txtMessage" class="form-control" type="text" placeholder="Type a message here..." />
                                        </div>
                                        <div class="btn-cont"><span class="arrow"></span><a id="aSendMsg" onclick="SendMessage();" class="btn blue icn-only"><i class="fa fa-check icon-white"></i></a></div>
                                        <div id="divLoading" style="display: none; position: absolute; top: 13px; left: 47%;">
                                            <img src="/images/default.gif" height="28" width="28" />
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
    <script id="chatTemplate" type="text/html">
        {{if loggedInUser==UserID}}
        <li class="out">
            <img class="avatar" alt="" src="${UserImage}" />
            <div class="message">
                <span class="arrow"></span>
                <a href="javascript:;" class="name">${Username} </a>
                <span class="datetime">at ${FormattedMessageOn}</span>
                <span class="body">${Message} </span>
            </div>
        </li>
        {{else}}
        <li class="in">
            <img class="avatar" alt="" src="${UserImage}" />
            <div class="message">
                <span class="arrow"></span>
                <a href="javascript:;" class="name">${Username}</a>
                <span class="datetime">at ${FormattedMessageOn}</span>
                <span class="body">${Message} </span>
            </div>
        </li>
        {{/if}}

    </script>

    <script type="text/javascript">
        var pageIndex = 0;

        var pageSize = 10;
        var threadID = <%=Request.QueryString["threadID"]%>;

        $('#txtMessage').keypress(function (e) {
            var key = e.which;
            if(key == 13)  // the enter key code
            {
                $('#aSendMsg').click();
                return false;  
            }
        });   
        function SendMessage()
        {
            $("#divLoading").show();
            var msg=$("#txtMessage").val();
            if(msg=="")
            {
                bootbox.alert("Please type something...");
            }
            else
            {$("#txtMessage").val("");
                $.ajax({
                    url: '/messages.aspx/SendMessage',
                    type: "POST",
                    data: '{"threadID":"' + threadID + '","message":"'+msg+'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        $("#divLoading").hide();
                        if(result.d.ResultStatus==1)
                        {
                            $("#chatTemplate").tmpl(result.d).appendTo("#ulChat");
                            $('.scroller').slimScroll({height: '500px',start:'bottom'});
                        }
                    }
                });
            }
        }
        function GetChat(pageIndex) {
            if (threadID > 0) {
                $("#divShowMoreChat").hide();
                if ($("#ulChat").html() == "") {
                    pageIndex = 0;
                }
                $.ajax({
                    url: '/messages.aspx/GetChat',
                    type: "POST",
                    data: '{"threadID":"' + threadID + '", "pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                        if (result.d.Count > 0) {
                            $("#divReply").show();
                            if (pageIndex == 0) {
                                $("#ulChat").empty();
                            }
                            if (result.d.HideShowMore) {
                                $("#divShowMoreChat").hide();
                            }
                            else {
                                //pageIndexJobsOpen++;
                                $("#divShowMoreChat").show();
                            }

                            $("#chatTemplate").tmpl(result.d.Inbox).prependTo("#ulChat");
                            if(pageIndex==0)
                            {
                                $('.scroller').slimScroll({height: '500px',start:'bottom'})
                            }
                            else
                            {
                                $('.scroller').slimScroll({height: '500px',start:'top'})
                            }
                        }
                        else {
                            $("#ulChat").html("No records found");
                        }

                    }
                });
            }
        }
        function ShowMoreChat() {
            pageIndex++;
            GetChat(pageIndex);
        }
        $(function () {
            GetChat(0);
        })
    </script>
    <script src="/js/jquery.slimscroll.min.js" type="text/javascript"></script>
</asp:Content>
