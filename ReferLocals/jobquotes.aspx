<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="jobquotes.aspx.cs" Inherits="ReferLocals.jobquotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="job_quote">
        <div class="container">
            <div class="col-md-12 quotes_h1">
                <%--<h1>Quotes you Received for Job <strong>"Leakage In Pipe " </strong></h1>--%>
                <div class="dataTables_length" style="float:left">
                    <label>
                        Sort By:
          <select id="drpSort" onchange="SortJobQuotes(this.value);" class="table-group-action-input form-control input-inline input-small input-sm">
              <option value="0">Low to High</option>
              <option value="1">High to Low</option>

          </select>
                    </label>
                </div>
            </div>
            <div class="col-md-9">
                <div class="innerJobeQuote2">
                    <h2 class="main_quote_heading">Quotes For <%=RouteData.Values["Title"] %> </h2>
                    <div id="divJobQuoteList">
                    </div>

                </div>
                <div id="divShowMoreQuotes" class="quote1_showMore">
                    <button onclick="BindJobQuotes();" class="btn_c" type="button">Show More </button>
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
            <div class="col-md-3">
                <div class="advertise_bg">
                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img3.jpg" alt="advertise image">
                        </a>
                    </div>
                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img2.jpg" alt="advertise image">
                        </a>
                    </div>
                    <div class="Add_advertise">
                        <a href="#">
                            <img src="/images/advetise_img1.jpg" alt="advertise image">
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script type="text/html" id="jobQuoteTemplate">
        <div id="divJobQuote${ID}" class="innerJobquote">
            <h2 class="quote_head_h2">${ProfessionalName} </h2>
            <div class="col-md-2">
                <img src="${ProfessionalImage}?height=107&width=107&mode=crop&scale=both" alt="#" class="quote_img">
            </div>
            <div class="col-md-6">
                <div class="quotepage">
                    <h3 class="quote_head">Estimated given : $${Estimate}</h3>
                    <p class="quote_head1">${Comments}</p>
                </div>
            </div>
            <div class="col-md-4" style="padding: 0px 0px 0px 5px;">
                <div class="b_quote_btn">
                    <div class="col-md-6">
                        <a class="btn btnVQuote" href="/AppointmentForm?quoteId=${ID}" >Accept</a>
                    </div>
                    <div class="col-md-6">
                        <button class="btn btnVQuote" onclick="AcceptRejectQuote(${ID},false);" type="button">Decline</button>
                    </div>
                    <div class="col-md-12">
                       <a id="aSendMsgModal" onclick="ShowSendMsgModal(${ProfessionalID},${JobID})"  clientidmode="Static" data-toggle="modal" class="btn btnVQuote">Send a Message</a>
                         <%--<button class="btn btnVQuote" type="button">Send Message</button>--%>
                    </div>
                </div>
            </div>
        </div>
    </script>
    <script type="text/javascript">
        var pageIndex = 0;
        var pageSize = 10;
        function SortJobQuotes(orderBy) {
            pageIndex = 0;
            BindJobQuotes();
        }
        function BindJobQuotes() {
            var orderBy = $("#drpSort").val();
            $("#divShowMoreQuotes").hide();

            var jobID = '<%=RouteData.Values["Id"]%>';
            $.ajax({
                url: '/jobquotes.aspx/GetJobQuotes',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{ "jobID":"' + jobID + '","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '","orderBy":"' + orderBy + '"}',
                success: function (data) {
                    var result = data.d;
                    if (pageIndex == 0) {
                        $("#divJobQuoteList").empty();
                    }

                    if (result.HideShowMore) {
                        $("#divShowMoreQuotes").hide();

                    }
                    else {
                        $("#divShowMoreQuotes").show();
                    }
                    $("#jobQuoteTemplate").tmpl(result.QuoteList).appendTo("#divJobQuoteList");
                    pageIndex++;
                },
                error: function (xhr) {
                    bootbox.alert(xhr.responseText);
                }
            });
        }
        $(function () {
            BindJobQuotes(0);
        });

        function AcceptRejectQuote(id, status) {
            bootbox.confirm('Are you sure?', function (result) {
                if (result)
                {
                    $("#divJobQuote" + id).hide();
                    $.ajax({
                        url: '/jobquotes.aspx/AcceptRejectQuote',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "id":"' + id + '","status":"' + status + '"}',
                        success: function (data) {
                            if (status == true)
                            {
                                window.location.href = "/AppointmentForm?quoteId=" + id;
                            }
                        }
                    })
                }});
            }
               
        var msgReceiverID;
        var jobID;
        function ShowSendMsgModal(userID,jobid) {
            msgReceiverID = userID;
            jobID = jobid;
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
                    url: '/jobquotes.aspx/SendMessage',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "msgReceiverID":"' + msgReceiverID + '","jobID":"'+jobID+'","msg":"' + msg + '"}',
                    success: function (data) {

                    }
                });
            }
        }
    </script>
</asp:Content>
