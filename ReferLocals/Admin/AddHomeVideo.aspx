<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddHomeVideo.aspx.cs" Inherits="ReferLocals.Admin.AddHomeVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnId" />
    <div class="page-content">
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li><a href="index.html">Home</a> <i class="fa fa-angle-right"></i></li>
                <li><span>Home Video</span> </li>
            </ul>
        </div>
        <!-- END PAGE BAR -->
        <!-- BEGIN PAGE TITLE-->
        <h3 class="page-title">
            <!--Bootstrap File Input <small>advanced bootstrap file input examples</small>-->
        </h3>
        <!-- END PAGE TITLE-->
        <!-- END PAGE HEADER-->
        <div class="note note-success homesucess">
            <h3>Upload Your Video</h3>
            <p>Your video will be visible on <a href="Index.html" target="_blank">home</a> page </p>
        </div>
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN PORTLET-->
                <div class="portlet light form-fit bordered">
                    <div class="portlet-title">
                        <div class="caption"><i class="fa fa-video-camera font-green"></i><span class="caption-subject font-green sbold uppercase">Add Video</span> </div>
                        <%--<div class="actions">
                            <input type="checkbox" class="make-switch" checked data-on="success" data-on-color="success" data-off-color="warning" data-size="small">
                        </div>--%>
                    </div>
                    <div class="portlet-body form">
                        <!-- BEGIN FORM-->
                        <div class="form-horizontal form-bordered">
                            <div class="form-body">
                                <div class="form-group ">
                                    <label class="control-label col-md-3">Video Upload </label>
                                    <div class="col-md-9">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="fileinput-preview thumbnail" data-trigger="fileinput" style="width: 600px; height: 300px;">
                                                <asp:Label Text="" ID="lblVideo" runat="server" /></div>
                                            <div>
                                                <span class="btn red btn-outline btn-file"><span class="fileinput-new">Select Video </span><span class="fileinput-exists">Change </span>
                                                    <asp:FileUpload ID="FileVideo" runat="server" />
                                                </span><a href="javascript:;" class="btn red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9"><asp:Button Text="Submit" ID="btnSubmit" OnClick="btnSubmit_OnClick" OnClientClick="return CheckValidation();" runat="server" class="btn green" /> <a href="/Admin/AddHomeVideo" class="btn btn-outline grey-salsa redlink">Cancel</a> </div>
                                </div>
                            </div>
                        </div>
                        <!-- END FORM-->
                    </div>
                </div>
                <!-- END PORTLET-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="/js/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="/js/profile.min.js" type="text/javascript"></script>
</asp:Content>
