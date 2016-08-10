<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ReferLocals.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="row profile-account CP_account">
                    <div class="col-md-3">
                        <ul class="ver-inline-menu tabbable margin-bottom-10">
                            <li><a href="/Profile"><i class="fa fa-cog"></i>Personal info </a><span class="after"></span></li>
                            <li ><a href="/ProfileImage"><i class="fa fa-picture-o"></i>Change Image </a></li>
                            <li  class="active"><a href="/ChangePassword"><i class="fa fa-lock"></i>Change Password </a></li>
                        </ul>
                    </div>
                    <div class="col-md-9">
                        <div class="tab-content">
                            <div class="tab-pane active">
                                <div>
                                    <div class="form-group">
                                        <label class="control-label">Current Password </label>
                                        <asp:TextBox TextMode="Password" runat="server" ID="txtOldPassword" class="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label">New Password </label>
                                       <asp:TextBox TextMode="Password" runat="server" ID="txtNewPassword" class="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label">Confirm Password </label>
                                         <asp:TextBox TextMode="Password" runat="server" ID="txtConfirmPassword" class="form-control" />
                                    </div>


                                    <div class="editProfileBtn">
                                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_OnClick" OnClientClick="return CheckValidation();"  CssClass="btn blue btn-lg" Text="Change Password" runat="server" /> 
                                        <a href="/ChangePassword" class="btn  btn-default btn-lg">Cancel</a>
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
    <script>
        function CheckValidation() {
            var oldpassword = $('#<%=txtOldPassword.ClientID%>').val();
            var newpassword = $('#<%=txtNewPassword.ClientID%>').val();
            var confirmpassword = $('#<%=txtConfirmPassword.ClientID%>').val();
            var ErrorMessage = "";

            if (oldpassword == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter current password.";
                }
                $('#<%=txtOldPassword.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtOldPassword.ClientID%>').css("border-color", "#c2cad8");
            }

            if (newpassword == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter new password.";
                }
                $('#<%=txtNewPassword.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtNewPassword.ClientID%>').css("border-color", "#c2cad8");
            }

            if (confirmpassword == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter confirm password.";
                }
                $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "#c2cad8");
            }

            if (confirmpassword != "" && newpassword != "" && confirmpassword != newpassword) {
                if (ErrorMessage == "") {
                    ErrorMessage = "Confirm password and new password doesn't matched.";
                }
                $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtConfirmPassword.ClientID%>').css("border-color", "#c2cad8");
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
