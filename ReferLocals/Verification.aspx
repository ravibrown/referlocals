<%@ Page Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="ReferLocals.Verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="log_text_mid">
        <div class="container">
            <h1><span>Welcome </span>to Refer locals </h1>
            <h3>" Enter your 6 digit Verification Code " </h3>
        </div>
    </div>
    <!-- BEGIN LOGIN -->
    <div class="content v_content1">
        <div class="login-form">
            <div class="form-group dform-group_v">
                <label class="control-label">Email or Phone Number</label>
                <asp:TextBox ID="txtEmailOrPhone" class="form-control form-control-solid placeholder-no-fix form-control-solid_v " autocomplete="off" ReadOnly="true" runat="server" />

            </div>
            <div class="form-group dform-group_v">
                <label class="control-label">Verification Code</label>
                <asp:TextBox ID="txtOTP" MaxLength="6" TextMode="Password" onkeypress="return isNumberKey(event)" class="form-control form-control-solid placeholder-no-fix" autocomplete="off" placeholder="OTP(One Time Password)" runat="server" />
            </div>
            <div class="form-actions form-actions1">
                <asp:Button ID="btnVerify" class="btn green uppercase" OnClick="btnVerify_OnClick" OnClientClick="return CheckValidation();" Text="Verify" runat="server" />
                <asp:Button ID="btnResend" class="btn green uppercase" OnClick="btnResend_OnClick" Text="Resend" runat="server" />
            </div>
            <div class="create-account create-account1">
                <p>Already have any account? &nbsp; <a id="#" class="uppercase" href="/Login">Login</a> </p>
            </div>
        </div>



        <!-- BEGIN REGISTRATION FORM -->

        <!-- END REGISTRATION FORM -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        function CheckValidation() {
            var ErrorMessage = "";
            var OTP = $("#<%=txtOTP.ClientID%>").val();
                var EmailOrPhone = $("#<%=txtEmailOrPhone.ClientID%>").val();

                if (EmailOrPhone == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter email or phone number.";
                    }
                    $('#<%=txtEmailOrPhone.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtEmailOrPhone.ClientID%>').css("border-color", "#c2cad8");
                }

                if (OTP == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter OTP";
                    }
                    $('#<%=txtOTP.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtOTP.ClientID%>').css("border-color", "#c2cad8");
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
