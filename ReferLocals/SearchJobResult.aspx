<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchJobResult.aspx.cs" Inherits="ReferLocals.SearchJobResult" %>

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


    <div class="search_ResultJobCard">
        <div class="container">
            <div class="col-md-9">
                <div class="search_h1 compressedh1">
                    <h1>Top <strong><%=subCategoryName %> </strong>Job Posted by People Living Close to You </h1>
                </div>
                <asp:Repeater ID="rptResults" runat="server" OnItemDataBound="rptResults_ItemDataBound">
                    <ItemTemplate>
                        <div class="searchJobCard">

                            <div class="linktextsearch">
                                <div class="flag_tooltip jobdone1">
                                    <a id="aFlag" runat="server">
                                        <i id="iFlag" runat="server" class="fa fa-flag-o"></i></a><span class="tooltipflag">Flag</span> </div>
                                <div class="search_tooltip jobdone1">
                                        <a id="aFavorite" runat="server">
                                            <i id="iFavorite" runat="server" class="fa fa-heart-o"></i>
                                        </a><span class="tooltipsearch">Follow</span>
                                    </div>
                                <%--<div class="search_tooltip jobdone1"><a href="#"><i class="fa fa-heart-o"></i></a><span class="tooltipsearch">Follow</span> </div>--%>
                                <div class="jobdone">
                                    <%-- <p>( 0 jobs done ) </p>--%>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="searchcardImage">
                                    <a href="#">
                                        <img src="<%#!string.IsNullOrEmpty(Convert.ToString(Eval("Image")))?DataAccess.HelperClasses.Common.JobImagesPath+Eval("Image"):DataAccess.HelperClasses.Common.JobDefaultImage%>?width=245&height=255&mode=crop&scale=both" alt="<%#Eval("Image") %>">
                                    </a>
                                </div>
                                <div>Posted By <a href="/user/profile/<%#Eval("CreaterUniqueId") %>"><%#Eval("Username") %></a></div>
                            </div>

                            <div class="col-md-8">
                                <div class="fieldSearch">
                                    <%--<h3 class="searchfsttext"><a href="/jobdetail/<%#Eval("Id") %>/<%#Convert.ToString(Eval("Title")).Replace(".","_").Replace(",","_").Replace("/","_") %>"><%#Eval("Title") %> </a></h3>--%>
                                    <h3 class="searchfsttext"><a href="/jobdetail/<%#Eval("Id") %>/<%#Eval("UrlFriendlyTitle")%>"><%#Eval("Title") %> </a></h3>

                                    <div class="col-md-6" style="margin: 13px 0px 0px;">
                                        <h4>Posted On <%#String.Format("{0:dddd, MMMM d, yyyy}", Convert.ToDateTime(Eval("CreatedDate"))) %> </h4>
                                        <h4><%#Eval("CityName") %>, <%#Eval("ZipName") %>  </h4>
                                        <p class="jobSearch_text2"><%#Eval("Description") %>  </p>
                                    </div>

                                    <div class="col-md-6">
                                        <a href="/jobdetail/<%#Eval("Id") %>/<%#Eval("UrlFriendlyTitle")%>" class="ancherback">Send Estimate</a>
                                        <a id="aShareThisJob" runat="server" clientidmode="Static" class="ancherback"  data-toggle="modal">Share This Job </a>
                                    <a href="/jobdetail/<%#Eval("Id") %>/<%#Eval("UrlFriendlyTitle")%>" class="ancherback">View Job Details </a>
                                    </div>
                                    
                                </div>


                            </div>
                        </div>
                        
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            
            <div class="col-md-3">
                <div class="advertise_bg">
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
                    <div class="Add_advertise">

                        <div class="videolink">
                            <iframe width="100%" height="185" src="https://www.youtube.com/embed/JSKm7homfxw" frameborder="0" allowfullscreen></iframe>
                        </div>

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
             <h4 class="modal-title">Why you want to flag this Job ?</h4>
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
              <button type="button" class="btn blue" data-dismiss="modal" aria-hidden="true" value="Cancel">Cancel</button><a onclick="FlagJob();"  class="btn green">Save changes</a>
           </div>
         </div>
       </div>
     </div>
    <div id="divShareThisJobModal" class="modal fade estimateFade" aria-hidden="true">
                    <div class="modal-dialog modal-dialogSocial">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Share This Job</h4>
                            </div>
                            <div class="modal-body">
                                <div class="scroller" style="height: 350px; padding: 0;" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="btnsocialBtnDiv">
                                                <div class="btn_n_social_btn">

                                                    <a href="#" onclick="ShareThisJob(0);" class="popup_share">
                                                        <img src="/images/a1.png" alt="">
                                                    </a>
                                                    <div style="position: relative; float: none; clear: both; overflow: hidden; height: 57px; margin: 0 0 8px; margin-top: 0px;">
                                                        <a style="z-index: 100; position: relative;" href="https://twitter.com/share" target="_blank" class="twitter-share-button popup_share" data-size="large">
                                                            <img src="/images/a2.png" alt=""></a>
                                                        <%--<script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>--%>
                                                        <a href="#" class="popup_share" style="position: absolute; top: -1px; left: 0px; z-index: 1;">
                                                            <img src="/images/a2.png" alt="">
                                                        </a>
                                                    </div>
                                                    <a onclick="ShowDivEmailBox(0);" class="popup_share">
                                                        <img src="/images/a3.png" alt="">
                                                    </a>
                                                    <div id="divEmailBox" style="display: none;height:110px;" class="input_pop_field">
                                                        <input id="txtEmail" type="text" name="lname" placeholder="Email" class="p_inputPopup1"><br /><br />
                                                        <a class="submitPopupBtn" onclick="SendEmailForJobLink(0,0);">Submit</a>
                                                    </div>
                                                    <a onclick="ShowDivTextBox(0)" class="popup_share t_popupshare">
                                                        <img src="/images/a4.png" alt="">
                                                    </a>

                                                    <div id="divTextBox" style="display: none;height:110px;width:100%;" class="input_pop_field">
                                                        <div style="width:212px;margin:0 auto;text-align:center">
                                                        <asp:DropDownList ID="drpCountryCode" ClientIDMode="Static" class="form-control form-control-solid placeholder-no-fix" Style="height:40px;width: 33%; float: left;" runat="server">
                                                            </asp:DropDownList>
                                                        <input id="txtPhone" style="float:left;width: 65%;margin-left: 1%;" type="text" onkeypress="return isNumberKey(event)" maxlength="16" name="lname" placeholder="Phone" class="p_inputPopup1"><br /><br /><br />
                                                        <a class="submitPopupBtn" onclick="SendTextForJobLink(0,0);">Submit</a>
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
    <!--==================================== End Search Result Mid HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="/js/shareFromList.js" type="text/javascript"></script>
     <script type="text/javascript">
         var selectedJobID = 0;
         function OpenFlagJobModal(jobID) {
             $("#divFlagModal").modal("show");
             selectedJobID = jobID;
         }
         function FlagJob() {
             SetFlagJob(selectedJobID);
         }
        function SetFavoriteJob(jobID) {
            var confirmMsg="";
            if ($(".aFavorite" + jobID + " i").hasClass("fa-heart-o")) {
                confirmMsg = "Are your sure to add this job to your favorite list?";
            }
            else {
                confirmMsg = "Are your sure to remove this job from your favorite list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed)
                {
                    if ($(".aFavorite" + jobID + " i").hasClass("fa-heart-o")) {
                        bootbox.alert("Job added to your favorite list");
                        $(".aFavorite" + jobID + " i").removeClass("fa fa-heart-o");
                        $(".aFavorite" + jobID + " i").addClass("fa fa-heart");
                    }
                    else {
                        $(".aFavorite" + jobID + " i").removeClass("fa fa-heart");
                        $(".aFavorite" + jobID + " i").addClass("fa fa-heart-o");
                        bootbox.alert("Job removed from your favorite list");
                    }

                    $.ajax({
                        url: '/searchjobresult.aspx/SetFavoriteJob',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "jobID":"' + jobID + '"}',
                        success: function (data) {

                        }
                    });
                }
            })
            
        }
        function SetFlagJob(jobID) {
            var confirmMsg = "";
            if ($(".aFlag" + jobID + " i").hasClass("fa-flag-o")) {
                confirmMsg = "Are your sure to add this job to your flag list?";
            }
            else {
                confirmMsg = "Are your sure to remove this job from your flag list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($(".aFlag" + jobID + " i").hasClass("fa-flag-o")) {
                        $(".aFlag" + jobID).hide();
                        bootbox.alert("Job added to your flag list");
                        $(".aFlag" + jobID + " i").removeClass("fa fa-flag-o");
                        $(".aFlag" + jobID + " i").addClass("fa fa-flag");
                    }
                    else {
                        $(".aFlag" + jobID + " i").removeClass("fa fa-flag");
                        $(".aFlag" + jobID + " i").addClass("fa fa-flag-o");
                        bootbox.alert("Job removed from your flag list");
                    }
                    var reason = $("#txtReason").val();
                    $.ajax({
                        url: '/searchjobresult.aspx/SetFlagJob',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "jobID":"' + jobID + '","reason":"' + reason + '"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }
    </script>
</asp:Content>
