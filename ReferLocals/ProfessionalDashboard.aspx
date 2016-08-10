<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProfessionalDashboard.aspx.cs" Inherits="ReferLocals.ProfessionalDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile-2.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <style>
        .bootstrap-tagsinput {
            width: 100%;
        }

        #div_CitiesIServeSearchContent {
            max-height: 235px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (userData != null)
        {%>
    <div class="requestProfile">
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
                                            <a href="/ProfileImage" class="profile-edit">edit </a></li>

                                        <li><a onclick="ShowReferals();">Referals <span><%=userData.ReferalCount %> </span></a></li>
                                        <li><a onclick="ShowEstimates();">Estimates<span id="spanEstimateCount"></span></a></li>
                                        <li><a onclick="ShowAppointmentRequests();">Appointment Requests<span id="spanAppointmentRequestCount" runat="server" clientidmode="Static"></span></a></li>
                                        <li><a onclick="ShowAppointments();">Appointments<span id="spanAppointmentCount" runat="server" clientidmode="Static"></span></a></li>
                                        <%--<li><a href="professional_dashboard_dropdown.html">Estimates  <span>8 </span></a></li>
                                        <li><a href="profesionals_profile_appointments_requests.html">Appointments </a></li>
                                        <li><a href="javascript:;">Messages </a></li>--%>
                                    </ul>
                                </div>
                                <div class="col-md-9">
                                    <div class="row">
                                        <div class="col-md-8 profile-info">


                                            <div class="fieldSearch professionalProfile_req">

                                                <h3 class="searchfsttext"><%=userData.FirstName+" "+userData.LastName %> <a class="req_srch" href="/profile">Edit </a></h3>

                                                <div class="address_search">
                                                    <p><i class="fa fa-at"></i><a href="#"><%=userData.Email %> </a></p>
                                                    <p><i class="fa fa-phone"></i><%=userData.CountryCode+"-"+userData.PhoneNumber %></p>
                                                    <p class="searchBold"><i class="fa fa-briefcase"></i><%=userData.CompanyName %> </p>
                                                    <p class="searchBold"><i class="fa fa-globe"></i><%=userData.Website %> </p>
                                                    <%if (!string.IsNullOrEmpty(userData.Appartment) || !string.IsNullOrEmpty(userData.StreetAddress))
                                                        { %>
                                                    <p class="searchBold"><i class="fa fa-map-marker"></i><%=userData.Appartment+" "+userData.StreetAddress%></p>
                                                    <%}%>
                                                    <p class="searchBold"><i class="fa fa-map-marker"></i><%=userData.CityName %></p>

                                                    <div style="overflow: hidden; display: block;">
                                                        <div class="bigtextlink" style="float: left; width: 21%; clear: both; margin-top: 0px; margin-bottom: 0px;">
                                                            refer me Url: 
                                                        </div>
                                                        <div class="bigtextlink" style="float: left; clear: both; margin-top: 0px;">
                                                            <%foreach (var url in userData.ProfessionalUrls)
                                                                {%>
                                                            <a href="http://<%=Request.Url.Host %>/<%=url.SubCategoryName%>/<%=userData.ProfileUrl %>/referme" style="width: 100%; display: block; margin: 0px;"><%=Request.Url.Host %>/<%=url.SubCategoryName%>/<%=userData.ProfileUrl %>/referme </a>
                                                            <%}%>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>



                                        </div>
                                        <!--end col-md-8-->
                                        <div class="col-md-4">

                                            <div class="form-group profesional_formgroup">
                                                <label class="control_label_md3">Cities Serving</label>

                                                <input type="text" value="" id="object_tagsinput" style="width: 100%;">
                                                <!-- <div class="margin-top-10"> <input type="text" class="form-control input-large reqLarge" placeholder="Add City" id="object_tagsinput_city"> </div>-->


                                                <div class="eerr" style="position: relative">
                                                    <input type="text" id="txtCitiesIServe" placeholder="Search your city.." class="form-control inputCitySearch" />

                                                    <div id="divCitiesIServeLoader" class="divCitySearchLoader" style="top: 5px; position: absolute; right: 10px; display: none">
                                                        <img src="/images/377.gif" />
                                                    </div>
                                                    <div id="div_CitiesIServeSearchContent" class="divCitySearchContent" style="margin-left: 3px; margin-top: 0px; width: 98%;">
                                                    </div>
                                                </div>
                                                <div class="margin-top-10"><a onclick="SetCitiesIServeLocation(this)" class="btn red" id="btnAddCity">Add City</a>  </div>
                                            </div>

                                        </div>
                                        <!--end col-md-4-->
                                    </div>
                                    <!--end row-->
                                    <div id="divReferalsContainer" class="tabbable-line tabbable-custom-profile">
                                        <div class="tab-content">
                                            <div class="tab-pane active">
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-advance table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Posted By</th>
                                                                <th>Location</th>
                                                                <th>Referred On</th>
                                                                <th>Satisfied</th>
                                                                <th>Comments</th>
                                                                <th>Action</th>
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

                                    <div id="divEstimateContainer" style="display: none;" class="tabbable-line tabbable-custom-profile">

                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_1_11">
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-advance table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Job title </th>
                                                                <th>Location </th>
                                                                <th>Estimate Status </th>
                                                                <th>Job Status </th>
                                                                <th>My Estimates </th>
                                                                <th>Revise Estimates </th>
                                                                <!-- <th class="hidden-xs"> <i class="fa fa-question"></i> Descrition </th>
                                                                 <th> <i class="fa fa-bookmark"></i> Amount </th>-->
                                                            </tr>
                                                        </thead>
                                                        <tbody id="divEstimate">
                                                        </tbody>
                                                    </table>

                                                    <div id="divShowMoreEstimates" class="table_reqBtn">
                                                        <a class="btn blue btn-outline" onclick="ShowMoreEstimates();" type="button">SHOW MORE</a>
                                                    </div>

                                                </div>
                                            </div>
                                            <!--tab-pane-->
                                            <!--tab-pane-->
                                        </div>
                                    </div>


                                    <div id="divAppointmentContainer" style="display: none;" class="tabbable-line tabbable-custom-profile vsprofiletable5">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a id="a_tabAppointmentRequests" href="#tab_AppointmentRequest" data-toggle="tab">Appointment Requests </a></li>
                                            <li><a id="a_tabMyAppointments" href="#tab_MyAppointments" data-toggle="tab">My Appointments </a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_AppointmentRequest">
                                                <div class="portlet-body">
                                                    <div class="table-scrollable">
                                                        <table class="table table-striped table-bordered table-advance table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>User </th>
                                                                    <th>Job Title </th>
                                                                    <th>My Estimate </th>
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
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <%if (userData != null)
        { %>
    <script src="/js/moment.min.js" type="text/javascript"></script>
    <script src="/js/fullcalendar.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var month = 0;
        var calendarMonth = new Date().getMonth();
        var gotodate = new Date();
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
        function GetRequestedAppointmentDates(appointmentID,IsDatesByMe,userName) {
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
                        if(IsDatesByMe)
                        {
                            $("#h4RequestDates").html("You Have Requested These Dates for Appointment");
                            
                        }
                        else
                        {
                            $("#h4RequestDates").html(userName+" Has Requested These Dates for Appointment");
                            
                        }
                        $("#YourRequestedDatesTemplate").tmpl(result.d).appendTo("#divYourRequestedDatesData");
                    }
                }
            });
        }
        function SetAddCityBtn(btn)
        {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtCitiesIServe").val(text);
            $("#btnAddCity").data("data-id",id);
            $("#btnAddCity").data("data-text",text);
            //AddTags(text, id);
            $("#div_CitiesIServeSearchContent").css("display", "none");
            $("#div_CitiesIServeSearchContent").empty();
        }
        function SetCitiesIServeLocation(btn) {
            
            if($(btn).data("data-id")!="")
            { $("#txtCitiesIServe").val("");
                
                var id = $(btn).data("data-id");
                var text = $(btn).data("data-text");
                $(btn).removeData("data-id");
                $(btn).removeData("data-text");
                AddTags(text, id);
            }
           
        }
        function GetCitiesIServeLocation(searchContent) {
            if (searchContent != "") {
                $.ajax({
                    url: '/Index.aspx/GetLocations',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "Keyword":"' + searchContent + '"}',
                    success: function (result) {
                        if (result.d != null) {
                            if (result.d.length > 0) {
                                $("#divCitiesIServeLoader").hide();
                                $("#div_CitiesIServeSearchContent").empty();
                                $("#div_CitiesIServeSearchContent").hide();
                                $("#CitiesIServe_template").tmpl(result.d).appendTo("#div_CitiesIServeSearchContent");
                                $("#div_CitiesIServeSearchContent").slideDown('slow');
                            }
                            else {
                                $("#divCitiesIServeLoader").hide();
                                $("#div_CitiesIServeSearchContent").empty();
                                $("#div_CitiesIServeSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                                $("#div_CitiesIServeSearchContent").slideDown('slow');
                            }
                        }
                        else {
                            $("#divCitiesIServeLoader").hide();
                            $("#div_CitiesIServeSearchContent").empty();
                            $("#div_CitiesIServeSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                            $("#div_CitiesIServeSearchContent").slideDown('slow');
                        }
                    }
                });
            }
            else {
                $("#divCitiesIServeLoader").hide();
                $("#div_CitiesIServeSearchContent").css("display", "none");
                $("#div_CitiesIServeSearchContent").empty();
            }
        }
        var pageIndexUserReferals=0; var pageSize = 10;
        var pageIndexAppointmentRequests = 0;
        var pageIndexEstimates= 0;
        $(function () {
            
            var CitiesIServe=  <%=jsonForCitiesIServe%>;

            $.each(CitiesIServe,function(i,v){
                $("#object_tagsinput").tagsinput("add", { value: v.Id, text: v.City });
            })

            $('#object_tagsinput').on('itemRemoved', function(event) {
                RemoveCityIServe(event.item.value);
                //var selectedCities=$('#object_tagsinput').val();
                //SetCitiesIServe(selectedCities);

                    
            });

            $('#object_tagsinput').on('itemAdded', function(event) {
                // event.item: contains the item
                AddCityIServe(event.item.value);    
                //var selectedCities=$('#object_tagsinput').val();
                //SetCitiesIServe(selectedCities);
            });
            var timeout;
            $('#txtCitiesIServe').keyup(function () {
                clearTimeout(timeout);
                var txtval = $(this).val();
                if (txtval.length > 1) {
                    $("#divCitiesIServeLoader").show();
                    timeout = setTimeout(function () { GetCitiesIServeLocation(txtval); }, 1000);
                }
                else {
                    $("#div_CitiesIServeSearchContent").empty();
                    $("#divCitiesIServeLoader").hide();
                }
            });
            GetWhoReferedMe(0);
            GetAppointmentRequests(0);
            GetEstimates(0);
        });
            function activeTab(tab){
                $('.nav-tabs a[href="#' + tab + '"]').tab('show');
            };
            function ShowAppointments() {
                $("#divReferalsContainer").hide();
                $("#divEstimateContainer").hide();
                $("#divAppointmentContainer").show();
                activeTab("tab_MyAppointments");
            }
            function ShowAppointmentRequests() {
                $("#divReferalsContainer").hide();
                $("#divEstimateContainer").hide();
                $("#divAppointmentContainer").show();
                activeTab("tab_AppointmentRequest");
            }
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
                                    url:'/jobdetail/'+v.JobID+'/'+v.UrlFriendlyJobTitle+''
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
                            //else {


                            //    $('#calendar').fullCalendar('destroy');
                            //    //if (month = new Date().getMonth() + 2) {

                            //    //    // $('#calendar').fullCalendar('next');
                            //    //}

                            //    //if (month = new Date().getMonth()) {
                            //    //    $('#calendar').fullCalendar('prev');
                            //    //}
                            //    $('#calendar').fullCalendar({
                            //        month: month,
                            //        header: {
                            //            left: 'prev,next today',
                            //            center: 'title',
                            //            right: 'month,agendaWeek,agendaDay'
                            //        },
                            //        overlap: false,
                            //        //defaultDate: '2016-06-06',

                            //        editable: false,
                            //        eventLimit: true, // allow "more" link when too many events
                            //        events: eventsArrary,
                            //        viewRender: function (view, element) {
                            //            month = view.intervalStart._d.getMonth() + 1
                            //            GetAppointmentsForCalendar();
                            //        }
                            //    });


                            //}
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
            function GetAppointmentRequests(pageIndex) {

                $("#divShowMoreAppointmentRequests").hide();
                if ($("#divAppointmentRequest").html() == "") {
                    pageIndex = 0;
                }
                $.ajax({
                    url: '/ProfessionalDashboard.aspx/GetAppointmentRequests',
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
            function GetEstimates(pageIndex) {

                $("#divShowMoreEstimates").hide();
                if ($("#divEstimate").html() == "") {
                    pageIndex = 0;
                }
                $.ajax({
                    url: '/professionaldashboard.aspx/GetEstimates',
                    type: "POST",

                    data: '{"pageIndex":"' + pageIndex + '","pageSize":"' + pageSize + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        $("#spanEstimateCount").html(result.d.EstimateCount);
                        if (result.d.EstimateCount > 0) {
                            if (pageIndex == 0) {
                                $("#divEstimate").empty();
                            }
                            if (result.d.HideShowMore) {
                                $("#divShowMoreEstimates").hide();
                            }
                            else {
                                //pageIndexJobsOpen++;
                                $("#divShowMoreEstimates").show();
                            }

                            $("#EstimateListTemplate").tmpl(result.d.EstimateList).appendTo("#divEstimate");

                        }
                        else {
                            $("#divEstimate").html("No records found");
                        }

                    }
                });
            }
            function GetWhoReferedMe(pageIndex) {

                $("#divShowMoreReferals").hide();
                if ($("#divReferalList").html() == "") {
                    pageIndex = 0;
                }
                $.ajax({
                    url: '/ProfessionalDashboard.aspx/GetWhoReferedMe',
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
            function ShowReferals() {
                $("#divReferalsContainer").show();
                $("#divEstimateContainer").hide();
                $("#divAppointmentContainer").hide();
            }
            function ShowEstimates() {
                $("#divReferalsContainer").hide();
                $("#divEstimateContainer").show();
                $("#divAppointmentContainer").hide();
            }
            function ShowMoreAppointmentRequests() {
                pageIndexAppointmentRequests++;
                GetAppointmentRequests(pageIndexAppointmentRequests);
            }
            function ShowMoreEstimates() {
                pageIndexEstimates++;
                GetEstimates(pageIndexEstimates);
            }
            function ShowMoreUserReferals() {
                pageIndexUserReferals++;
                GetWhoReferedMe(pageIndexUserReferals);
            }
            function AddCityIServe(cityID)
            {
            
                $.ajax({
                    url: '/ProfessionalDashboard.aspx/AddCityIServe',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "cityID":"' +cityID+ '"}',
                    success: function (data) {}});
            
            }
            function RemoveCityIServe(cityID)
            {
            
                $.ajax({
                    url: '/ProfessionalDashboard.aspx/RemoveCityIServe',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "cityID":"' +cityID+ '"}',
                    success: function (data) {}});
            
            }
            function SetCitiesIServe(selectedCities)
            {
                $.ajax({
                    url: '/ProfessionalDashboard.aspx/SetCitiesOfServe',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "cities":"' + selectedCities + '"}',
                    success: function (data) {}});
            }
            function AddTags(cityText, cityValue) {
                var tagvalue = cityValue ;<%--$('#<%=drpAvailableCities.ClientID%> option:selected').val();--%>
                var tagtext = cityText ;<%--$('#<%=drpAvailableCities.ClientID%> option:selected').text();--%>
                if (parseInt(tagvalue) != 0) {
                    $('#object_tagsinput').tagsinput('add', { value: tagvalue, text: tagtext });
                }
               
            }

    </script>
    <script type="text/javascript">

        function FlagReferral(ID)
        {
            bootbox.confirm('Are you sure?',function(i){
                if(i)
                {
                    $("#aFlag"+ID).html("Flagged");
                    $.ajax({
                        url: '/ProfessionalDashboard.aspx/FlagReferral',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ "ID":"' + ID+ '"}',
                        success: function (data) {
                            if(data.d)
                            {
                                $("#aFlag"+ID).html("Flagged");
                            }
                            else
                            {
                                $("#aFlag"+ID).html('<a  class="btn btn-sm grey-salsa btn-outline" onclick="FlagReferral('+ID+')">Flag this </a>');
                            }
                        }
            
                    });
                }
            })
            
        }
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
    <script id="referalListTemplate" type="text/html">
        <tr>
            <td>${UserName}</td>
            <td>${CityName}</td>
            <td>${CreatedDateString}</td>
            <td>{{if IsSatisfied}} Yes {{else}}No{{/if}}</td>
            <td class="ppr_width">${Comment}</td>
            <td id="aFlag${Id}">{{if IsFlag}}
                Flagged
                {{else}}
                <a class="btn btn-sm grey-salsa btn-outline" onclick="FlagReferral(${Id})">Flag this </a>
                {{/if}}
            </td>
        </tr>
    </script>
    <script id="CitiesIServe_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetAddCityBtn(this);">
            ${City}, ${State}
        </label>
    </script>
    <script id="AppointmentRequestListTemplate" type="text/html">
        <tr>
            <td><a href="javascript:;">${UserName} </a></td>
            <td>${JobTitle}</td>
            <td>$${Estimate}</td>
            <td class="stepwidth">${Comments}</td>

            <td style="text-align: center">{{if RescheduledByProfessional==true}}
                  <a class="btn btn-sm grey-salsa btn-outline" onclick="GetRequestedAppointmentDates(${AppointmentID},true,'${UserName}')" data-toggle="modal">Your Requested Dates  </a>

                {{else}}
                  <a class="btn btn-sm grey-salsa btn-outline" onclick="GetRequestedAppointmentDates(${AppointmentID},false,'${UserName}')" data-toggle="modal">View Requested Dates  </a>
                {{/if}}
            </td>
        </tr>

    </script>
    <script id="EstimateListTemplate" type="text/html">
        <tr>
            <td><a href="javascript:;">${JobTitle} </a></td>
            <td>${Location}</td>
            <td>{{if EstimateStatus==0}}
                Pending
             {{else  EstimateStatus==1}}    
                Accepted
                {{else EstimateStatus==2}}    
                Rejected
                {{/if}}    
            </td>
            <td>{{if JobStatus==1}}
                Open
            {{else JobStatus==2}}    
                Done
                {{else  JobStatus==3}}    
                Cancelled
                {{/if}}    </td>
            <td>$${Estimate}</td>
            <td>{{if JobStatus==1}}
                <a class="btn btn-sm grey-salsa btn-outline" href="/JobDetail/${JobID}/{UrlFriendlyJobTitle}">Revise </a>
                {{/if}}
            </td>
        </tr>
    </script>
    <script id="YourRequestedDatesTemplate" type="text/html">
        {{tmpl(Dates,{scb:RescheduledByProfessional}) "#YourRequestedDatesDataTemplate"}}
         <div class="form-group groupestimate">
             <a href="/appointmentform?appointmentID=${ID}&quoteID=${QuoteID}&RescheduledByProfessional=true" class="btn green" type="button">Reschedule</a>
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
                {{if  $item.scb==false}}
                <div class="devide_2text">
                    <a onclick="AcceptAppointmentDate(${ID})" class="btn blue" type="button">Accept</a>
                </div>
                {{/if}}
            </div>
        </div>


    </script>
    <%} %>

    <script src="/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <%--<script src="/js/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/js/app.min.js" type="text/javascript"></script>--%>
</asp:Content>
