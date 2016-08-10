<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserJobs.aspx.cs" Inherits="ReferLocals.UserJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-md-9">
            <div class="search_h1 compressedh1">
                <h1>Jobs Posted by <strong>
                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                </strong></h1>
            </div>
            <div id="divJobs">
            </div>
            <div id="divShowMoreJobs" class="table_reqBtn">
                <a class="btn blue btn-outline" onclick="ShowMoreUserJobs();">SHOW MORE</a>
            </div>
        </div>
        <div class="col-md-3">
            <div class="advertise_bg">
                <div class="Add_advertise">
                    <div class="videolink">
                        <iframe width="100%" height="185" src="https://www.youtube.com/embed/JSKm7homfxw" frameborder="0" allowfullscreen></iframe>
                    </div>
                </div>
                <div class="Add_advertise">
                    <a href="#">
                        <img src="img/advetise_img1.jpg" alt="advertise image">
                    </a>
                </div>
                <div class="Add_advertise Add_advertise_d"><a href="#" class="down_advertiseLink">Text me the Link  </a></div>
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
                                        <div id="divEmailBox" style="display: none; height: 110px;" class="input_pop_field">
                                            <input id="txtEmail" type="text" name="lname" placeholder="Email" class="p_inputPopup1"><br />
                                            <br />
                                            <a class="submitPopupBtn" onclick="SendEmailForJobLink(0,0);">Submit</a>
                                        </div>
                                        <a onclick="ShowDivTextBox(0)" class="popup_share t_popupshare">
                                            <img src="/images/a4.png" alt="">
                                        </a>

                                        <div id="divTextBox" style="display: none; height: 110px; width: 100%;" class="input_pop_field">
                                            <div style="width: 212px; margin: 0 auto; text-align: center">
                                                <asp:DropDownList ID="drpCountryCode" ClientIDMode="Static" class="form-control form-control-solid placeholder-no-fix" Style="height: 40px; width: 33%; float: left;" runat="server">
                                                </asp:DropDownList>
                                                <input id="txtPhone" style="float: left; width: 65%; margin-left: 1%;" type="text" onkeypress="return isNumberKey(event)" maxlength="16" name="lname" placeholder="Phone" class="p_inputPopup1"><br />
                                                <br />
                                                <br />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script id="jobTemplate" type="text/html">
        <div class="searchJobCard">

            <div class="linktextsearch">
                <div class="flag_tooltip jobdone1">
                    {{if LoggedInUserID>0}}
                    
                        {{if IsFlag==false}}
                        <a id="aFlag${Id}"><i id="iFlag${Id}" onclick="OpenFlagJobModal(${Id})" class="fa fa-flag-o"></i></a><span class="tooltipflag">Flag</span>
                        {{/if}}
                    {{else}}
                    <a id="aFlag${Id}"><i id="iFlag${Id}" onclick="ShowLoginAlert('Please login to flag this job')" class="fa fa-flag-o"></i></a><span class="tooltipflag">Flag</span>
                    {{/if}}
                </div>
                <div class="search_tooltip jobdone1">
                     {{if LoggedInUserID>0}}
                        {{if IsFavorite}}
                        <a id="aFavorite${Id}">
                            <i id="iFavorite${Id}" onclick="SetFavoriteJob(${Id});" class="fa fa-heart"></i>
                        </a><span class="tooltipsearch">Follow</span>
                        {{else}}
                        <a id="aFavorite${Id}">
                            <i id="iFavorite${Id}" onclick="SetFavoriteJob(${Id});" class="fa fa-heart-o"></i>
                        </a><span class="tooltipsearch">Follow</span>
                        {{/if}}
                    {{else}}
                     <a id="aFavorite${Id}">
                        <i id="iFavorite${Id}" onclick="ShowLoginAlert('Please login to favorite this job')" class="fa fa-heart-o"></i>
                    </a><span class="tooltipsearch">Follow</span>
                    {{/if}}
                </div>

                <div class="jobdone">
                </div>
            </div>

            <div class="col-md-4">
                <div class="searchcardImage">
                    <a href="#">
                        <img src="${Image}">
                    </a>
                </div>
                <%--<div>Posted By <a href="/user/profile/${UserID}">${UserName}</a></div>--%>
            </div>

            <div class="col-md-8">
                <div class="fieldSearch">

                    <h3 class="searchfsttext"><a href="${JobUrl}">${Title} </a></h3>

                    <div class="col-md-6" style="margin: 13px 0px 0px;">
                        <h4>Posted On ${CreatedDateString}</h4>
                        <h4>${CityName} ${ZipName}</h4>
                        <p class="jobSearch_text2">${Description}</p>
                    </div>

                    <div class="col-md-6">
                        <a href="${JobUrl}" class="ancherback">Send Estimate</a>
                        {{if LoggedInUserID>0}}
                        <a id="aShareThisJob" class="ancherback" onclick="ShowShareModal(${Id},'${FormattedTitle}');" data-toggle="modal">Share This Job </a>
                        {{else}}
                        <a id="aShareThisJob" class="ancherback" onclick="ShowLoginAlert('Please login to share this job');" data-toggle="modal">Share This Job </a>
                        {{/if}}
                        <a href="${JobUrl}" class="ancherback">View Job Details </a>
                    </div>

                </div>


            </div>
        </div>

    </script>
    <script>

    </script>
    <script src="/js/shareFromList.js" type="text/javascript"></script>
    <script type="text/javascript">
        var selectedJobID = 0;
       
        function OpenFlagJobModal(jobID)
        {
            $("#divFlagModal").modal("show");
            selectedJobID =jobID;
        }
        function FlagJob()
        {
            SetFlagJob(selectedJobID);
        }
        function SetFlagJob(jobID) {
            
            var confirmMsg = "";
            if ($("#iFlag" + jobID).hasClass("fa-flag-o")) {
                confirmMsg = "Are your sure to add this job to your flag list?";
            }
            else {
                confirmMsg = "Are your sure to remove this job from your flag list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($("#iFlag" + jobID).hasClass("fa-flag-o")) {
                        $("#iFlag" + jobID).hide();
                        bootbox.alert("Job added to your flag list");
                        $("#iFlag" + jobID).removeClass("fa fa-flag-o");
                        $("#iFlag" + jobID).addClass("fa fa-flag");
                    }
                    else {
                        $("#iFlag" + jobID).removeClass("fa fa-flag");
                        $("#iFlag" + jobID).addClass("fa fa-flag-o");
                        bootbox.alert("Job removed from your flag list");
                    }

                    var reason= $("#txtReason").val();
                    $.ajax({
                        url: '/searchjobresult.aspx/SetFlagJob',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "jobID":"' + jobID + '","reason":"'+reason+'"}',
                        success: function (data) {

                        }
                    });
                }
            })

        }
        function SetFavoriteJob(jobID) {
            var confirmMsg = "";
            if ($("#iFavorite" + jobID).hasClass("fa-heart-o")) {
                confirmMsg = "Are your sure to add this job to your favorite list?";
            }
            else {
                confirmMsg = "Are your sure to remove this job from your favorite list?";
            }
            bootbox.confirm(confirmMsg, function (isconfirmed) {
                if (isconfirmed) {
                    if ($("#iFavorite" + jobID).hasClass("fa-heart-o")) {
                        bootbox.alert("Job added to your favorite list");
                        $("#iFavorite" + jobID).removeClass("fa fa-heart-o");
                        $("#iFavorite" + jobID).addClass("fa fa-heart");
                    }
                    else {
                        $("#iFavorite" + jobID).removeClass("fa fa-heart");
                        $("#iFavorite" + jobID).addClass("fa fa-heart-o");
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
        var pageIndexUserJobs = 0;
        function ShowMoreUserJobs() {
            pageIndexUserJobs++;
            GetUserJobs(pageIndexUserJobs);
        }
        $(document).ready(function () {
            //ShowReferList('No');
            GetUserJobs(0);
        });

        var pageSize = 10;
        var index = 0;

        function GetUserJobs(pageIndex) {
            if (pageIndex == 0) {
                pageIndexUserReferals = 0;
            }
            $("#divShowMoreJobs").hide();
            if ($("#divJobs").html() == "") {
                pageIndex = 0;
                pageIndexUserReferals = 0;
            }

            $.ajax({
                url: '/UserJobs.aspx/GetUserJobs',
                type: "POST",
                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    if (result.d.JobCount > 0) {
                        if (pageIndex == 0) {
                            $("#divJobs").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreJobs").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreJobs").show();
                        }

                        $("#jobTemplate").tmpl(result.d.Jobs).appendTo("#divJobs");

                    }
                    else {
                        $("#divJobs").html("No records found");
                    }

                }
            });
        }

    </script>
</asp:Content>
