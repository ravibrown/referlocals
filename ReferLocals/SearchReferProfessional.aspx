<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchReferProfessional.aspx.cs" Inherits="ReferLocals.SearchReferProfessional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Referred a Professional2 HTML ====================================-->

    <div class="Referred Referred_search">
        <div class="container">
            <div class="search_h2">
                <h2 class="referBottem"><i class="fa fa-angle-double-right"></i>Search Professional By Information </h2>
            </div>
            <div class="pic_field">
                <div class="col-md-5" style="padding: 0">
                    <div class="image_login_width">
                        <img src="/images/bg_man.png" alt="" />
                        <div class="relativeReferrals">
                            <p>Your <span>Referral</span> is very </p>
                            <p><span>valuable </span></p>
                            <p>and we thank you </p>
                            <p>for taking this </p>
                            <p><span>initiative </span></p>
                        </div>
                    </div>
                </div>
                <div class="col-md-7">

                    <div class="image_login_form">
                        <h2>Enter Required Information </h2>
                        <div class="form-group">
                            <asp:TextBox ID="txtName" runat="server" placeholder="Name of Professional" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="drpCountryCode" class="form-control drp_control" Style="width: 15%; float: left;" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPhone" onkeypress="return isNumberKey(event)" runat="server" Style="width: 84%; float: left; margin-left: 1%;" placeholder="Phone Number" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control" />
                        </div>
                        <div class="btn_professional">
                            <asp:Button Text="Provide Referral" ID="btnSubmit" CssClass="btn blue btn-lg" OnClick="btnSubmit_OnClick" OnClientClick="return CheckValidation();" runat="server" />
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>



    <!--==================================== End Referred HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        function CheckValidation() {
            var ErrorMessage = "";
            var Phone = $("#<%=txtPhone.ClientID%>").val();
            var Email = $("#<%=txtEmail.ClientID%>").val();
            var Name = $("#<%=txtName.ClientID%>").val();

            if (Name == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter name";
                }
                $('#<%=txtName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtName.ClientID%>').css("border-color", "#c2cad8");
            }


           <%-- if (Email == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter email.";
                }
                $('#<%=txtEmail.ClientID%>').css("border-color", "red");
            }--%>
            if (Email != "") {
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

            if (Phone == "" && Email == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter Email or Phone number.";
                }
                $('#<%=txtPhone.ClientID%>').css("border-color", "red");
                $('#<%=txtEmail.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtEmail.ClientID%>').css("border-color", "#c2cad8");
                $('#<%=txtPhone.ClientID%>').css("border-color", "#c2cad8");
            }
            <%--if (Phone == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter phone number.";
                }
                $('#<%=txtPhone.ClientID%>').css("border-color", "red");
            }--%>
            if (Phone != "") {
                if (!ValidateUSPhoneNumber(Phone)) {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter valid phone number";
                    }
                    $('#<%=txtPhone.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtPhone.ClientID%>').css("border-color", "#c2cad8");
                }
            }
            
            if (ErrorMessage != "") {
                bootbox.alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
