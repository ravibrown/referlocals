<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="ReferLocals.UserDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <!-- Always force latest IE rendering engine or request Chrome Frame -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile-2.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (userData != null)
        {%>
    <div class="requestProfile vc_profile">
        <div class="container">
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="row">
                                <div class="col-md-3">
                                    <ul class="list-unstyled profile-nav">
                                        <li>
                                            <img alt="" class="img-responsive pic-bordered" src="<%=!string.IsNullOrEmpty(userData.Image)?DataAccess.HelperClasses.Common.UserImagesPath+userData.Image:DataAccess.HelperClasses.Common.NoImageIcon%>?width=263&height=275&scale=both" />
                                            <a class="profile-edit" href="/ProfileImage">edit </a></li>
                                        <li><a onclick="ShowJobs();">Jobs <span><%=userData.JobsPosted+userData.JobsDone+userData.JobsCancelled %> </span></a></li>
                                        <li><a onclick="ShowReferals();">Refferals <span id="spanReferalCount"></span></a></li>
                                        <%--<li><a onclick="ShowAppointments();">My Appointments<span id="spanAppointmentRequestCount"></span></a></li>--%>
                                        <li><a onclick="ShowAppointmentRequests();">Appointment Requests<span id="spanAppointmentRequestCount" runat="server" clientidmode="Static"></span></a></li>
                                        <li><a onclick="ShowAppointments();">Appointments<span id="spanAppointmentCount" runat="server" clientidmode="Static"></span></a></li>

                                        <%--<li><a href="user_appointment.html">My Appointments </a></li>--%>
                                    </ul>
                                </div>

                                <div class="col-md-9">

                                    <div class="row">
                                        <div class="col-md-8 profile-info">
                                            <div class="fieldSearch professionalProfile_req">
                                                <h3 class="searchfsttext"><a href="/profile"><%=userData.FirstName+" "+userData.LastName %> <span class="req_srch">Edit </span></a></h3>
                                                <div class="address_search address_search_v1">

                                                    <p><i class="fa fa-at"></i><a href="#"><%=userData.Email %> </a></p>
                                                    <p><i class="fa fa-phone"></i><%=userData.CountryCode+"-"+userData.PhoneNumber %></p>
                                                    <p class="searchBold"><i class="fa fa-map-marker"></i><%=userData.Appartment+" "+userData.StreetAddress%></p>
                                                    <p class="searchBold"><i class="fa fa-map-marker"></i><%=userData.CityName %></p>

                                                </div>
                                            </div>
                                        </div>
                                        <!--end col-md-8-->
                                        <div class="col-md-4">
                                            <div class="ulAncher_dasdboard">
                                                <h2 class="ancher_h2dashboard">User Summary </h2>
                                                <ul>
                                                    <li><a href="#">Job Posted <span><%=userData.JobsPosted%> </span></a></li>
                                                    <li><a href="#">Job Done <span><%=userData.JobsDone%></span></a></li>
                                                    <li><a href="#">Job Cancelled<span><%=userData.JobsCancelled%></span></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <!--end col-md-4-->
                                    </div>


                                    <!--end row-->
                                    <div id="divJobsContainer" class="tabbable-line tabbable-custom-profile vsprofiletable5">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_1_11" onclick="GetUserJobsOpen(0)" data-toggle="tab">Open </a></li>
                                            <li><a href="#tab_1_22" onclick="GetUserJobsDone(0)" data-toggle="tab">Done </a></li>
                                            <li><a href="#tab_1_23" onclick="GetUserJobsCancelled(0)" data-toggle="tab">Cancelled </a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_1_11">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Job List <a class="btn btn-sm grey-salsa btn-outline" href="/addnewjob">Add New job </a></h2>
                                                            <div id="divJobListOpen" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="table_reqBtn" id="divShowMore" style="display: none;">
                                                        <a type="button" class="btn blue btn-outline" onclick="ShowMoreJobsOpen();">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>





                                            <div class="tab-pane" id="tab_1_22">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Job List <a class="btn btn-sm grey-salsa btn-outline" href="#">Add New job </a></h2>
                                                            <div id="divJobListDone" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreDone" style="display: none;" class="table_reqBtn">
                                                        <a type="button" onclick="ShowMoreJobsDone();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--tab-pane-->


                                            <div class="tab-pane" id="tab_1_23">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <div class="dash_v">
                                                            <h2 class="list_dashboard_job">Job List <a class="btn btn-sm grey-salsa btn-outline" href="/addnewjob">Add New job </a></h2>
                                                            <div id="divJobListCancelled" class="deah_board_full1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divShowMoreCancelled" class="table_reqBtn" style="display: none;">
                                                        <a type="button" onclick="ShowMoreJobsCancelled();" class="btn blue btn-outline">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>




                                            <!--tab-pane-->
                                        </div>
                                    </div>

                                    <div id="divReferalsContainer" style="display: none;" class="tabbable-line tabbable-custom-profile">
                                        <div class="tab-content">
                                            <div class="tab-pane active">
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-advance table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Professinal Name</th>
                                                                <th>Location</th>
                                                                <th>Reffered</th>
                                                                <th>Satisfied</th>
                                                                <th>Comments</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="divReferalList">
                                                        </tbody>
                                                    </table>
                                                    <div id="divShowMoreReferals" class="table_reqBtn">
                                                        <a class="btn blue btn-outline" onclick="ShowMoreUserReferals();" type="button">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--tab-pane-->

                                            <!--tab-pane-->
                                        </div>
                                    </div>
                                    <div id="divAppointmentContainer" style="display: none;" class="tabbable-line tabbable-custom-profile vsprofiletable5">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_AppointmentRequest" data-toggle="tab">Appointment Requests </a></li>
                                            <li><a id="a_tabMyAppointments" href="#tab_MyAppointments" data-toggle="tab">My Appointments </a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_AppointmentRequest">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <table class="table table-striped table-bordered table-advance table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>Professional </th>
                                                                    <th>Job Title </th>
                                                                    <th>Estimate </th>
                                                                    <th>Message </th>
                                                                    <th>Requested Dates </th>
                                                                </tr>
                                                            </thead>

                                                            <tbody id="divAppointmentRequest">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div id="divShowMoreAppointmentRequests" class="table_reqBtn">
                                                        <a class="btn blue btn-outline" onclick="ShowMoreAppointmentRequests();" type="button">SHOW MORE</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divYourRequestedDates" class="modal fade estimateFade estimateFade_v step_fade" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog1">
                                                    <div class="modal-content">
                                                        <div class="modal-header backgroundtitle backgroundtitle_v">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                            <h4 id="h4RequestDates" class="modal-title">You Have Requested These Dates for Appointment</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="scroller" style="height: 250px" data-always-visible="1" data-rail-visible1="1">
                                                                <div class="row">
                                                                    <div id="divYourRequestedDatesData" class="col-md-12">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--tab-pane-->
                                            <div class="tab-pane" id="tab_MyAppointments">
                                                <div class="tabbable-line tabbable-custom-profile">

                                                    <div class="tab-content" style="border: medium none; padding: 0px;">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="portlet light portlet-fit bordered calendar">
                                                                    <div class="portlet-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3 col-sm-12">

                                                                                <div class="draggable_ancher">
                                                                                    <h2>Today's Appointment  </h2>

                                                                                    <div id="divTodayAppointments" class="list_appointment">
                                                                                    </div>
                                                                                </div>

                                                                                <!-- END DRAGGABLE EVENTS PORTLET-->
                                                                            </div>
                                                                            <div class="col-md-9 col-sm-12" style="position: relative;">
                                                                                <div id="calendar" class="has-toolbar">
                                                                                </div>
                                                                                <div id="divCalendarLoader" style="display: none; position: absolute; vertical-align: middle; top: 28%; left: 41%; z-index: 99;">
                                                                                    <img src="/images/default.gif" alt="loader" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--tab-pane-->


                                                    </div>
                                                </div>


                                            </div>
                                            <!--tab-pane-->
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

    <%}
        else
        {%>
    <div class="requestProfile vc_profile">
        <div class="container">
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="row">
                                <h3 style="color: #0094ff"><a href="/Login">Please login to view your dashboard</a></h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="/js/moment.min.js" type="text/javascript"></script>
    <script src="/js/fullcalendar.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <%--<script src="/js/jquery-ui.min.js" type="text/javascript"></script>--%>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->

    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <%--<script src="/js/calendar.min.js" type="text/javascript"></script>--%>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->

    <%--<script src="/js/quick-sidebar.min.js" type="text/javascript"></script>--%>
    <!-- END THEME LAYOUT SCRIPTS -->
    <script id="YourRequestedDatesTemplate" type="text/html">
        {{tmpl(Dates,{scb:RescheduledByProfessional}) "#YourRequestedDatesDataTemplate"}}
         <div class="form-group groupestimate">
             <a href="/appointmentform?appointmentID=${ID}&quoteID=${QuoteID}&RescheduledByUser=true" class="btn green" type="button">Reschedule</a>
         </div>
    </script>
    <script id="YourRequestedDatesDataTemplate" type="text/html">
        <div class="width_appointment">
            <div class="devidePart_text">
                <div class="devide_2text">
                    <p>${AppointmentDate}</p>
                </div>
                <div class="devide_2text">
                    <p>${AppointmentTime} </p>
                </div>
                {{if  $item.scb==true}}
                <div class="devide_2text">
                    <a onclick="AcceptAppointmentDate(${ID})" class="btn blue" type="button">Accept</a>
                </div>
                {{/if}}
            </div>
        </div>


    </script>
    <script id="AppointmentRequestListTemplate" type="text/html">
        <tr>
            <td><a href="/referdetail?Id=${ProfessionalUniqueID}">${ProfessionalName} </a></td>
            <td>${JobTitle}</td>
            <td>$${Estimate}</td>
            <td class="stepwidth">${Comments}</td>

            <td style="text-align: center">{{if RescheduledByProfessional==false}}
                  <a class="btn btn-sm grey-salsa btn-outline" onclick="GetRequestedAppointmentDates(${AppointmentID},true,'${ProfessionalName}')" data-toggle="modal">Your Requested Dates  </a>

                {{else}}
                  <a class="btn btn-sm grey-salsa btn-outline" onclick="GetRequestedAppointmentDates(${AppointmentID},false,'${ProfessionalName}')" data-toggle="modal">View Requested Dates  </a>
                {{/if}}
            </td>
        </tr>

    </script>
    <script id="referalListTemplate" type="text/html">
        <tr>
            <td>${ProfessionalName}</td>
            <td>${CityName}</td>
            <td>${CreatedDateString}</td>
            <td>{{if IsSatisfied}} Yes {{else}}No{{/if}}</td>
            <td class="ppr_width">${Comment}</td>

        </tr>
    </script>
    <script id="jobListTemplate" type="text/html">
        <div id="divJobOpen${Id}" class="desh_board1">
            
            
                {{if JobStatus==1||JobStatus==3}}
            <h3 class="dashboard_H3" >Posted on ${CreatedDateString}  <a href="/addnewjob?ID=${Id}">Edit Job </a></h3>
                {{else}}
                 <h3 class="dashboard_H3" >Posted on ${CreatedDateString}  </h3>
             {{/if}}
            <div class="col-md-2 textDah_board">
                {{if Image!=""&&Image!=null}}
                
                              <a href="/jobdetail/${Id}/${UrlFriendlyTitle}">
                                  <img src="<%=DataAccess.HelperClasses.Common.JobImagesPath%>${Image}?width=111&height=111&scale=both&mode=crop" alt="${Image}" /></a>
                {{else}}
                               <a href="/jobdetail/${Id}/${UrlFriendlyTitle}">
                                   <img src="<%=DataAccess.HelperClasses.Common.JobDefaultImage%>?width=111&height=111&scale=both&mode=crop" alt="NO Image" /></a>
                {{/if}}
            </div>
            <div class="col-md-5 textDah_board1">
                <h2 class="leakage" style="float: left"><a href="/jobdetail/${Id}/${UrlFriendlyTitle}">${Title}</a> </h2>
                <p>
                    Professional Needed: ${SubCategoryName}
                    
                </p>
                <p>${Address}</p>
                <p>${CityName}</p>
            </div>
            <div class="col-md-5 textDah_board3">
                {{if JobStatus==1}}
                <div class="ancher_desh"><a href="/jobquotes/${Id}/${UrlFriendlyTitle}">${JobQuoteCount} <span>View Quote </span></a></div>
                {{/if}}
                <div class="deshboard_on">
                    {{if JobStatus==1}}
                    <input type="checkbox" data-jobid="${Id}" checked class="make-switch switchOpenJob bootstrapSwitch" <%--onchange="confirm('Are you sure?')?ChangeJobStatus(${Id},3):DonotChangeState(this,true);"--%> data-size="small">
                    <%--{{else JobStatus=2}}
                    <input type="checkbox" data-jobid="${Id}" class="make-switch switchDoneJob bootstrapSwitch" data-size="small" />--%>
                    {{else JobStatus==3}}
                    <input type="checkbox" data-jobid="${Id}" class="make-switch switchCancelJob bootstrapSwitch" <%--onchange="confirm('Are you sure?')?ChangeJobStatus(${Id},1):DonotChangeState(this,false);"--%> data-size="small">
                    {{/if}}
                </div>
            </div>
        </div>
    </script>
    <script id="todayAppointmentsTemplate" type="text/html">
        <a href="#responsive1" data-toggle="modal">${AppointmentTime} at ${JobLocation} </a>
    </script>
    <script type="text/javascript">
        var month = 0;
        var calendarMonth = new Date().getMonth();
        var gotodate = new Date();

        function GetAppointments() {
            $.ajax({
                url: '/UserDashboard.aspx/GetAppointments',
                type: "POST",
                data: '{"dateType":"1","pageIndex":"0","pageSize":"10000","month":"0"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != null && result.d.DatesWithJob.length > 0) {
                        var data = result.d.DatesWithJob;
                        var eventsArrary = new Array();
                        $.each(data, function (i, v) {
                            eventsArrary.push({
                                title: v.JobTitle,
                                start: v.AppointmentDate
                            });
                        })

                        $("#divTodayAppointments").empty();
                        $("#todayAppointmentsTemplate").tmpl(result.d.DatesWithJob).appendTo("#divTodayAppointments");

                    }
                    else {
                        $("#divTodayAppointments").html("No Records Found");
                    }
                }
            });
        }

        function GetAppointmentsForCalendar() {
            $("#divCalendarLoader").show();
            $.ajax({
                url: '/UserDashboard.aspx/GetAppointments',
                type: "POST",
                data: '{"dateType":"3","pageIndex":"0","pageSize":"10000","month":"' + (calendarMonth + 1) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $("#divCalendarLoader").hide();
                    if (result.d != null) {
                        var data = result.d.DatesWithJob;
                        var eventsArrary = new Array();
                        $.each(data, function (i, v) {
                            eventsArrary.push({
                                title: v.JobTitle,
                                start: v.AppointmentDate,
                                url: '/jobdetail/' + v.JobID + '/' + v.UrlFriendlyJobTitle + ''
                            });
                        })

                        $('#calendar').fullCalendar('destroy');

                        $('#calendar').fullCalendar({
                            defaultDate: gotodate,
                            header: {
                                left: 'prev,next',
                                center: 'title',
                                right: 'month'
                            },
                            overlap: false,
                            editable: false,
                            eventLimit: true, // allow "more" link when too many events
                            events: eventsArrary,
                        });
                        $('.fc-next-button').on("click", function () {

                            calendarMonth = (gotodate.getMonth()) + 1;
                            gotodate.setMonth(calendarMonth);
                            GetAppointmentsForCalendar();
                        });


                        $('.fc-prev-button').on("click", function () {

                            calendarMonth = (gotodate.getMonth()) - 1;
                            gotodate.setMonth(calendarMonth);
                            GetAppointmentsForCalendar();

                        });

                    }
                }
            });
        }
        function callback(data) {
            var eventsArrary = new Array();
            $.each(data, function (i, v) {
                eventsArrary.push({
                    title: v.JobTitle,
                    start: v.AppointmentDate
                });
            })
            return eventsArrary;
        }
        function LoadCalendar() {
            var current_url = '';
            var new_url = '';
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month'
                },
                overlap: false,
                //defaultDate: '2016-06-06',
                events: function (start, end, callback) {

                    //var year = end.getFullYear();

                    new_url = 'https://www.referlocals.com/webapi/api/Appointment/GetAppointmentsForCalendar?userType=3&userID=31&dateType=3&pageIndex=0&pageSize=1000&month=' + month + '';

                    //new_url  = '/api/user/events/list/' + id + '/year/' + year;
                    //$.getJSON(new_url);
                    if (new_url != current_url) {

                        $.ajax({
                            url: new_url,
                            jsonp: "callback",
                            data: '',
                            dataType: 'jsonp',
                            type: 'POST',
                            success: function (response) {

                                current_url = new_url;
                                user_events = response.d;

                                callback(response);
                            }
                        })
                    } else {
                        callback(user_events);
                    }
                },

                //eventSources: {
                //    url: 'https://www.referlocals.com/webap/api/Appointment/GetAppointmentsForCalendar?userType=3&userID=31&dateType=3&pageIndex=0&pageSize=1000&month=' + month + '',

                //},
                editable: false,
                eventLimit: true, // allow "more" link when too many events
                //events: eventsArrary,
                viewRender: function (view, element) {
                    month = view.intervalStart._d.getMonth() + 1
                    //GetAppointmentsForCalendar();
                }
            });
        }
        function AcceptAppointmentDate(appointmentDateID) {

            $.ajax({
                url: '/UserDashboard.aspx/AcceptAppointmentDate',
                type: "POST",
                data: '{"appointmentDateID":"' + appointmentDateID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d > 0) {
                        $("#divYourRequestedDatesData").html("<h4 style='font-weight: bold;text-align: center;'>Your appointment has been set</h4>");
                        setTimeout(function () { window.location.href = window.location.href }, 3000);
                    }
                }
            })
        }
        function GetRequestedAppointmentDates(appointmentID, IsDatesByMe, userName) {
            $("#divYourRequestedDates").modal();
            $.ajax({
                url: '/UserDashboard.aspx/GetRequestedDatesByAppointmentID',
                type: "POST",
                data: '{"appointmentID":"' + appointmentID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != null) {
                        $("#divYourRequestedDatesData").empty();
                        if (IsDatesByMe) {
                            $("#h4RequestDates").html("You Have Requested These Dates for Appointment");

                        }
                        else {
                            $("#h4RequestDates").html(userName + " Has Requested These Dates for Appointment");

                        }
                        $("#YourRequestedDatesTemplate").tmpl(result.d).appendTo("#divYourRequestedDatesData");

                    }
                }
            });
        }
        function DonotChangeState(checkbox, OnState) {
            $(this).bootstrapSwitch('state', OnState, true);
            return false;

        }
        var jobStatusSelected = 1;
        var pageIndexJobsOpen = 0;
        var pageIndexJobsDone = 0;
        var pageIndexJobsCancelled = 0;
        var pageIndexUserReferals = 0;
        var pageIndexAppointmentRequests = 0;
        var pageSize = 10;
        $(function () {

            GetUserJobsOpen(0);
            GetUserJobsDone(0);
            GetUserJobsCancelled(0);
            GetUserReferals(0);
            GetAppointmentRequests(0);
        })
        function ShowJobs() { $("#divJobsContainer").show(); $("#divReferalsContainer").hide(); $("#divAppointmentContainer").hide(); }
        function ShowReferals() {
            $("#divJobsContainer").hide(); $("#divReferalsContainer").show();
            $("#divAppointmentContainer").hide();
        }
        function ShowAppointments() {
            $("#divJobsContainer").hide();
            $("#divReferalsContainer").hide();
            $("#divAppointmentContainer").show();
            activeTab("tab_MyAppointments");
        }
        function activeTab(tab) {
            $('.nav-tabs a[href="#' + tab + '"]').tab('show');
        };
        function ShowAppointmentRequests() {
            $("#divJobsContainer").hide();
            $("#divReferalsContainer").hide();
            $("#divAppointmentContainer").show();
            activeTab("tab_AppointmentRequest");
        }

        function ChangeJobStatus(jobID, jobStatus) {

            $("#divJobOpen" + jobID).remove();
            $.ajax({
                url: '/UserDashboard.aspx/ChangeJobStatus',
                type: "POST",

                data: '{"jobStatus":"' + jobStatus + '","jobID":"' + jobID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) { },
                error: function (result) {
                    $("#divJobOpen" + jobID).show();
                }
            })
        }

        function GetUserReferals(pageIndex) {

            $("#divShowMoreReferals").hide();
            if ($("#divReferalList").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/UserDashboard.aspx/GetReferalsByUser',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
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
        function GetUserJobsOpen(pageIndex) {

            $("#divShowMore").hide();
            if ($("#divJobListOpen").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/UserDashboard.aspx/GetUserJobs',
                type: "POST",

                data: '{"jobStatus":"1","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.JobCount > 0) {
                        if (pageIndex == 0) {
                            $("#divJobListOpen").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMore").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMore").show();
                        }

                        $("#jobListTemplate").tmpl(result.d.Jobs).appendTo("#divJobListOpen");
                        $(".make-switch").bootstrapSwitch();


                        $('.switchDoneJob').on('switchChange.bootstrapSwitch', function (event, state) {

                            var $chk = $(this);
                            bootbox.confirm('Are you sure, Open this job again?', function (confirmResult) {
                                if (confirmResult) {
                                    ChangeJobStatus($chk.data("jobid"), 1);
                                }
                                else {

                                    $chk.bootstrapSwitch('state', !state, true)
                                }
                            });

                        });

                        $('.switchOpenJob').on('switchChange.bootstrapSwitch', function (event, state) {

                            var $chk = $(this);
                            bootbox.confirm('Are you sure, Cancel this job?', function (confirmResult) {
                                if (confirmResult) {
                                    ChangeJobStatus($chk.data("jobid"), 3);
                                }
                                else {

                                    $chk.bootstrapSwitch('state', !state, true)
                                }
                            });

                        });

                    }
                    else {
                        $("#divJobListOpen").html("No records found");
                    }

                }
            });
        }

        function GetUserJobsDone(pageIndex) {
            $("#divShowMoreDone").hide();
            if ($("#divJobListDone").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/UserDashboard.aspx/GetUserJobs',
                type: "POST",

                data: '{"jobStatus":"2","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.JobCount > 0) {
                        if (pageIndex == 0) {
                            $("#divJobListDone").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreDone").hide();
                        }
                        else {
                            //pageIndexJobsDone++;
                            $("#divShowMoreDone").show();
                        }

                        $("#jobListTemplate").tmpl(result.d.Jobs).appendTo("#divJobListDone");
                        $(".make-switch").bootstrapSwitch();
                        $('.switchDoneJob').on('switchChange.bootstrapSwitch', function (event, state) {

                            var $chk = $(this);
                            bootbox.confirm('Are you sure, Open this job again?', function (confirmResult) {
                                if (confirmResult) {
                                    ChangeJobStatus($chk.data("jobid"), 1);
                                }
                                else {

                                    $chk.bootstrapSwitch('state', !state, true)
                                }
                            });

                        });
                    }
                    else {
                        $("#divJobListDone").html("No records found");
                    }

                }
            });
        }
        function GetUserJobsCancelled(pageIndex) {

            $("#divShowMoreCancelled").hide();
            if ($("#divJobListCancelled").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/UserDashboard.aspx/GetUserJobs',
                type: "POST",

                data: '{"jobStatus":"3","pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.JobCount > 0) {
                        if (pageIndex == 0) {
                            $("#divJobListCancelled").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreCancelled").hide();
                        }
                        else {
                            // pageIndexJobsCancelled++;
                            $("#divShowMoreCancelled").show();
                        }

                        $("#jobListTemplate").tmpl(result.d.Jobs).appendTo("#divJobListCancelled");
                        $(".make-switch").bootstrapSwitch();
                        $('.switchCancelJob').on('switchChange.bootstrapSwitch', function (event, state) {
                            var $chk = $(this);

                            bootbox.confirm('Are you sure, Activate this job?', function (confirmResult) {
                                if (confirmResult) {
                                    ChangeJobStatus($chk.data("jobid"), 1);
                                }
                                else {

                                    $chk.bootstrapSwitch('state', !state, true)
                                }
                            });

                            //if (confirm('Are you sure, Cancel this job?')) {
                            //    ChangeJobStatus($(this).data("jobid"), 1);
                            //}
                            //else {

                            //    $(this).bootstrapSwitch('state', !state, true)
                            //}

                        });
                    }
                    else {
                        $("#divJobListCancelled").html("No records found");
                    }

                }
            });
        }
        function ShowMoreUserReferals() {
            pageIndexUserReferals++;
            GetUserReferals(pageIndexUserReferals);
        }
        function ShowMoreAppointmentRequests() {
            pageIndexAppointmentRequests++;
            GetAppointmentRequests(pageIndexAppointmentRequests);
        }
        function ShowMoreJobsOpen() {
            pageIndexJobsOpen++;
            GetUserJobsOpen(pageIndexJobsOpen);
        }
        function ShowMoreJobsDone() {
            pageIndexJobsDone++;
            GetUserJobsDone(pageIndexJobsDone);
        }
        function ShowMoreJobsCancelled() {
            pageIndexJobsCancelled++;
            GetUserJobsCancelled(pageIndexJobsCancelled);
        }

        function GetAppointmentRequests(pageIndex) {

            $("#divShowMoreAppointmentRequests").hide();
            if ($("#divAppointmentRequest").html() == "") {
                pageIndex = 0;
            }
            $.ajax({
                url: '/UserDashboard.aspx/GetAppointmentRequests',
                type: "POST",

                data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $("#spanAppointmentRequestCount").html(result.d.QuoteCount);
                    if (result.d.QuoteCount > 0) {
                        if (pageIndex == 0) {
                            $("#divAppointmentRequest").empty();
                        }
                        if (result.d.HideShowMore) {
                            $("#divShowMoreAppointmentRequests").hide();
                        }
                        else {
                            //pageIndexJobsOpen++;
                            $("#divShowMoreAppointmentRequests").show();
                        }

                        $("#AppointmentRequestListTemplate").tmpl(result.d.QuoteList).appendTo("#divAppointmentRequest");

                    }
                    else {
                        $("#divAppointmentRequest").html("No records found");
                    }

                }
            });
        }
    </script>



    <script type="text/javascript">

        $(function () {
            //$('#calendar').fullCalendar({
            //    // put your options and callbacks here
            //})
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                if (e.target.id = "tabMyAppointments") {
                    // $('#calendar').fullCalendar('today');
                    GetAppointments();
                    //LoadCalendar();
                    GetAppointmentsForCalendar();
                }// newly activated tab

            })
        })

    </script>

</asp:Content>
