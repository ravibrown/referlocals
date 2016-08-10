<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProfileImage.aspx.cs" Inherits="ReferLocals.ProfileImage" %>

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
                <div class="row profile-account">
                    <div class="col-md-3">
                        <ul class="ver-inline-menu tabbable margin-bottom-10">
                            <li><a href="/Profile"><i class="fa fa-cog"></i>Personal info </a><span class="after"></span></li>
                            <li class="active"><a href="/ProfileImage"><i class="fa fa-picture-o"></i>Change Image </a></li>
                            <li><a href="/ChangePassword"><i class="fa fa-lock"></i>Change Password </a></li>
                        </ul>
                    </div>
                    <div class="col-md-9">
                        <div class="tab-content">
                            <div class="tab-pane active">
                                <div>
                                    <div class="form-group image_label">
                                        <label>Change Image</label>
                                        <div class="col-md-12">
                                            <div data-provides="fileinput" class="fileinput fileinput-new" style="width: 100%;">
                                                <div style="width: 100%; height: 150px;" data-trigger="fileinput" class="fileinput-preview thumbnail"></div>
                                                <div>
                                                    <span class="btn red btn-outline btn-file"><span class="fileinput-new">Select Image </span><span class="fileinput-exists">Change </span>
                                                        <asp:FileUpload ID="FileImage" runat="server" />
                                                    </span><a data-dismiss="fileinput" class="btn red fileinput-exists" href="javascript:;">Remove </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="editProfileBtn">
                                        <asp:Button ID="btnSave" CssClass="btn blue btn-lg" OnClick="btnSave_OnClick" Text="Save Changes" runat="server" />
                                        <button class="btn  btn-default btn-lg" type="button">Cancel</button>
                                    </div>
                                    <div class="div_imageSection">
                                        <div class="col-md-12">
                                            <h3>Uploaded images </h3>
                                        </div>
                                        <asp:Repeater ID="rptImages" runat="server" OnItemCommand="rptImages_OnItemCommand">
                                            <ItemTemplate>
                                                <div class="col-md-3">
                                                    <img src="<%=DataAccess.HelperClasses.Common.UserImagesPath%><%#Eval("Image") %>?height=180&width=180&mode=crop" alt="<%#Eval("Image") %>" />
                                                    <div class="imagetextimage">
                                                        <asp:LinkButton Text="Set as Profile Picture" CssClass="setprofilepic" CommandArgument='<%#Eval("Id") %>' CommandName="SetProfilePicture"  runat="server" />
                                                        <asp:LinkButton Text="Delete" CssClass="setprofilepic" CommandArgument='<%#Eval("Id") %>' CommandName="Delete" runat="server" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
    <link href="css/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-fileinput.js" type="text/javascript"></script>
</asp:Content>
