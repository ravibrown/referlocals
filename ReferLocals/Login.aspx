
<%@ Page Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ReferLocals.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="log_text_mid">
        <div class="container">
            <h1><span>Welcome </span>to Referlocals™ </h1>
            <h3>Register with us today. Its Free!</h3>
            
        </div>
    </div>
    <!-- BEGIN LOGIN -->
    <div class="content v_content1">
        <div class="login-form">
            <div class="alert alert-danger display-hide">
                <button class="close" data-close="alert"></button>
                <span>Enter Email/Phone Number and password. </span>
            </div>
            <div class="form-group dform-group">
                <%--<label class="control-label">Email</label>--%>
                <asp:TextBox ID="txtEmail" MaxLength="100" class="form-control form-control-solid placeholder-no-fix" autocomplete="off" placeholder="Email" runat="server" />

            </div>
                <p style="text-align:center;margin-bottom: 0px !important;">OR</p>
            
            <div class="form-group dform-group" style="float: left; width: 100%; margin-bottom: 30px;">
                <%--<label class="control-label">Phone Number</label>--%>
                <asp:DropDownList ID="drpCountryCode" class="form-control form-control-solid placeholder-no-fix" Style="width: 15%; float: left;" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtPhone" onkeypress="return isNumberKey(event)" MaxLength="16" class="form-control form-control-solid placeholder-no-fix" autocomplete="off" Style="width: 84%; float: left; margin-left: 1%;" placeholder="PhoneNumber" runat="server" />

            </div>
            <div class="form-group dform-group">
                <%--<label class="control-label">Password</label>--%>
                <asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="20" class="form-control form-control-solid placeholder-no-fix" placeholder="Password" runat="server" />
            </div>
            <div class="form-actions" style="border: none !important">
                <asp:Button ID="btnLogin" Text="Login" ClientIDMode="Static" 
                    class="btn green uppercase bBluebtn" 
                    OnClick="btnLogin_OnClick" OnClientClick="return CheckValidation('Login');" runat="server" />
                <%--<button class="btn btn-primary btn-lg loginBtnSize" type="button"><i class="fa fa-facebook-f "></i>Login With Facebook</button>--%>
                <a href="javascript:;" id="forget-password" class="forget-password fgp">Forgot Password?</a>
            </div>
            <div class="create-account create-account1">
                <p>Dont have any account yet? &nbsp;<a href="#" id="register-btn" class="uppercase">Register Now</a> </p>
            </div>
        </div>


        <!-- END LOGIN FORM -->

        <!-- BEGIN FORGOT PASSWORD FORM -->
        <div class="forget-form">
            <h3 class="font-green" style="color: #0094d2 !important">Forget Password ?</h3>
            <p>Enter your E-mail address or Phone Number below to reset your password. </p>
            <div class="form-group">
                <asp:TextBox ID="txtForgetEmail" MaxLength="100" class="form-control placeholder-no-fix" autocomplete="off" placeholder="Email" runat="server" />
            </div>
             <p style="text-align:center;">OR</p>
            <div class="form-group" style="float: left; width: 100%; margin-bottom: 30px;">
                <asp:DropDownList ID="drpForgetCountryCode" class="form-control form-control-solid placeholder-no-fix" Style="width: 15%; float: left;" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtForgetPhone" onkeypress="return isNumberKey(event)" MaxLength="16" class="form-control placeholder-no-fix" autocomplete="off" placeholder="Phone Number" Style="width: 84%; float: left; margin-left: 1%;" runat="server" />
            </div>
            <div class="form-actions">
                <asp:Button ID="btnForget" Text="Continue" runat="server" OnClick="btnForget_OnClick" OnClientClick="return CheckValidation('Forget');" class="btn green uppercase bBluebtn" />
            </div>

            <div class="create-account create-account1">
                <p>Know your password? &nbsp;<a class="uppercase" id="back-btn" href="javascript:;">Login Now</a> </p>
            </div>

        </div>
        <!-- END FORGOT PASSWORD FORM -->

        <!-- BEGIN REGISTRATION FORM -->
        <div class="register-form">
            <div class="form-group dform-group">
                <label class="control-label">Email</label>
                <asp:TextBox ID="txtRegisterEmail" MaxLength="100" class="form-control form-control-solid placeholder-no-fix" autocomplete="off" placeholder="Email" runat="server" />
            </div>
             <p style="text-align:center;margin-bottom: 0px !important;">OR</p>
            <div class="form-group dform-group" style="float: left; width: 100%; margin-bottom: 30px;">
                <label class="control-label">Phone Number</label>
                <asp:DropDownList ID="drpRegisterCountryCode" class="form-control form-control-solid placeholder-no-fix" Style="width: 15%; float: left;" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtRegisterPhone" onkeypress="return isNumberKey(event)" MaxLength="16" class="form-control form-control-solid placeholder-no-fix" autocomplete="off" Style="width: 84%; float: left; margin-left: 1%;" placeholder="Phone Number" runat="server" />
            </div>
            <div class="form-group dform-group">
                <label class="control-label">Password</label>
                <asp:TextBox ID="txtRegisterPassword" TextMode="Password" MaxLength="20" class="form-control form-control-solid placeholder-no-fix" placeholder="Password" runat="server" />
            </div>
            <div class="form-group margin-top-20 margin-bottom-20">
                <label class="check">
                    <asp:CheckBox ID="chkAgree" Text="" runat="server" />
                    I agree to the <a href="javascript:;">Terms and Conditions </a>
                </label>
                <div id="register_tnc_error"></div>
            </div>
            <div class="form-actions form-actions1">
                <asp:Button ID="btnRegister" Text="Continue" runat="server" OnClick="btnRegister_OnClick" OnClientClick="return CheckValidation('Register');" class="btn green uppercase" />
                <%--<button type="button" class="btn btn-primary btn-lg loginBtnSize"><i class="fa fa-facebook-f "></i>Register With Facebook</button>--%>
            </div>
            <div class="create-account create-account1">
                <p>Already have any account? &nbsp; <a id="register-back-btn" class="uppercase" href="javascript:;">Login</a> </p>
            </div>
        </div>
        <!-- END REGISTRATION FORM -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">

        $(document).keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                $('#btnLogin').click();
                return false;
            }
        });
        $(document).ready(function () {
            if (window.location.hash.toString().indexOf("register")>0)
            {
                $("#register-btn").click();
            }
            $(".login-form").keypress(function (e) {
                if (e.keyCode === 13) {
                    $('#<%=btnLogin.ClientID%>').trigger("click")

                    }
                });

                $(".forget-form").keypress(function (e) {
                    if (e.keyCode === 13) {
                        $('#<%=btnForget.ClientID%>').trigger("click")

                    }
                });
                $(".register-form").keypress(function (e) {
                    if (e.keyCode === 13) {
                        $('#<%=btnRegister.ClientID%>').trigger("click")

                    }
                });
            });

            function CheckValidation(type) {
                var ErrorMessage = "";
                if (type == "Login") {
                    var Email = $("#<%=txtEmail.ClientID%>").val();
                    var Phone = $("#<%=txtPhone.ClientID%>").val();
                    var Password = $("#<%=txtPassword.ClientID%>").val();

                    if (Email == "" && Phone == "") {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Please enter email or phone number.";
                        }
                        $('#<%=txtEmail.ClientID%>').css("border-color", "red");
                        $('#<%=txtPhone.ClientID%>').css("border-color", "red");
                    }
                    else {
                        if (!IsEmail(Email) && Email != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid email";
                            }
                            $('#<%=txtEmail.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtEmail.ClientID%>').css("border-color", "#c2cad8");
                        }

                        if (!ValidateUSPhoneNumber(Phone) && Phone != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid phone number";
                            }
                            $('#<%=txtPhone.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtPhone.ClientID%>').css("border-color", "#c2cad8");
                        }
                    }

                    if (Password == "") {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Please enter password.";
                        }
                        $('#<%=txtPassword.ClientID%>').css("border-color", "red");
                    }
                    else {
                        $('#<%=txtPassword.ClientID%>').css("border-color", "#c2cad8");
                    }
                }
                else if (type == "Forget") {
                    var Email = $("#<%=txtForgetEmail.ClientID%>").val();
                    var Phone = $("#<%=txtForgetPhone.ClientID%>").val();
                    if (Email == "" && Phone == "") {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Please enter email or phone number.";
                        }
                        $('#<%=txtForgetEmail.ClientID%>').css("border-color", "red");
                        $('#<%=txtForgetPhone.ClientID%>').css("border-color", "red");
                    }
                    else {
                        if (!IsEmail(Email) && Email != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid email.";
                            }
                            $('#<%=txtForgetEmail.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtForgetEmail.ClientID%>').css("border-color", "#c2cad8");
                        }

                        if (!ValidateUSPhoneNumber(Phone) && Phone != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid phone number.";
                            }
                            $('#<%=txtForgetPhone.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtForgetPhone.ClientID%>').css("border-color", "#c2cad8");
                        }
                    }
                }
                else if (type == "Register") {
                    var Email = $("#<%=txtRegisterEmail.ClientID%>").val();
                    var Phone = $("#<%=txtRegisterPhone.ClientID%>").val();
                    var Password = $("#<%=txtRegisterPassword.ClientID%>").val();

                    if (Email == "" && Phone == "") {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Please enter email or phone number.";
                        }
                        $('#<%=txtRegisterEmail.ClientID%>').css("border-color", "red");
                        $('#<%=txtRegisterPhone.ClientID%>').css("border-color", "red");
                    }
                    else {
                        if (!IsEmail(Email) && Email != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid email.";
                            }
                            $('#<%=txtRegisterEmail.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtRegisterEmail.ClientID%>').css("border-color", "#c2cad8");
                        }

                        if (!ValidateUSPhoneNumber(Phone) && Phone != "") {
                            if (ErrorMessage == "") {
                                ErrorMessage = "Please enter valid phone number.";
                            }
                            $('#<%=txtRegisterPhone.ClientID%>').css("border-color", "red");
                        }
                        else {
                            $('#<%=txtRegisterPhone.ClientID%>').css("border-color", "#c2cad8");
                        }
                    }

                    if (Password == "") {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Please enter password.";
                        }
                        $('#<%=txtRegisterPassword.ClientID%>').css("border-color", "red");
                    }
                    else {
                        $('#<%=txtRegisterPassword.ClientID%>').css("border-color", "#c2cad8");
                    }

                    if ($('#<%=chkAgree.ClientID%>').prop("checked") == false) {
                        if (ErrorMessage == "") {
                            ErrorMessage = "Kindly check terms and conditions.";
                        }
                        $('#<%=chkAgree.ClientID%>').css("border-color", "red");
                    }
                    else {
                        $('#<%=chkAgree.ClientID%>').css("border-color", "#c2cad8");
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
    <%--<script src="/js/custom.js"></script>--%>
</asp:Content>
