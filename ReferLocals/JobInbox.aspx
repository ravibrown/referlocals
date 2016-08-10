<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="JobInbox.aspx.cs" Inherits="ReferLocals.JobInbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="requestProfile">
        <div class="container">
            <div class="profile ed_profile">

                <div class="row profile-account">

                    <div class="col-md-3">
                        <ul class="ver-inline-menu tabbable margin-bottom-10">
                            <li ><a href="/inbox"><i class="fa fa-cog"></i>Inbox </a><span class="after"></span></li>
                            <li class="active"><a href="/jobinbox"><i class="fa fa-picture-o"></i>Job Messages </a></li>
                        </ul>
                    </div>

                    <div class="col-md-9">
                        <div class="messgeProfile">
                            <div class="portlet light portlet-fit bordered">
                                <div class="messge_head_bg">
                                    <h2>Job Messages </h2>
                                </div>
                                <div class="portlet-body">
                                    <div id="divInbox" class="timeline">
                                    </div>
                                    <div id="divShowMoreInbox" class="show_more_mesgs"><a onclick="ShowMoreInbox();" class="btn btn-circle green btn-sm vs_btn" data-close-others="true" type="button">Show More </a></div>
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

    <script id="InboxTemplate" type="text/html">
        <div class="timeline-item">
            <div class="timeline-badge">
                <img class="timeline-badge-userpic jobImgRadius" src="${JobImage}" alt="">
            </div>
            <div class="timeline-body">
                <div class="timeline-body-arrow"></div>
                <div class="timeline-body-head">
                    <div class="timeline-body-head-caption"><a href="javascript:;" class="timeline-body-title font-blue-madison">${JobTitle}</a> <span class="timeline-body-time font-grey-cascade">Replied at ${FormattedMessageOn}</span>
                       {{if IsRepliedByLoggedInUser}}
                         <span class="iconRedo1"> <i class="icon-action-redo font-red-sunglo"></i> </span>
                        {{/if}}
                    </div>
                    <div class="timeline-body-head-actions">
                        <div class="btn-group">
                            <a href="/Inbox.aspx?jobID=${JobID}" class="btn btn-circle green btn-sm vs_btn" type="button" data-close-others="true">View</a>
                            <%--<button class="btn btn-circle green btn-sm vs_btn" type="button" data-close-others="true"> Delete </button>--%>
                        </div>
                    </div>
                </div>
                <div class="timeline-body-content"><span class="font-grey-cascade">${Message} </span></div>
            </div>
        </div>
    </script>

    <script type="text/javascript">
        var pageIndex = 0;

        var pageSize = 10;

        function GetJobInbox(pageIndex) {

            $("#divShowMoreInbox").hide();
            if ($("#divInbox").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/JobInbox.aspx/GetJobInbox',
                type: "POST",
                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.Count > 0) {
                        if (pageIndex == 0) {
                            $("#divInbox").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreInbox").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreInbox").show();
                        }

                        $("#InboxTemplate").tmpl(result.d.Inbox).appendTo("#divInbox");

                    }
                    else {
                        $("#divInbox").html("No records found");
                    }

                }
            });
        }
        function ShowMoreInbox() {
            pageIndex++;
            GetJobInbox(pageIndex);
        }
        $(function () {
            GetJobInbox(0);
        })
    </script>
</asp:Content>
