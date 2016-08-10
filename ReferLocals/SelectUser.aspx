<%@ Page Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="SelectUser.aspx.cs" Inherits="ReferLocals.SelectUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="log_text_mid">
        <div class="container">
            <div class="blue_image_logo">
                <a href="#">
                    <img src="/images/blue_image_logo.png" alt="" />
                </a>
            </div>

            <div class="trackMe">
                <div class="dialog active" scopeid="signup-success">
                    <div class="header_choose">
                        <h1 class="header2_head">Which describes you best?</h1>
                    </div>
                    <div class="body_choose">
                        <div class="row">
                            <div class="col-sm-6 homeownerV" id="homeowner" onclick="RedirectSection('User');">
                                <div class="wrapper_chose">
                                    <div class="icon"></div>
                                    <div class="title header-5">User</div>
                                    <div class="description">I am a user or looking for Professional</div>
                                </div>
                            </div>
                            <div compid="pro" class="col-sm-6" id="pro" onclick="RedirectSection('Professional');">
                                <div class="wrapper_chose">
                                    <div class="icon"></div>
                                    <div class="title header-5">Professional</div>
                                    <div class="description">I am a Professional offering my services</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        function RedirectSection(type)
        {
            var id = '<%=Id%>';
            if(type=="User")
            {
                window.location.href = "/UserSection?Id="+id;
            }
            else {
                window.location.href = "/ProfessionalSection?Id="+id;
            }
        }
    </script>
</asp:Content>
