<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddHomeCard.aspx.cs" Inherits="ReferLocals.Admin.AddHomeCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile.min.css" rel="stylesheet" type="text/css" />
    <script>

        function AddNewHomeCard() {
            $('#<%=chkHomeCard.ClientID%>').prop("checked", false);
            $('.checker span').removeClass('checked');
            $('#<%=hdnId.ClientID%>').val('');
            $('#<%=drpPosition.ClientID%>').val('0');
            $('#<%=txtTitle.ClientID%>').val('');
            $('#<%=txtLink.ClientID%>').val('');
            $('.IconImage').empty();
            $('.SingleImage').empty();
            $(".divHomeCardRecords").css("display", "none");
            $(".divAddHomeCard").css("display", "block");
            return false;
        }

        function EditHomeCards(btn) {
            var id = $(btn).attr('data-id');
            var title = $(btn).attr('data-title');
            var link = $(btn).attr('data-link');
            var image = $(btn).attr('data-image');
            var iconimage = $(btn).attr('data-iconimage');
            var position = $(btn).attr('data-position');
            var isapprovedbyadmin = $(btn).attr('data-isapprovedbyadmin');
            if (isapprovedbyadmin == "True") {
                $('#<%=chkHomeCard.ClientID%>').prop("checked", true);
                $('.checker span').addClass('checked');
            }
            $('#<%=hdnId.ClientID%>').val(id);
            $('#<%=txtTitle.ClientID%>').val(title);
            $('#<%=txtLink.ClientID%>').val(link);
            $('#<%=drpPosition.ClientID%>').val(position);
            $('.IconImage').append('<img src="<%=DataAccess.HelperClasses.Common.HomeCardIconImagePath%>' + iconimage + '"/>');
            $('.SingleImage').append('<img src="<%=DataAccess.HelperClasses.Common.HomeCardImagePath%>' + image + '"/>')
            $(".divHomeCardRecords").css("display", "none");
            $(".divAddHomeCard").css("display", "block");
        }
        function CheckValidation() {
            var Title = $('#<%=txtTitle.ClientID%>').val();
                var Link = $('#<%=txtLink.ClientID%>').val();
                var Position = $('#<%=drpPosition.ClientID%> option:selected').val();
                var ErrorMessage = "";

                if (Title == "") {
                    if (ErrorMessage == "") {
                        ErrorMessage = "Please enter title.";
                    }
                    $('#<%=txtTitle.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtTitle.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Link == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter link.";
                }
                $('#<%=txtLink.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtLink.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Position == "0") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select position.";
                }
                $('#<%=drpPosition.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=drpPosition.ClientID%>').css("border-color", "#c2cad8");
            }

            if (ErrorMessage != "") {
                alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />

    <div class="page-content divHomeCardRecords">
        <div class="alert alert-danger hide" id="alertDanger" runat="server">
            <asp:Label ID="lblErrorMsg" Text="" runat="server" />
        </div>
        <div class="alert alert-success hide" id="alertSuccess" runat="server">
            <asp:Label ID="lblSuccessMsg" Text="" runat="server" />
        </div>
        <!-- BEGIN PAGE HEADER-->
        <!-- BEGIN PAGE BAR -->
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li><a href="index.html">Dashboard</a> <i class="fa fa-angle-right"></i></li>
                <li>Home Cards</li>
            </ul>
        </div>
        <!-- END PAGE BAR -->
        <!-- END PAGE HEADER-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption">Home Cards</div>
                        <div class="tools">
                            <a class="reload" href="javascript:;" data-original-title="" title=""></a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div id="sample_3_wrapper" class="dataTables_wrapper no-footer">

                            <div class="row rowPadding">
                                <div class="col-md-6 col-sm-12">
                                    <div class="btn-group btn-group-solid">
                                        <a class="btn btn-lg red" onclick="return AddNewHomeCard();"><i class="fa fa-plus"></i>Add New Cards </a>
                                        <br />
                                        <h3 class="card_n">Max. 4 cards can be added on home page </h3>                                    
                                    </div>
                                </div>
                            </div>
                            <div class="table-scrollable">
                                <table class="table table-striped table-hover table-bordered dataTable no-footer">
                                    <thead>
                                        <tr>
                                            <th># </th>
                                            <th>Image </th>
                                            <th>Icon Image </th>
                                            <th>Title </th>
                                            <th>Link </th>
                                            <th>Position </th>
                                            <th>Status </th>
                                            <th>Action </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptHomeCards" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("SNO") %> </td>
                                                    <td><a class="linktext" href="#">
                                                        <img src="<%=DataAccess.HelperClasses.Common.HomeCardImagePath%><%#Eval("Image") %>" alt="<%#Eval("Image") %>" style="width: 50px; height: 50px;" />
                                                    </a></td>
                                                    <td><a class="linktext" href="#">
                                                        <img src="<%=DataAccess.HelperClasses.Common.HomeCardIconImagePath%><%#Eval("IconImage") %>" alt="<%#Eval("IconImage") %>" style="width: 50px; height: 50px;" />
                                                    </a></td>
                                                    <td><%#Eval("Title") %> </td>
                                                    <td><a class="linktext" href='<%#Eval("Link") %>' target="_blank"><%#Eval("Link") %></a></td>
                                                    <td><%#Eval("Position") %></td>
                                                    <td><%#Eval("ISApprovedByAdmin") %></td>
                                                    <td class="ancherwidth ancherwidth2"><a onclick="EditHomeCards(this);" data-id="<%#Eval("Id")%>" data-position="<%#Eval("Position")%>" data-title="<%#Eval("Title")%>" data-link="<%#Eval("Link")%>" data-iconimage="<%#Eval("IconImage")%>" data-isapprovedbyadmin="<%#Eval("IsApprovedByAdmin")%>" data-image="<%#Eval("Image")%>" class="btn dark btn-sm btn-outline sbold uppercase"><i class="fa fa-edit"></i>Edit </a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END EXAMPLE TABLE PORTLET-->
            </div>
        </div>
    </div>
    <div class="divAddHomeCard" style="display: none !important;">
        <div class="page-content">
            <div class="page-bar">
                <ul class="page-breadcrumb">
                    <li><a href="index.html">Home</a> <i class="fa fa-angle-right"></i></li>
                    <li><a href="/Admin/AddHomeCard">Home Cards </a><i class="fa fa-angle-right"></i></li>
                    <li><span>Add New Card</span> </li>
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
                <h3 style="margin-bottom: 0px;">Add New Card</h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <!-- BEGIN PORTLET-->
                    <div class="portlet light form-fit bordered">

                        <div class="portlet-body">
                            <!-- BEGIN FORM-->
                            <div class="form-horizontal">
                                <div class="form-body">


                                    <div class="form-group">
                                        <label class="control-label col-md-3">Title <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtTitle" class="form-control" placeholder="Enter your Title" MaxLength="30" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Link <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtLink" class="form-control" placeholder="www.xyz.com" MaxLength="100" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Positions <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="drpPosition" class="form-control" aria-hidden="true">
                                                <asp:ListItem Text="Select" Value="0" />
                                                <asp:ListItem Text="1" Value="1" />
                                                <asp:ListItem Text="2" Value="2" />
                                                <asp:ListItem Text="3" Value="3" />
                                                <asp:ListItem Text="4" Value="4" />
                                            </asp:DropDownList>

                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label for="form_control_1" class="col-md-3 control-label" style="color: #333">Select Background Image</label>
                                        <div class="col-md-4">
                                            <div data-provides="fileinput" class="fileinput fileinput-new" style="width: 100%;">
                                                <div style="width: 100%; height: 150px;" data-trigger="fileinput" class="fileinput-preview thumbnail SingleImage"></div>
                                                <div>
                                                    <span class="btn red btn-outline btn-file"><span class="fileinput-new">Select Image </span><span class="fileinput-exists">Change </span>
                                                        <asp:FileUpload ID="FileImage" runat="server" />
                                                    </span><a data-dismiss="fileinput" class="btn red fileinput-exists" href="javascript:;">Remove </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="form_control_1" class="col-md-3 control-label" style="color: #333">Select Icon</label>
                                        <div class="col-md-4">
                                            <div data-provides="fileinput" class="fileinput fileinput-new" style="width: 100%;">
                                                <div style="width: 100%; height: 150px;" data-trigger="fileinput" class="fileinput-preview thumbnail IconImage"></div>
                                                <div>
                                                    <span class="btn red btn-outline btn-file"><span class="fileinput-new">Select Icon </span><span class="fileinput-exists">Change </span>
                                                        <asp:FileUpload ID="FileIconImage" runat="server" />
                                                    </span><a data-dismiss="fileinput" class="btn red fileinput-exists" href="javascript:;">Remove </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Active </label>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="chkHomeCard" Text="" runat="server" />

                                        </div>
                                    </div>

                                </div>
                                <div class="form-actions" style="margin-bottom: 25px;">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button Text="Submit" ID="btnSubmit" OnClick="btnSubmit_OnClick" OnClientClick="return CheckValidation();" runat="server" class="btn green" />
                                            <a href="/Admin/AddHomeCard" class="btn default">Cancel</a>
                                        </div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="/js/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="/js/profile.min.js" type="text/javascript"></script>
</asp:Content>
