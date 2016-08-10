<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ReferLocals.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[disabled], select[disabled] {
            cursor: default;
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #ccc;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnZipId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedCities" runat="server" />
    <asp:HiddenField ID="hdnCountryCode" runat="server" />
    <!--==================================== Start Professional Profile Appointments Requests HTML ====================================-->
  
    <div class="requestProfile">

        <div class="container">
            <div class="profile ed_profile">
                <!--tab_1_2-->


                 <i class="fa fa-angle-double-left"></i><a href="/user_dashboard" style="padding-bottom: 10px;
float: right;
width: 99%;
font-style: italic;
font-size: 16px;color: #169EF4;">Go Back to My Dashboard</a>
                <div class="row profile-account">

                    <div class="col-md-3">
                        <ul class="ver-inline-menu tabbable margin-bottom-10">
                            <li class="active"><a href="/Profile"><i class="fa fa-cog"></i>Personal info </a><span class="after"></span></li>
                            <li><a href="/ProfileImage"><i class="fa fa-picture-o"></i>Change Image </a></li>
                            <li><a href="/ChangePassword"><i class="fa fa-lock"></i>Change Password </a></li>
                        </ul>
                    </div>
                    <div class="col-md-9">
                        <div class="tab-content">
                            <div class="tab-pane active">
                                <div role="form">
                                    <div class="form-group">
                                        <label class="control-label">First Name</label>
                                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" class="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label">Last Name</label>
                                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" class="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Email </label>
                                        <div style="width: 100%; float: left; margin-bottom: 15px;">

                                            <asp:TextBox ID="txtEmail" Enabled="false" runat="server" placeholder="Email" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Phone Number</label>
                                        <div style="width: 100%; float: left; margin-bottom: 15px;">
                                            <asp:DropDownList ID="drpCountryCode" Enabled="false" onchange="ChangeCountryCode();" class="form-control form-control-solid placeholder-no-fix" Style="width: 15%; float: left; border: 1px solid #c2cad8; height: 35px; border-radius: 4px; padding: 6px 12px;" runat="server">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtPhoneNumber" Enabled="false" runat="server" placeholder="Phone Number" class="form-control" Style="width: 84%; float: left; margin-left: 1%;" />
                                        </div>
                                    </div>
                                    <div id="divProfessional" runat="server">
                                        <div class="form-group">
                                            <label class="control-label">Business / Company Name</label>
                                            <asp:TextBox ID="txtCompanyName" runat="server" placeholder="Business / Company Name" class="form-control" />
                                        </div>


                                        <div class="form-group">
                                            <label class="control-label">business Website</label>
                                            <asp:TextBox ID="txtWebsite" runat="server" placeholder="Website" class="form-control" />
                                        </div>

                                        <div class="form-group" style="position: relative">
                                            <label>Cities I Serve <span class="required">* </span></label>
                                            <input type="text" id="txtCitiesIServe" runat="server" clientidmode="Static" value=" " class="form-control inputCitySearch" />
                                            <div id="divCitiesIServeLoader" class="divCitySearchLoader" style="top: 30px; position: absolute; right: 10px; display: none">
                                                <img src="/images/377.gif" />
                                            </div>
                                            <div id="div_CitiesIServeSearchContent" class="divCitySearchContent">
                                            </div>
                                            <%-- <asp:DropDownList ID="drpAvailableCities" onchange="AddTags();" class="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>--%>
                                            <br />
                                            <div class="col-md-12">
                                                <input type="text" id="object_tagsinput" name="name" value=" " />
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divUser" runat="server">
                                        <div class="form-group">
                                            <label class="control-label">Street Address</label>
                                            <asp:TextBox ID="txtStreetAddress" runat="server" placeholder="Street Address" class="form-control" />
                                        </div>


                                        <div class="form-group">
                                            <label class="control-label">Apartment</label>
                                            <asp:TextBox ID="txtAppartment" runat="server" placeholder="Apartment" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="position: relative;">
                                        <%if (divUser.Visible)
                                          {%>
                                        <label class="control-label">Location</label>
                                        <%}
                                          else
                                          {%>
                                        <label class="control-label">Work Location</label>
                                        <%} %>
                                        <input type="text" id="txtWorkSearchCity" runat="server" clientidmode="Static" value=" " class="form-control" />
                                        <div id="divWorkLoader" style="display: none; top: 58px;">
                                            <img src="/images/377.gif" />
                                        </div>
                                        <div id="div_WorkSearchContent">
                                        </div>
                                    </div>
                                   

                                    <div class="editProfileBtn">
                                        <asp:Button ID="btnSave" OnClick="btnSave_OnClick" OnClientClick="return CheckValidation();" class="btn blue btn-lg" Text="Save Changes" runat="server" />
                                        <button class="btn  btn-default btn-lg" type="button">Cancel</button>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <!--end col-md-9-->
                </div>
                <!--end tab-pane-->
            </div>
        </div>
    </div>

    <!--==================================== End Professional Profile Appointments Requests HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <!-- START Tags Input CSS & JS -->
    <link href="/css/bootstrap-tagsinput.css" rel="stylesheet" type="text/css" />
    <script src="/js/bootstrap-tagsinput.min.js" type="text/javascript"></script>
    <script src="/js/components-bootstrap-tagsinput.min.js" type="text/javascript"></script>
    <!-- End Tags Input CSS & JS -->
    <script>
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

        $(window).load(function () {

            BindCitiesServe();
        });

        function CheckValidation() {

            var ErrorMessage = "";
            var Phone = $("#<%=txtPhoneNumber.ClientID%>").val();
            var FirstName = $("#<%=txtFirstName.ClientID%>").val();
            var LastName = $("#<%=txtLastName.ClientID%>").val();
            var StreetAddress = $("#<%=txtStreetAddress.ClientID%>").val();
            var Apartment = $("#<%=txtAppartment.ClientID%>").val();
            var CompanyName = $("#<%=txtCompanyName.ClientID%>").val();
            var Website = $("#<%=txtWebsite.ClientID%>").val();
            var City = $("#hdnZipId").val();
           <%-- var City = $('#<%=drpCity.ClientID%> option:selected').val();
            var State = $('#<%=drpState.ClientID%> option:selected').val();
            var Zip = $('#<%=drpZip.ClientID%> option:selected').val();--%>
            var SelectedCities = $('#object_tagsinput').val();
            var UserType = '<%=UserType%>';
            debugger;
            if (FirstName == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter first name";
                }
                $('#<%=txtFirstName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtFirstName.ClientID%>').css("border-color", "#c2cad8");
            }

            if (LastName == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter last name";
                }
                $('#<%=txtLastName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtLastName.ClientID%>').css("border-color", "#c2cad8");
            }


            if (Phone == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter phone number.";
                }
                $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "red");
            }
            else {
                if (!ValidateUSPhoneNumber(Phone)) {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter valid phone number";
                    }
                    $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtPhoneNumber.ClientID%>').css("border-color", "#c2cad8");
                }
            }


            if (parseInt(UserType) == 1) {
                if (StreetAddress == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter street address";
                    }
                    $('#<%=txtStreetAddress.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtStreetAddress.ClientID%>').css("border-color", "#c2cad8");
                }

                if (Apartment == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter apartment";
                    }
                    $('#<%=txtAppartment.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtAppartment.ClientID%>').css("border-color", "#c2cad8");
                }
            }
            else {
                if (CompanyName == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter company name";
                    }
                    $('#<%=txtCompanyName.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtCompanyName.ClientID%>').css("border-color", "#c2cad8");
                }

                if (Website == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter website";
                    }
                    $('#<%=txtWebsite.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtWebsite.ClientID%>').css("border-color", "#c2cad8");
                }
            }

            if (City == "0") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select city";
                }
                $('#txtSearchWorkCity').css("border-color", "red");
            }
            else {
                $('#txtSearchWorkCity').css("border-color", "#c2cad8");
            }

          

            if (SelectedCities == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select cities you serve";
                }
               <%-- $('#<%=drpAvailableCities.ClientID%>').css("border-color", "red");--%>
            }
            else {
                $('#<%=hdnSelectedCities.ClientID%>').val(SelectedCities);
                <%--$('#<%=drpAvailableCities.ClientID%>').css("border-color", "#c2cad8");--%>
            }


            if (ErrorMessage != "") {
                bootbox.alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }

        function AddTags(cityText, cityValue) {
            var tagvalue = cityValue <%--$('#<%=drpAvailableCities.ClientID%> option:selected').val();--%>
            var tagtext = cityText <%--$('#<%=drpAvailableCities.ClientID%> option:selected').text();--%>
            if (parseInt(tagvalue) != 0) {
                $('#object_tagsinput').tagsinput("add", { value: tagvalue, text: tagtext });
            }
            return false;
        }

        function ChangeCountryCode() {
            var countrycode = $('#<%=drpCountryCode.ClientID%> option:selected').val();
            if (countrycode != "") {
                $('#<%=hdnCountryCode.ClientID%>').val(countrycode);
            }
            return false;
        }

        

        function BindCitiesServe() {
            var state = '';
            $.ajax({
                url: '/Profile.aspx/GetCitiesOfServe',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{ "state":"' + state + '"}',
                success: function (data) {
                    var maindata = JSON.parse(data.d);
                    if (maindata.length > 0) {
                        for (var i = 0; i < maindata.length; i++) {
                            var Id = maindata[i].Id;
                            var city = maindata[i].City;
                            var state = maindata[i].State;
                            $("#object_tagsinput").tagsinput("add", { value: Id, text: city + "," + state });
                        }
                    }
                },
                error: function (xhr) {
                    bootbox.alert(xhr.responseText);
                }
            });
        }

       
    </script>
    <script id="WorkCitySearch_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetWorkLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <script id="CitiesIServe_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetCitiesIServeLocation(this);">
            ${City}, ${State}
        </label>
    </script>
</asp:Content>
