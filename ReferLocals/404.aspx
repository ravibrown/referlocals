<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="ReferLocals._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>404 Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <!--<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.css" rel="stylesheet">
<link href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.2.3/css/simple-line-icons.min.css" rel="stylesheet" type="text/css" /> -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <link href="/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->

    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="/css/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="/css/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="/css/error.min.css" rel="stylesheet" type="text/css" />

    <style>
        .text404_div {
            float: left;
        }

        .p_404 {
            float: left;
            width: 70%;
            margin: 45px 0 0;
            text-align: justify;
        }

        .main404 {
            width: 70%;
            text-align: justify;
        }

        .input404 {
            float: left;
            width: 100%;
            margin: 15px 0 0;
        }

        .v_eror .btn.red.btn-outline {
            background: rgba(255, 255, 255, 0.41) none repeat scroll 0 0;
            color: #fff;
        }

            .v_eror .btn.red.btn-outline:hover {
                background: #169ef4;
                color: #fff;
            }

        .input404 .btn.green {
            background: #169ef4 none repeat scroll 0 0;
        }

            .input404 .btn.green:hover {
                background: #169ef4 none repeat scroll 0 0;
                text-shadow: 1px 1px 1px #000;
            }
    </style>

    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<body class=" page-404-3">

    <div class="page-inner">
        <img src="/images/earth.jpg" class="img-responsive" alt="">
    </div>

    <div class="container error-404 v_eror">
        <h1 style="font-size: 100px; line-height: 100px">404 Error</h1>
        <!-- <h2>Houston, we have a problem.</h2>-->
        <p class="main404">We have encountered some unexpected error, In the meantime you can take following actions .</p>
        <p>
            <a href="/index" class="btn red btn-outline">Return home </a>
            <br>
        </p>

        <div class="text404_div">
            <p class="p_404">We do send great reads, money saving ideas and offers once in a while. Subscribe to our Newsletter </p>
            <div class="input404">
                <form id="form1" runat="server" class="form-inline">
                    <div style="clear:both;">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a"
                            runat="server" ErrorMessage="Please enter valid email" SetFocusOnError="true"
                            Text="Please enter valid email" ForeColor="Red" ControlToValidate="txtEmail" ValidateRequestMode="Enabled"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredValidator2" ValidationGroup="a"
                            runat="server" ErrorMessage="Please enter email" SetFocusOnError="true"
                            Text="Please enter email" ForeColor="Red" ControlToValidate="txtEmail" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>
                    </div>
                    <div style="width: 75% !important" class="input-group input-group-lg input-large">
                        <asp:TextBox ID="txtEmail" runat="server" type="text"
                            ValidationGroup="a" ValidateRequestMode="Enabled" CausesValidation="true"
                            placeholder="Enter E-mail" CssClass="form-control"></asp:TextBox>


                        <span class="input-group-btn">
                            <asp:Button ID="btnSubscribe" runat="server" ValidationGroup="a" CausesValidation="true"
                                Style="text-transform: capitalize; font-size: 16px;" CssClass="btn green"
                                Text="Subscribe" OnClick="btnSubscribe_Click" />
                        </span>
                    </div>
                </form>
            </div>
        </div>

    </div>

    <!-- BEGIN CORE PLUGINS -->
    <script src="/js/jquery.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/js/js.cookie.min.js" type="text/javascript"></script>
    <!--<script src="js/bootstrap-hover-dropdown.min.js" type="text/javascript"></script> 
<script src="js/jquery.slimscroll.min.js" type="text/javascript"></script> 
<script src="js/jquery.blockui.min.js" type="text/javascript"></script> 
<script src="js/jquery.uniform.min.js" type="text/javascript"></script> 
<script src="js/bootstrap-switch.min.js" type="text/javascript"></script> -->
    <!-- END CORE PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="/js/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <!-- END THEME LAYOUT SCRIPTS -->
</body>
</html>
