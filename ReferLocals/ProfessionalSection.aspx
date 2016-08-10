<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProfessionalSection.aspx.cs" Inherits="ReferLocals.ProfessionalSection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- meta charec set -->
    <meta charset="utf-8" />
    <!-- Always force latest IE rendering engine or request Chrome Frame -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <!-- Page Title -->
    <title>Referlocals.com</title>
    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="/css/main.css" />
    <link rel="stylesheet" href="/css/media-queries.css" />
    <style>
        .autocomplete_section, .workautocomplete_section {
            width: 100% !important;
            padding: 4px 6px 7px 11px !important;
            border-bottom: 1px solid #ccc !important;
            color: #555 !important;
            cursor: pointer;
            font-size: 14px !important;
            font-weight: 400 !important;
            font-family: 'Open Sans', sans-serif !important;
        }

        #div_SearchContent, #div_WorkSearchContent, #div_CitiesIServeSearchContent {
            background-color: #f1f1f1;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.73);
            display: none;
            margin: 2px 0 0 18px;
            max-height: 735px;
            overflow-y: hidden;
            overflow: auto;
            width: 92%;
            z-index: 9999;
        }

        #divLoader, #divWorkLoader {
            position: absolute;
            top: 5px;
            right: 25px;
        }

        .txtbox_invisible {
            border: none;
            height: 0px;
            width: 0px;
        }

        .bootstrap-tagsinput input {
            width: 100% !important;
        }

        .drp_control {
            width: 15% !important;
            float: left !important;
            border: 1px solid #c2cad8;
            height: 40px;
            border-radius: 4px;
            padding: 6px 12px;
        }
    </style>
    <link href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.2.3/css/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <link href="/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />

    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->

    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="/css/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="/css/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="/css/layout.min.css" rel="stylesheet" type="text/css" />
    <!--<link href="css/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />-->
    <link href="/css/custom.min.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.min.js" type="text/javascript"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/js/cookie.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="/js/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/js/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="/js/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap-switch.min.js" type="text/javascript"></script>

    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->

    <!-- END PAGE LEVEL PLUGINS -->

    <!-- BEGIN THEME FORM RUN SCRIPTS -->
    <script src="/js/select2.full.min.js" type="text/javascript"></script>
    <script src="/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="/js/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="/js/form-wizard.min.js" type="text/javascript"></script>
    <!-- END THEME FORM RUN SCRIPTS -->

    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="/js/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->

    <!-- START Tags Input CSS & JS -->
    <link href="/css/bootstrap-tagsinput.css" rel="stylesheet" type="text/css" />
    <script src="/js/bootstrap-tagsinput.min.js" type="text/javascript"></script>
    <script src="/js/components-bootstrap-tagsinput.min.js" type="text/javascript"></script>
    <script src="/js/bootbox.min.js" type="text/javascript"></script>
    <!-- End Tags Input CSS & JS -->

    <!-- START PROFILE CSS & JS -->
    <%--    <link href="/css/profile.min.css" rel="stylesheet" type="text/css" />
    <script src="/js/profile.min.js" type="text/javascript"></script>--%>
    <!-- END PROFILE CSS & JS -->

    <!--==================================== End Special Css And Js ====================================-->
    <script id="CategoryScript" type="text/html">
        <div class="search_h2">
            <h2><i class="fa fa-angle-double-right"></i>${Name} </h2>
        </div>
        <div class="small_images" id="CheckView${Id}">
            {{tmpl(lst_subcategory) "#SubCategoryScript"}}
        </div>
        {{if SubCategoryCount > 4}}
        <div class="stepbtn">
            <button class="btn green uppercase lineblue" id="btnViewMore${Id}" data-id="${Id}" onclick="return ShowSubCategoryList(this);" type="submit">View More</button>
        </div>
        {{else}}
        {{/if}}
    </script>
    <script id="SubCategoryScript" type="text/html">
        <div class="col-md-3">
            <div class="imageSmall divCat${CategoryId}">
                <img alt="${Image}" src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath %>${Image}?width=255&height=265&mode=crop&scale=both" />
                <div class="smallBlack">
                    <h2 class="">${Name}
                    </h2>
                </div>
                <div class="form-group form-md-checkboxes has-info checkbox_bg">
                    <div class="md-checkbox">
                        <input type="checkbox" id="checkbox1_${Id}" data-id="${Id}" class="md-check" onchange="return SelectCategory(this);" />
                        <label for="checkbox1_${Id}"><span class="inc"></span><span class="check"></span><span class="box"></span></label>
                    </div>
                </div>
            </div>
        </div>
    </script>
    <script type="text/javascript">



        $(document).ready(function () {
            ShowCategoryList();

        });
        var take = 4;
        var index = 0;
        var subIndex = 1;
        function ShowCategoryList() {
            index = index + 1;
            $.ajax({
                url: '/ProfessionalSection.aspx/GetCategories',
                type: "POST",
                data: '{ "index":"' + parseInt(index - 1) + '","take":"' + parseInt(take) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.length > 0) {
                        if (index - 1 == 0) {
                            $("#divCategory").empty();
                        }
                        $("#CategoryScript").tmpl(result.d).appendTo("#divCategory");
                    }
                    else {
                        if (index - 1 == 0) {
                            $("#divCategory").html('<div style="width:100%;text-align:left;font-size:14px;font-weight:bold"><font color="black">No Record.</font></div>');
                        }
                    };
                }
            });
            return false;
        }

        function ShowSubCategoryList(btn) {
            var id = $(btn).attr("data-id");
            var countSubCats = $(".divCat" + id).length;
            subIndex = Math.floor(countSubCats / take);
            subIndex = subIndex + 1;
            $.ajax({
                url: '/ProfessionalSection.aspx/GetSubCategories',
                type: "POST",
                data: '{ "index":"' + parseInt(subIndex - 1) + '","take":"' + parseInt(take) + '","categoryid":"' + parseInt(id) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.length > 0) {
                        if (subIndex - 1 == 0) {
                            $("#CheckView" + id).empty();
                        }
                        $("#SubCategoryScript").tmpl(result.d).appendTo("#CheckView" + id);
                        if (result.d.length == take) {
                            $("#btnViewMore" + id).css("display", "block");
                        }
                        else {
                            $("#btnViewMore" + id).css("display", "none");
                        }
                    }
                    else {
                        $("#btnViewMore" + id).css("display", "none");
                    };
                }
            });
            return false;
        }
        //function ShowCategoryList() {
        //    index = index + 1;
        //    $.ajax({
        //        url: '/ProfessionalSection.aspx/GetCategories',
        //        type: "POST",
        //        data: '{ "index":"' + parseInt(index - 1) + '","take":"' + parseInt(take) + '"}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (result) {
        //            if (result.d.length > 0) {
        //                if (index - 1 == 0) {
        //                    $("#divCategory").empty();
        //                }
        //                $("#CategoryScript").tmpl(result.d).appendTo("#divCategory");
        //            }
        //            else {
        //                if (index - 1 == 0) {
        //                    $("#divCategory").html('<div style="width:100%;text-align:left;font-size:14px;font-weight:bold"><font color="black">No Record.</font></div>');
        //                }
        //            };
        //        }
        //    });
        //    return false;
        //}

        //function ShowSubCategoryList(btn) {
        //    var id = $(btn).attr("data-id");
        //    subIndex = subIndex + 1;
        //    $.ajax({
        //        url: '/ProfessionalSection.aspx/GetSubCategories',
        //        type: "POST",
        //        data: '{ "index":"' + parseInt(subIndex - 1) + '","take":"' + parseInt(take) + '","categoryid":"' + parseInt(id) + '"}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (result) {
        //            if (result.d.length > 0) {
        //                if (subIndex - 1 == 0) {
        //                    $("#CheckView" + id).empty();
        //                }
        //                $("#SubCategoryScript").tmpl(result.d).appendTo("#CheckView" + id);
        //                if (result.d.length == take) {
        //                    $("#btnViewMore" + id).css("display", "block");
        //                }
        //                else {
        //                    $("#btnViewMore" + id).css("display", "none");
        //                }
        //            }
        //            else {
        //                $("#btnViewMore" + id).css("display", "none");
        //            };
        //        }
        //    });
        //    return false;
        //}

        function ValidateUSPhoneNumber(phoneNumber) {
            var regExp = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
            var phone = phoneNumber.match(regExp);
            if (phone) {
                return true;
            }
            return false;
        }

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }


        <%-- function AddTags() {
            var tagvalue = $('#<%=drpAvailableCities.ClientID%> option:selected').val();
            var tagtext = $('#<%=drpAvailableCities.ClientID%> option:selected').text();
            if (parseInt(tagvalue) != 0) {
                $('#object_tagsinput').tagsinput("add", { value: tagvalue, text: tagtext })
            }
        }--%>
        function AddTags(cityText, cityValue) {
            var tagvalue = cityValue <%--$('#<%=drpAvailableCities.ClientID%> option:selected').val();--%>
            var tagtext = cityText <%--$('#<%=drpAvailableCities.ClientID%> option:selected').text();--%>
            if (parseInt(tagvalue) != 0) {
                $('#object_tagsinput').tagsinput("add", { value: tagvalue, text: tagtext });
            }
            return false;
        }

        function SelectCategory(btn) {
            var id = $(btn).attr("data-id");
            var SelectedIds = $("#<%=hdnCategories.ClientID%>").val();
            var SelectedIdText = $("#<%=hdnCategories.ClientID%>").text();
            if ($(btn).prop("checked") == true) {
                SelectedIds += id + ",";
                SelectedIdText += id + ",";
            }
            else {
                SelectedIds = SelectedIds.replace(id + ",", "");
                SelectedIdText = SelectedIdText.replace(id + ",", "");
            }
            $("#<%=hdnCategories.ClientID%>").val(SelectedIds);
            $("#<%=hdnCategories.ClientID%>").text(SelectedIdText);
        }







        function CheckValidation() {
            var ErrorMessage = "";
            var Phone = $("#<%=txtPhoneNumber.ClientID%>").val();
            var Email = $("#<%=txtEmail.ClientID%>").val();
            var FirstName = $("#<%=txtFirstName.ClientID%>").val();
            var LastName = $("#<%=txtLastName.ClientID%>").val();
            var BusinessSite = $("#<%=txtBusinessSite.ClientID%>").val();
            var CompanyName = $("#<%=txtCompanyName.ClientID%>").val();
            var City = $("#hdnZipId").val();

            var SelectedCities = $('#object_tagsinput').val();

            if (FirstName == "") {
                if (ErrorMessage == "") {
                    ErrorMessage += "Please enter first name" + "<br/>";
                }
                $('#<%=txtFirstName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtFirstName.ClientID%>').css("border-color", "#c2cad8");
            }

            if (LastName == "") {

                ErrorMessage += "Please enter last name" + "<br/>";

                $('#<%=txtLastName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtLastName.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Email == "") {

                ErrorMessage += "Please enter email" + "<br/>";

                $('#<%=txtEmail.ClientID%>').css("border-color", "red");
            }
            else {
                if (!IsEmail(Email)) {

                    ErrorMessage += "Please enter valid email" + "<br/>";

                    $('#<%=txtEmail.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtEmail.ClientID%>').css("border-color", "#c2cad8");
                }
            }

            if (Phone == "") {

                ErrorMessage += "Please enter phone number" + "<br/>";

                $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "red");
                }
                else {
                    if (!ValidateUSPhoneNumber(Phone)) {

                        ErrorMessage += "Please enter valid phone number" + "<br/>";

                        $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "#c2cad8");
                }
            }



            if (BusinessSite == "") {

                ErrorMessage += "Please enter business site" + "<br/>";

                $('#<%=txtBusinessSite.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtBusinessSite.ClientID%>').css("border-color", "#c2cad8");
                }

                if (CompanyName == "") {

                    ErrorMessage += "Please enter company name" + "<br/>";

                    $('#<%=txtCompanyName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtCompanyName.ClientID%>').css("border-color", "#c2cad8");
            }



            if (City == "0") {
                ErrorMessage += "Please enter work location" + "<br/>";

                $('#txtWorkSearchCity').css("border-color", "red");
            }
            else {
                $('#txtWorkSearchCity').css("border-color", "#c2cad8");
            }

            if (SelectedCities == "") {

                ErrorMessage += "Please enter cities you serve" + "<br/>";

                $('#<%=txtCitiesIServe.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=object_tagsinput.ClientID%>').text(SelectedCities);
                $('#<%=txtCitiesIServe.ClientID%>').css("border-color", "#c2cad8");
            }

            if (ErrorMessage != "") {
                bootbox.alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }
        $(document).ready(function () {
            var timeout;
            $('#txtWorkSearchCity').keyup(function () {
                clearTimeout(timeout);
                var txtval = $(this).val();
                if (txtval.length > 1) {
                    $("#divWorkLoader").show();
                    timeout = setTimeout(function () { GetWorkLocation(txtval); }, 1000);
                }
                else {
                    $("#div_WorkSearchContent").empty();
                    $("#divWorkLoader").hide();
                }
            });

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

        });

        function SetWorkLocation(btn) {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtWorkSearchCity").val(text);
            $("#hdnZipId").val(id);

            $("#div_WorkSearchContent").css("display", "none");
            $("#div_WorkSearchContent").empty();
        }
        function GetWorkLocation(searchContent) {
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
                                $("#divWorkLoader").hide();
                                $("#div_WorkSearchContent").empty();
                                $("#div_WorkSearchContent").hide();
                                $("#WorkCitySearch_template").tmpl(result.d).appendTo("#div_WorkSearchContent");
                                $("#div_WorkSearchContent").slideDown('slow');
                            }
                            else {
                                $("#divWorkLoader").hide();
                                $("#div_WorkSearchContent").empty();
                                $("#div_WorkSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                                $("#div_WorkSearchContent").slideDown('slow');
                            }
                        }
                        else {
                            $("#divWorkLoader").hide();
                            $("#div_WorkSearchContent").empty();
                            $("#div_WorkSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                            $("#div_WorkSearchContent").slideDown('slow');
                        }
                    }
                });
            }
            else {
                $("#divWorkLoader").hide();
                $("#div_WorkSearchContent").css("display", "none");
                $("#div_WorkSearchContent").empty();
            }
        }

        function SetCitiesIServeLocation(btn) {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtCitiesIServe").val(text);
            AddTags(text, id);
            $("#txtCitiesIServe").val("");
            //$("#hdnZipId").val(id);
            $("#div_CitiesIServeSearchContent").css("display", "none");
            $("#div_CitiesIServeSearchContent").empty();
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

        function SendText() {
            $("#aSendText").hide();

            $.ajax({
                url: '/professionalsection.aspx/SendReferMeUrlsText',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '',
                success: function (result) {
                    if (result.d == true) {
                        $("#textSent").show();
                        window.location.href = "/professional_dashboard";

                    }
                    else {
                        $("#aSendText").show();
                    }

                }
            });
        }

    </script>
    <script id="WorkCitySearch_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State} ${Zip}" onclick="return SetWorkLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <script id="CitiesIServe_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State} ${Zip}" onclick="return SetCitiesIServeLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <script>
        function OpenReferMeUrlModal() {

            $('#responsive1').modal({ backdrop: 'static', keyboard: false });
        }

    </script>
</head>
<body id="body">

    <div>
        <!--==================================== Start Fixed Navigation ====================================-->
        <div class="toplogin_head">
            <a href="/Index">
                <img src="/images/blue_logo.png" alt="#" />
            </a>
        </div>

        <!--==================================== End Fixed Navigation ====================================-->

        <!--==================================== Start Search Professional Headder ====================================-->

       <%-- <div class="Search_for">
            <div class="extraspace"></div>
            <div class="container">
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li><a href="index.html" class="active">Home</a> <i class="fa fa-angle-double-right"></i></li>
                        <li><span>Search</span> </li>
                    </ul>
                  
                </div>
            </div>
        </div>--%>

        <!--==================================== End Search Professional Headder ====================================-->

        <!--==================================== Start Signup Form HTML ====================================-->

        <div class="numbring_form">
            <div class="container">
                <div class="portlet light bordered" id="form_wizard_1">
                    <div class="portlet-title">
                        <h2 class="stepheading">Thank you for joining Refer Locals </h2>
                    </div>
                    <div class="portlet-body form">
                        <form id="submit_form" runat="server" class="form-horizontal">
                            <asp:HiddenField ID="hdnZipId" runat="server" Value="0" ClientIDMode="Static" />

                            <div class="form-wizard form_wizard_v">
                                <div class="form-body">
                                    <ul class="nav nav-pills nav-justified steps">
                                        <li><a href="#tab1" data-toggle="tab" class="step"><span class="number">1 </span><span class="desc"><i class="fa fa-check"></i>Signup </span></a></li>
                                        <li class="active"><a href="#tab2" data-toggle="tab" class="step active"><span class="number">2 </span><span class="desc"><i class="fa fa-check"></i>Profession Type </span></a></li>
                                        <li><a href="#tab3" data-toggle="tab" class="step"><span class="number">3 </span><span class="desc"><i class="fa fa-check"></i>Basic Info </span></a></li>
                                        <!--      <li> <a href="#tab4" data-toggle="tab" class="step"> <span class="number"> 4 </span> <span class="desc"> <i class="fa fa-check"></i> Confirm </span> </a> </li>-->
                                    </ul>
                                    <div id="bar" class="progress progress-striped" role="progressbar">
                                        <div class="progress-bar progress-bar-success"></div>
                                    </div>
                                    <div class="tab-content">
                                        <div class="alert alert-danger display-none">
                                            <button class="close" data-dismiss="alert"></button>
                                            You have some form errors. Please check below.
                                        </div>
                                        <div class="alert alert-success display-none">
                                            <button class="close" data-dismiss="alert"></button>
                                            Your form validation is successful!
                                        </div>
                                        <div class="tab-pane  pagetab_sign" id="tab1">
                                            <h3 class="block block_new">Provide your account details</h3>
                                            <div class="col-md-8">
                                                <div class="content v_content1">
                                                    <div class="login-form">
                                                        <div class="form-group sign_group_form">
                                                            <label class="control-label">Email or Phone Number</label>
                                                            <asp:TextBox class="form-control form-control-solid placeholder-no-fix" ID="txtEmailOrPhone" autocomplete="off" runat="server" ReadOnly="true" />
                                                        </div>
                                                        <div class="form-group sign_group_form sign_group_form2">
                                                            <label class="control-label">Password</label>
                                                            <asp:TextBox class="form-control form-control-solid placeholder-no-fix" ID="txtPassword" TextMode="Password" autocomplete="off" runat="server" ReadOnly="true" />
                                                        </div>
                                                    </div>

                                                    <!-- BEGIN REGISTRATION FORM -->

                                                    <!-- END REGISTRATION FORM -->
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane active" id="tab2">
                                            <asp:TextBox ID="hdnCategories" runat="server" required="true" CssClass="txtbox_invisible" />
                                            <p class="step_p_text">"We are committed to provide you the tools to grow your business and you will never pay a  monthly fee or fee to give quotes on jobs posted by your potential client." </p>
                                            <p class="step_p_text"><strong>Finish the Sign up Process, start using Referlocals to grow your business </strong></p>
                                            <h3 class="block block_new">Choose your Profession Type</h3>
                                            <div id="divCategory">
                                            </div>

                                        </div>

                                        <div class="tab-pane pagetab_sign" id="tab3">
                                            <p class="step_p_text">There is No Charge to Sign Up or to Connect with your client Signing Up with us Should be No Brainer!  You are just Step away to fuel your business based on referrals </p>
                                            <p class="step_p_text"><strong>Pleased to have you on Board </strong></p>
                                            <h3 class="block block_new">Provide your Basic Information</h3>
                                            <div class="col-md-8">
                                                <div class="col-md-10 form-group">
                                                    <label>First Name <span class="required">* </span></label>
                                                    <asp:TextBox class="form-control" ID="txtFirstName" runat="server" MaxLength="50" />
                                                </div>
                                                <div class="col-md-10 form-group">
                                                    <label>Last Name <span class="required">* </span></label>
                                                    <asp:TextBox class="form-control" ID="txtLastName" runat="server" MaxLength="50" />
                                                </div>
                                                <div class="col-md-10 form-group" id="divPhone" runat="server">
                                                    <label>Phone Number <span class="required">* </span></label>
                                                    <asp:DropDownList ID="drpCountryCode" class="form-control drp_control" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:TextBox class="form-control" ID="txtPhoneNumber" onkeypress="return isNumberKey(event)" Style="width: 84%; float: left; margin-left: 1%;" runat="server" MaxLength="16" />
                                                </div>
                                                <div class="col-md-10 form-group" id="divEmail" runat="server">
                                                    <label>Email <span class="required">* </span></label>
                                                    <asp:TextBox class="form-control" ID="txtEmail" runat="server" MaxLength="100" />
                                                </div>
                                                <div class="col-md-10 form-group">
                                                    <label>Business / Company Name <span class="required">* </span></label>
                                                    <asp:TextBox class="form-control" ID="txtCompanyName" runat="server" MaxLength="100" />
                                                </div>
                                                <div class="col-md-10 form-group">
                                                    <label>Business Website <span class="required">* </span></label>
                                                    <asp:TextBox class="form-control" ID="txtBusinessSite" runat="server" MaxLength="200" />
                                                </div>
                                                <%--<div class="col-md-10 form-group">
                                                    <label>Work location ( State ) <span class="required">* </span></label>
                                                    <asp:DropDownList ID="drpState" class="form-control" onchange="return BindCitiesByState();" runat="server">
                                                        <asp:ListItem Text="Select" Value="0" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-10 form-group">
                                                    <label>Work location ( City ) <span class="required">* </span></label>
                                                    <asp:DropDownList ID="drpCity" class="form-control" onchange="return BindZipByCity();" runat="server">
                                                        <asp:ListItem Text="Select" Value="0" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-10 form-group">
                                                    <label>Work location ( Zip ) <span class="required">* </span></label>
                                                    <asp:DropDownList ID="drpZip" onchange="return SelectedZip();" class="form-control" runat="server">
                                                        <asp:ListItem Text="Select" Value="0" />
                                                    </asp:DropDownList>
                                                </div>--%>
                                                <%--<div class="col-md-10 form-group vslct">
                                                    <label>Cities I Serve <span class="required">* </span></label>
                                                    <asp:DropDownList ID="drpAvailableCities" onchange="AddTags();" class="form-control" runat="server">
                                                        <asp:ListItem Text="Select" Value="0" />
                                                    </asp:DropDownList>
                                                    <div class="col-md-12">
                                                        <asp:TextBox class="form-control" ID="object_tagsinput" runat="server" />
                                                    </div>
                                                </div>--%>
                                                <div class="col-md-10 form-group">
                                                    <label>Work location  <span class="required">* </span></label>
                                                    <input type="text" id="txtWorkSearchCity" runat="server" clientidmode="Static" value="" placeholder="Enter City or Zipcode.." class="form-control" />
                                                    <div id="divWorkLoader" style="display: none; top: 35px;">
                                                        <img src="/images/377.gif" />
                                                    </div>
                                                    <div id="div_WorkSearchContent">
                                                    </div>
                                                </div>
                                                <div class="col-md-10 form-group vslct" style="position: relative">
                                                    <label>Cities I Serve <span class="required">* </span></label>
                                                    <input type="text" id="txtCitiesIServe" runat="server" placeholder="Enter City or Zipcode.." clientidmode="Static" value="" class="form-control inputCitySearch" />
                                                    <div id="divCitiesIServeLoader" class="divCitySearchLoader" style="top: 35px; position: absolute; right: 30px; display: none">
                                                        <img src="/images/377.gif" />
                                                    </div>
                                                    <div id="div_CitiesIServeSearchContent" class="divCitySearchContent">
                                                    </div>
                                                    <%-- <asp:DropDownList ID="drpAvailableCities" onchange="AddTags();" class="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>--%>
                                                    <br />
                                                    <div class="col-md-12">
                                                        <asp:TextBox class="form-control" ID="object_tagsinput" ClientIDMode="Static" runat="server" />
                                                        <%--<input type="text" id="object_tagsinput" runat="server" name="name" value=" " />--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="profesional_div">
                                                    <h2>Why <strong>Referlocals ? </strong></h2>
                                                    <div class="text_div_signup">
                                                        <img src="/images/signup_icon.png" alt="" />
                                                        <p>We believe Referrals are the best way to grow your business. </p>
                                                    </div>
                                                    <div class="text_div_signup">
                                                        <img src="/images/signup_icon2.png" alt="" />
                                                        <p>We do not charge Monthly Fee ( No concept of buying zipcodes or cities, just mention which cities do you serve in). </p>
                                                    </div>
                                                    <div class="text_div_signup">
                                                        <img src="/images/signup_icon2.png" alt="" />
                                                        <p>We do not charge you a Fee to send you response or quotes for the jobs posted by people . </p>
                                                    </div>
                                                    <div class="text_div_signup">
                                                        <img src="/images/signup_icon3.png" alt="" />
                                                        <p>We are bunch of Technology People who are committed to provide you better tools to build and grow your business. </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions nextSignup_btn ">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <a href="javascript:;" class="btn default button-previous bp_btn"><i class="fa fa-angle-left"></i>Back </a><a href="javascript:;" class="btn btn-outline green button-next">Continue <i class="fa fa-angle-right"></i></a>
                                            <!--<a href="javascript:;" class="btn green button-submit"> Submit <i class="fa fa-check"></i> </a>-->
                                            <asp:Button Text="Sign up" ID="btnSignUp" OnClick="btnSignUp_OnClick" OnClientClick="return CheckValidation();" class="btn green button-submit " runat="server" />
                                            <a class="btn green button-submit ancherback v_sign_up_btn hide" id="btnOpenModal" href="#responsive1" data-toggle="modal">Sign up </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div id="responsive1" class="modal fade estimateFade estimateFade_v step_fade" aria-hidden="false">
            <div class="modal-dialog modal-dialog1">
                <div class="modal-content">
                    <div class="modal-header backgroundtitle">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
                        <h4 class="modal-title">Hey <strong><%=txtFirstName.Text %> <%=txtLastName.Text %></strong> </h4>
                    </div>
                    <div class="modal-body">
                        <div class="scroller" style="height: 320px" data-always-visible="1" data-rail-visible1="1">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="popuplink" style="margin-bottom: 0px;">
                                        Your refer me URL: 
                                        <%if (urlData != null)
                                          { %>
                                        <%foreach (var url in urlData)
                                          {%>
                                        <a href="http://<%=Request.Url.Host %>/<%=url.SubCategoryName%>/<%=profileUrl %>/referme" style="width: 100%; display: block; margin: 0px;"><%=Request.Url.Host %>/<%=url.SubCategoryName%>/<%=profileUrl %>/referme </a>
                                        <br />
                                        <%} %>
                                        <%} %>
                                    </div>
                                    <div class="centetext_popup">
                                        <h4>Send this URL as a Text message to you </h4>
                                        <p>So that you can text your clients later and ask them to refer you on ReferLocals </p>
                                    </div>
                                    <%--  <div class="searchcity">
                                        <div class="input-group col-md-12">
                                            <input type="text" style="background: rgb(255, 255, 255) none repeat scroll 0% 0%; border-radius: 2px 0px 0px 2px;" class="  search-query form-control serchfield" placeholder="Enter Your phone Number" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-danger" type="button"><span class=" serchicon">Send Text</span> </button>
                                            </span>
                                        </div>
                                    </div>--%>
                                    <div id="textSent" style="display: none; text-align: center; color: green;margin-bottom:10px;">Text message sent to your registered phone number</div>
                                    <div class="form-group groupestimate" style="margin-top: 0px;">
                                        <a id="aSendText" onclick="SendText()" class="btn green" type="button">Send Text</a>
                                        <a class="btn green" type="button" href="/professional_dashboard">Skip</a>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--==================================== End Signup Form HTML ====================================-->
    </div>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    
    <asp:Literal ID="ltScript" runat="server"></asp:Literal>
</body>
</html>
