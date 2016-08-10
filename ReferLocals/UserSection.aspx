<%@ Page Title="" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserSection.aspx.cs" Inherits="ReferLocals.UserSection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .autocomplete_section {
            width: 100%;
            padding: 4px 6px 7px 11px;
            border-bottom: 1px solid #ccc;
            color: #555;
            cursor: pointer;
        }

        #div_SearchContent {
            background-color: #f1f1f1;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.73);
            display: none;
            /*margin: 2px 0 0 18px;*/
            max-height: 735px;
            overflow-y: hidden;
            overflow: auto;
            width: 100%;
            z-index: 9999;
        }

        #divLoader {
            position: absolute;
            top: 35px;
            right: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Signup Form HTML ====================================-->
    <asp:HiddenField ID="hdnZipId" runat="server" />
    <div class="numbring_form">
        <div class="container">
            <div class="portlet light bordered" id="form_wizard_1">
                <div class="portlet-title">
                    <h2 class="stepheading"><strong>Welcome </strong>to Refer Local </h2>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal" id="submit_form">
                        <div class="form-wizard form_wizard_v">
                            <div class="form-body">

                                <div class="tab-pane pagetab_sign">
                                    <p class="step_p_text">
                                        We will not share your contact info with professionals unless you accept quote on one of your job :) 
                                        <br />
                                        We are Awesome that way :) Do Tell us about yourself
                                    </p>

                                    <div class="col-md-8">

                                        <div class="col-md-10 form-group">
                                            <label>First Name <span class="required">* </span></label>
                                            <asp:TextBox ID="txtFirstName" class="form-control" MaxLength="50" runat="server" />
                                        </div>

                                        <div class="col-md-10 form-group">
                                            <label>Last Name <span class="required">* </span></label>
                                            <asp:TextBox ID="txtLastName" class="form-control" MaxLength="50" runat="server" />
                                        </div>

                                        <div class="col-md-10 form-group" id="divPhone">
                                            <label>Phone Number <span class="required">* </span></label>
                                            <asp:DropDownList ID="drpCountryCode" class="form-control form-control-solid placeholder-no-fix" Style="width: 15%; float: left; border: 1px solid #c2cad8; height: 40px; border-radius: 4px; padding: 6px 12px;" runat="server">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtPhoneNumber" onkeypress="return isNumberKey(event)" class="form-control" MaxLength="16" runat="server" Style="width: 84%; float: left; margin-left: 1%;" />
                                        </div>
                                        <div class="col-md-10 form-group" id="divEmail">
                                            <label>Email <span class="required">* </span></label>
                                            <asp:TextBox ID="txtEmail" class="form-control" MaxLength="100" runat="server" />
                                        </div>


                                        <div class="col-md-10 form-group">
                                            <label>Street Address <span class="required">* </span></label>
                                            <asp:TextBox ID="txtStreetAddress" class="form-control" MaxLength="100" runat="server" />
                                        </div>

                                        <div class="col-md-10 form-group">
                                            <label>Apartment <span class="required">* </span></label>
                                            <asp:TextBox ID="txtApartment" class="form-control" MaxLength="100" runat="server" />
                                        </div>
                                        <div class="col-md-10 form-group" style="position: relative;">
                                            <label>Location <span class="required">* </span></label>
                                            <input type="text" id="txtSearchCity" value="" placeholder="Enter City or Zipcode.." class="form-control" />
                                            <div id="divLoader" style="display: none;">
                                                <img src="/images/377.gif" /></div>
                                            <div id="div_SearchContent">
                                            </div>
                                        </div>

                                        <%-- <div class="col-md-10 form-group">
                                            <label>State <span class="required">* </span></label>
                                            <asp:DropDownList ID="drpState" onchange="return BindCitiesByState();" class="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-10 form-group">
                                            <label>City <span class="required">* </span></label>
                                            <asp:DropDownList ID="drpCity" onchange="return BindZipByCity();" class="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                        </div>                                       

                                        <div class="col-md-10 form-group">
                                            <label>Zip Code <span class="required">* </span></label>
                                            <asp:DropDownList ID="drpZip" onchange="return SelectedZip();" class="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                        </div>--%>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="profesional_div">
                                            <h2>Why <strong>Referlocals ? </strong></h2>
                                            <div class="text_div_signup">
                                                <img src="/images/signup_icon4.png" alt="" />
                                                <p>Search for Local Trusted Professionals referred by your neighbors. </p>
                                            </div>
                                            <div class="text_div_signup">
                                                <img src="/images/signup_icon5.png" alt="" />
                                                <p>Post a Job and get in front of Top professionals and let them give you the best quote. </p>
                                            </div>
                                            <div class="text_div_signup">
                                                <img src="/images/signup_icon6.png" alt="" />
                                                <p>Help other people like you looking for Professional Referrals. </p>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="form-actions nextSignup_btn ">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button Text="Sign up" ID="btnSignup" class="btn btn-outline green button-next v_disbtn" OnClick="btnSignup_OnClick" OnClientClick="return CheckValidation();" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden"  id="hdnLocationId" value="" runat="server" ClientIDMode="Static"  />
    <!--==================================== End Signup Form HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        function CheckValidation() {
            var ErrorMessage = "";
            var Phone = $("#<%=txtPhoneNumber.ClientID%>").val();
            var Email = $("#<%=txtEmail.ClientID%>").val();
            var FirstName = $("#<%=txtFirstName.ClientID%>").val();
            var LastName = $("#<%=txtLastName.ClientID%>").val();
            var StreetAddress = $("#<%=txtStreetAddress.ClientID%>").val();
            var Apartment = $("#<%=txtApartment.ClientID%>").val();
            var City = $("#hdnLocationId").val();
           <%-- var City = $('#<%=drpCity.ClientID%> option:selected').val();
            var State = $('#<%=drpState.ClientID%> option:selected').val();
            var Zip = $('#<%=drpZip.ClientID%> option:selected').val();--%>

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

            if (Email == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter email.";
                }
                $('#<%=txtEmail.ClientID%>').css("border-color", "red");
            }
            else {
                if (!IsEmail(Email)) {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter valid email";
                    }
                    $('#<%=txtEmail.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtEmail.ClientID%>').css("border-color", "#c2cad8");
                }
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
                    ErrorMessage = "Please enter appartment";
                }
                $('#<%=txtApartment.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtApartment.ClientID%>').css("border-color", "#c2cad8");
            }

            if (City == "0" || City == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select city";
                }
                $('#txtSearchCity').css("border-color", "red");
            }
            else {
                $('#txtSearchCity').css("border-color", "#c2cad8");
            }

            <%--if (State == "0") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select state";
                }
                $('#<%=drpState.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=drpState.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Zip == "0") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select zip code";
                }
                $('#<%=drpZip.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=drpZip.ClientID%>').css("border-color", "#c2cad8");
            }--%>


            if (ErrorMessage != "") {
                bootbox.alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }

        <%--function BindCitiesByState() {
            $('#<%=drpCity.ClientID%>').empty();
            var state = $('#<%=drpState.ClientID%> option:selected').text();
            if (state != "") {
                $.ajax({
                    url: '/ProfessionalSection.aspx/GetCitiesByState',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "state":"' + state + '"}',
                    success: function (data) {
                        var maindata = JSON.parse(data.d);
                        if (maindata.length > 0) {
                            $('#<%=drpCity.ClientID%>').append($("<option value=0>Select</option>"));
                            for (var i = 0; i < maindata.length; i++) {
                                var Id = maindata[i].Id;
                                var Text = maindata[i].Name;
                                $('#<%=drpCity.ClientID%>').append($("<option value=" + Id + ">" + Text + "</option>"));
                            }
                        }
                        else {
                            var Value = 0;
                            var Text = "Select";
                            $('#<%=drpCity.ClientID%>').append($("<option value=" + Value + ">" + Text + "</option>"));
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            }
            else {
                var Value = 0;
                var Text = "Select";
                $('#<%=drpCity.ClientID%>').append($("<option value=" + Value + ">" + Text + "</option>"));
            }
        }

        function BindZipByCity() {
            $('#<%=drpZip.ClientID%>').empty();
            var city = $('#<%=drpCity.ClientID%> option:selected').text();
            if (city != "") {
                $.ajax({
                    url: '/ProfessionalSection.aspx/GetZipByCity',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "city":"' + city + '"}',
                    success: function (data) {
                        var maindata = JSON.parse(data.d);
                        if (maindata.length > 0) {
                            $('#<%=drpZip.ClientID%>').append($("<option value=0>Select</option>"));
                            for (var i = 0; i < maindata.length; i++) {
                                var Id = maindata[i].Id;
                                var Text = maindata[i].Name;
                                $('#<%=drpZip.ClientID%>').append($("<option value=" + Id + ">" + Text + "</option>"));
                            }
                        }
                        else {
                            var Value = 0;
                            var Text = "Select";
                            $('#<%=drpZip.ClientID%>').append($("<option value=" + Value + ">" + Text + "</option>"));
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            }
            else {
                var Value = 0;
                var Text = "Select";
                $('#<%=drpZip.ClientID%>').append($("<option value=" + Value + ">" + Text + "</option>"));
            }
        }

        function SelectedZip() {
            var id = $('#<%=drpZip.ClientID%> option:selected').val();
            $("#<%=hdnZipId.ClientID%>").val(id);
            return false;
        }--%>
    </script>
    <script id="CitySearch_template" type="text/html">
        <label class="autocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <script>
        $(document).ready(function () {
            var timeout;
            $('#txtSearchCity').keyup(function () {
                clearTimeout(timeout);
                var txtval = $(this).val();
                if (txtval.length > 1) {
                    $("#divLoader").show();
                    timeout = setTimeout(function () { GetLocation(txtval); }, 1000);
                }
                else {
                    $("#div_SearchContent").empty();
                    $("#divLoader").hide();
                }
            });
        });
        function SetLocation(btn) {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtSearchCity").val(text);
            $("#hdnLocationId").val(id);
            $("#div_SearchContent").css("display", "none");
            $("#div_SearchContent").empty();
        }
        function GetLocation(searchContent) {
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
                                $("#divLoader").hide();
                                $("#div_SearchContent").empty();
                                $("#div_SearchContent").hide();
                                $("#CitySearch_template").tmpl(result.d).appendTo("#div_SearchContent");
                                $("#div_SearchContent").slideDown('slow');
                            }
                        }
                        else {
                            $("#divLoader").hide();
                            $("#div_SearchContent").empty();
                            $("#div_SearchContent").html('<label class="autocomplete_section">No Search Result Found!</label>');
                            $("#div_SearchContent").slideDown('slow');
                        }
                    }
                });
            }
            else {
                $("#divLoader").hide();
                $("#div_SearchContent").css("display", "none");
                $("#div_SearchContent").empty();
            }
        }
    </script>
</asp:Content>
