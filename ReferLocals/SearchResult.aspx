<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="ReferLocals.SearchResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="/css/uniform.default.css" rel="stylesheet" type="text/css" />--%>
    <%-- <link href="/css/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
<link href="/css/plugins-md.min.css" rel="stylesheet" type="text/css" />
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Search Result Mid HTML ====================================-->


    <div class="s_result">
        <div class="container">

            <div class="col-md-9">
                <div class="search_h1 compressedh1">
                    <h1>Top <strong><%=subcategoryName %></strong> Referred by People Living Close to You </h1>
                </div>
                <div id="divDifferenCatMsg" runat="server" visible="false" style="background: red; padding: 10px; color: white; font-size: 15px; font-weight: bold; margin-bottom: 10px;">
                    <%--Following are the results from different professinal category.--%>
                    Our System matched the Phone Number or Email provided by you to following professionals
                </div>
                <div id="divResult" runat="server">
                    <asp:Repeater ID="rptResult" runat="server" OnItemDataBound="rptResult_OnItemDataBound">
                        <ItemTemplate>
                            <div class="searchcard">

                                <div class="linktextsearch">
                                    <div class="flag_tooltip jobdone1">
                                        <a id="aFlag" runat="server">
                                            <i id="iFlag" runat="server" class="fa fa-flag"></i></a><span class="tooltipflag">Flag</span>
                                    </div>
                                    <div class="search_tooltip jobdone1">
                                        <a id="aFavorite" runat="server">
                                            <i id="iFavorite" runat="server" class="fa fa-heart-o"></i>
                                        </a><span class="tooltipsearch">Follow</span>
                                    </div>
                                    <div class="jobdone">
                                        <p>( <%#Eval("JobsDone") %> jobs done ) </p>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="searchcardImage">
                                        <a href="#">
                                            <img src="<%=DataAccess.HelperClasses.Common.UserImagesPath%><%#!string.IsNullOrEmpty(Convert.ToString(Eval("Image")))?Eval("Image"):DataAccess.HelperClasses.Common.NoImageIconWithoutPath %>?width=245&height=255&mode=crop&scale=both" alt="<%#Eval("Image") %>">
                                        </a>
                                        &nbsp;&nbsp;
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnId" Value='<%#Eval("Id") %>' runat="server" />
                                <div class="col-md-8">
                                    <div class="fieldSearch">
                                        <h3 class="searchfsttext"><a href="/ReferDetail?Id=<%#Eval("UniqueId") %>"><%#Eval("FirstName") %> <%#Eval("LastName") %> </a></h3>

                                        <div class="col-md-6" style="margin: 13px 0px 0px;">
                                            <p style="margin: 14px 0;"><%#((List<DataAccess.Classes.ProfessionalUrls>)Eval("ProfessionalUrls"))!=null?((List<DataAccess.Classes.ProfessionalUrls>)Eval("ProfessionalUrls")).Count>0?((List<DataAccess.Classes.ProfessionalUrls>)Eval("ProfessionalUrls"))[0].SubCategoryName:"":""%></p>
                                            <h4>Cities Serving </h4>
                                            <asp:Repeater ID="rptCities" runat="server">
                                                <ItemTemplate>
                                                    <p style="margin: 14px 0;"><%#Eval("CityName") %></p>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                        <div class="col-md-6">
                                            <a href="/ReferDetail?Id=<%#Eval("UniqueId") %>" class="ancherback">Referrals:<%#GetTotalReferal(Convert.ToInt64(Eval("Id"))) %></a>
                                            <a id="aSendMsgModal" runat="server" clientidmode="Static" data-toggle="modal" class="ancherback">Send Message</a>
                                            <a id="aInviteToViewJob" runat="server" clientidmode="Static" data-toggle="modal" class="ancherback">Invite to view your Job </a>
                                            <a href="/ReferDetail?Id=<%#Eval("UniqueId") %>" class="ancherback">View Profile </a>
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
                <div id="divNoResult" runat="server">
                    No Record Found
                </div>
            </div>


            <div class="col-md-3">
                <div class="advertise_bg">
                    <div class="Add_advertise"><a href="/referprofessional/Refer" class="ancher_btn_professional">Refer A Professionals </a></div>

                    <div class="Add_advertise">

                        <div class="videolink">
                            <iframe width="100%" height="185" src="https://www.youtube.com/embed/JSKm7homfxw" frameborder="0" allowfullscreen></iframe>
                        </div>

                    </div>
                    <div class="Add_advertise app_bg_img">

                        <p class="textapp_text1">Post a Job faster </p>
                        <p class="textapp_text2">Install the <span>App </span>today </p>
                        <div class="col-md-10 col-md-offset-1 btnappimg">
                            <a href="<%=DataAccess.HelperClasses.Common.IOSAppLink %>" target="_blank">
                                <img src="/images/image_app2.jpg" alt="">
                            </a>
                        </div>
                       <%-- <div class="col-md-10 col-md-offset-1 btnappimg">
                            <a href="#">
                                <img src="/images/image_app3.jpg" alt="">
                            </a>
                        </div>--%>
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
    <!--==================================== End Search Result Mid HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

    <script type="text/javascript">
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
                        data: '{ "professionalID":"' + professionalID + '","reason":"'+reason+'"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }


        function ShowLoginAlert(message) {
            bootbox.alert(message);
        }
    </script>


    <script src="/js/jquery.slimscroll.min.js" type="text/javascript"></script>

    <script src="/js/jquery.uniform.min.js" type="text/javascript"></script>


</asp:Content>
