<%@ Page Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ReferLocals.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="log_text_mid">
        <div class="container">
            <h1><span>Reset </span>Your Password </h1>
            <h5>" A strong password is a combination of letters and punctuation marks. Its must be at last 6 Charcters long " </h5>
        </div>
    </div>
    <!-- BEGIN LOGIN -->
    <div class="content v_content1">
        <div class="login-form">
            <div class="form-group dform-group_v">
                <label class="control-label">New Password</label>
                <asp:TextBox ID="txtNewPassword" class="form-control form-control-solid placeholder-no-fix" TextMode="Password" autocomplete="off" placeholder="New Password" runat="server" />
            </div>
            <div class="form-group dform-group_v">
                <label class="control-label">Confirm Password </label>
                <asp:TextBox ID="txtConfirmPassword" class="form-control form-control-solid placeholder-no-fix" TextMode="Password" autocomplete="off" placeholder="Confirm Password" runat="server" />
            </div>
            <div class="form-actions form-actions1 forget_action">
                <asp:Button ID="btnConfirm" class="btn green uppercase" OnClick="btnConfirm_OnClick" OnClientClick="return CheckValidation();" Text="Confirm" runat="server" />

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
            var NewPassword = $("#<%=txtNewPassword.ClientID%>").val();
                var ConfirmPassword = $("#<%=txtConfirmPassword.ClientID%>").val();

                if (NewPassword == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter new password.";
                    }
                    $('#<%=txtNewPassword.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtNewPassword.ClientID%>').css("border-color", "#c2cad8");
                }

                if (ConfirmPassword == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter confirm password.";
                    }
                    $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "red");
                }
                else {
                    if (ConfirmPassword != NewPassword) {
                        if (ErrorMessage == "") {
                            ErrorMessage = "New password and confirm password didn't matched.";
                        }
                        $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "red");
                        $('#<%=txtNewPassword.ClientID%>').css("border-color", "red");
                    }
                    else {
                        $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "#c2cad8");
                        $('#<%=txtNewPassword.ClientID%>').css("border-color", "#c2cad8");
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
