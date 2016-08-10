<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddSubCategory.aspx.cs" Inherits="ReferLocals.Admin.AddSubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile.min.css" rel="stylesheet" type="text/css" />
    <script>
        function EditSubCategory(btn) {
            var id = $(btn).attr('data-id');
            var description = $(btn).attr('data-description');
            var name = $(btn).attr('data-name');
            var categoryid = $(btn).attr('data-categoryid');
            var image = $(btn).attr('data-image');
            var isapprovedbyadmin = $(btn).attr('data-isapprovedbyadmin');
            if (isapprovedbyadmin == "True") {
                $('#<%=chkCategory.ClientID%>').prop("checked", true);
                $('.checker span').addClass('checked');
            }

            $('#<%=hdnId.ClientID%>').val(id);
            $('#<%=drpCategory.ClientID%>').val(categoryid);
            $('#<%=txtName.ClientID%>').val(name);
            $('#<%=txtDescription.ClientID%>').val(description);
            $('.IconImage').append('<img src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath%>' + image + '"/>');
            $("#<%=divGridSubCategory.ClientID%>").attr("style", "display:none !important");
            $("#<%=divAddSubCategory.ClientID%>").attr("style", "display:block !important");
        }

        function CheckValidation() {
            var Name = $('#<%=txtName.ClientID%>').val();
            var Description = $('#<%=txtDescription.ClientID%>').val();
            var Category = $('#<%=drpCategory.ClientID%> option:selected').val();
            var ErrorMessage = "";

            if (Name == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter name.";
                }
                $('#<%=txtName.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtName.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Category == "0") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please select category.";
                }
                $('#<%=drpCategory.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=drpCategory.ClientID%>').css("border-color", "#c2cad8");
            }

            if (Description == "") {
                if (ErrorMessage == "") {
                    ErrorMessage = "Please enter description.";
                }
                $('#<%=txtDescription.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtDescription.ClientID%>').css("border-color", "#c2cad8");
            }

            if (ErrorMessage != "") {
                alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }

        function AddSubCategory() {
            $('#<%=chkCategory.ClientID%>').prop("checked", false);
            $('.checker span').removeClass('checked');
            $('#<%=drpCategory.ClientID%>').val('<%=CategoryId%>');
            $('#<%=hdnId.ClientID%>').val('');
            $('#<%=txtName.ClientID%>').val('');
            $('#<%=txtDescription.ClientID%>').val('');
            $('.IconImage').empty();
            $("#<%=divGridSubCategory.ClientID%>").attr("style", "display:none !important");
            $("#<%=divAddSubCategory.ClientID%>").attr("style", "display:block !important");
            return false;
        }

        function DisableSubCategory(btn) {
            var id = $(btn).attr('data-id');
            var Approved = $(btn).attr('data-isapprovedbyadmin');
            $.ajax({
                url: '/Admin/AddSubCategory.aspx/DisableSubCategory',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{ "Id":"' + parseInt(id) + '","Approved":"' + Approved + '"}',
                success: function (data) {
                    alert(data.d);
                    window.location.href = window.location.href;
                },
                error: function () {
                    alert("server error");
                }
            });
        }
    </script>
    <style>
        .active {
            z-index: 3;
            color: #fff !important;
            background-color: #337ab7 !important;
            border-color: #337ab7 !important;
            cursor: default !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnId" />
    <div id="divGridSubCategory" runat="server">
        <div class="page-content">
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
                    <li><span>Sub Category List</span> </li>
                </ul>
            </div>
            <!-- END PAGE BAR -->
            <!-- END PAGE HEADER-->
            <div class="row">
                <div class="col-md-12">
                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                    <div class="portlet box green">
                        <div class="portlet-title">
                            <div class="caption">Sub Category List </div>
                            <div class="tools">
                                <a class="reload" href="javascript:;" data-original-title="" title=""></a>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div id="sample_3_wrapper" class="dataTables_wrapper no-footer">

                                <div class="row rowPadding">
                                    <div class="col-md-6 col-sm-12">
                                        <div class="btn-group btn-group-solid">
                                            <a class="btn btn-lg red" href="/Admin/AddCategory"><i class="fa fa-plus"></i>Add New Category </a>
                                            <a class="btn btn-lg green" style="cursor: pointer" onclick="return AddSubCategory();"><i class="fa fa-link"></i>Add New Sub-Category </a>
                                        </div>

                                    </div>
                                </div>
                                <div id="divTableShow" runat="server">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12">
                                            <div class="dataTables_length" id="sample_3_length">
                                                <label>
                                                    <asp:DropDownList ID="drpPage" OnSelectedIndexChanged="drpPage_OnSelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Text="5" Value="5" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="15" Value="15" />
                                                        <asp:ListItem Text="20" Value="20" />
                                                        <asp:ListItem Text="All" Value="-1" />
                                                    </asp:DropDownList>
                                                    entries</label>
                                            </div>

                                        </div>
                                        <div class="col-md-6 col-sm-12">
                                            <div id="sample_3_filter" class="dataTables_filter">
                                                <label>
                                                    Search:
                <input type="search" class="form-control input-sm input-small input-inline" placeholder="" aria-controls="sample_3" />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-scrollable">
                                        <table class="table table-striped table-hover table-bordered dataTable no-footer">
                                            <thead>
                                                <tr>
                                                    <th># </th>
                                                    <th>Sub Category ID </th>
                                                    <th>Image </th>
                                                    <th>Name </th>
                                                    <th>Category </th>
                                                    <th>Description </th>
                                                    <th>Status </th>
                                                    <th>Action </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptCategories" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("SNO") %> </td>
                                                            <td><a class="linktext" href="#"><%#Eval("Id") %> </a></td>
                                                            <td><a class="linktext" href="#">
                                                                <img src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath %><%#Eval("Image") %>?width=50&height=50"" alt="<%#Eval("Image") %>" style="width: 50px; height: 50px;">
                                                            </a></td>
                                                            <td><a class="linktext" onclick="EditSubCategory(this);" data-id="<%#Eval("Id") %>" data-categoryid="<%#Eval("CategoryId") %>" data-name="<%#Eval("Name") %>" data-description="<%#Eval("Description") %>" data-image="<%#Eval("Image") %>" data-isapprovedbyadmin="<%#Eval("IsApprovedByAdmin") %>" style="cursor: pointer"><%#Eval("Name") %> </a></td>
                                                            <td><%#Eval("CategoryName") %></td>
                                                            <td class="word_limit word_limit2"><%#Eval("Description").ToString().Length > 30 ?Eval("Description").ToString().Substring(0,30)+"...":Eval("Description") %></td>
                                                            <td><%#Eval("IsApprovedByAdmin") %></td>
                                                            <td class="ancherwidth ancherwidth2"><a onclick="EditSubCategory(this);" data-id="<%#Eval("Id") %>" data-categoryid="<%#Eval("CategoryId") %>" data-name="<%#Eval("Name") %>" data-description="<%#Eval("Description") %>" data-image="<%#Eval("Image") %>" data-isapprovedbyadmin="<%#Eval("IsApprovedByAdmin") %>" style="cursor: pointer" class="btn dark btn-sm btn-outline sbold uppercase"><i class="fa fa-edit"></i>Edit </a>
                                                                <a style='cursor: pointer;' class="btn <%#(Convert.ToBoolean(Eval("IsApprovedByAdmin"))==true?"dark":"red")%>   btn-sm btn-outline sbold uppercase" onclick="return DisableSubCategory(this);" data-id="<%#Eval("Id") %>" data-isapprovedbyadmin="<%#Eval("IsApprovedByAdmin") %>"><i class="fa fa-trash-o"></i><%#(Convert.ToBoolean(Eval("IsApprovedByAdmin"))==true?"Disable":"Enable")%> </a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5 col-sm-12">
                                            <div class="dataTables_info" id="sample_3_info" role="status" aria-live="polite">
                                                Showing 1 to
                                            <asp:Label ID="lblTotalRecordShow" Text="5" runat="server" />
                                                of
                                        <asp:Label ID="lblTotalRecords" Text="0" runat="server" />
                                                entries
                                            </div>
                                        </div>
                                        <div class="col-md-7 col-sm-12">
                                            <div class="dataTables_paginate paging_bootstrap_number" id="sample_3_paginate">
                                                <ul class="pagination" style="visibility: visible;">
                                                    <%-- <li class="prev disabled"><a title="Prev" href="#"><i class="fa fa-angle-left"></i></a></li>--%>
                                                    <asp:Repeater ID="rptPager" runat="server" OnItemCommand="rptPager_OnItemCommand">
                                                        <ItemTemplate>
                                                            <li>
                                                                <asp:LinkButton ID="lnkPaging" CssClass='<%#Eval("Text").ToString()=="1"?"active":"" %>' Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="Paging" runat="server" />
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <%--<li class="next"><a title="Next" href="#"><i class="fa fa-angle-right"></i></a></li>--%>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divNoRecords" runat="server">
                                    No Records
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>
            </div>
        </div>
    </div>

    <div id="divAddSubCategory" runat="server" style="display: none;">
        <div class="page-content">
            <div class="page-bar">
                <ul class="page-breadcrumb">
                    <li><a href="index.html">Home</a> <i class="fa fa-angle-right"></i></li>
                    <li><a href="/Admin/AddCategory">Category List</a> <i class="fa fa-angle-right"></i></li>
                    <li><span>Add New Sub Category</span> </li>
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
                <h3 style="margin-bottom: 0px;">Add New Sub Category</h3>
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
                                        <label class="control-label col-md-3">Name <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" MaxLength="50" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Category <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="drpCategory" class="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Description <span aria-required="true" class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDescription" class="form-control" runat="server" MaxLength="200" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="form_control_1" class="col-md-3 control-label" style="color: #333">Image</label>
                                        <div class="col-md-4">
                                            <div data-provides="fileinput" class="fileinput fileinput-new" style="width: 100%;">
                                                <div style="width: 100%; height: 150px;" data-trigger="fileinput" class="fileinput-preview thumbnail IconImage"></div>
                                                <div>
                                                    <span class="btn red btn-outline btn-file"><span class="fileinput-new">Select Image </span><span class="fileinput-exists">Change </span>
                                                        <asp:FileUpload ID="FileImage" runat="server" />
                                                    </span><a data-dismiss="fileinput" class="btn red fileinput-exists" href="javascript:;">Remove </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Active </label>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="chkCategory" runat="server" />
                                        </div>
                                    </div>


                                </div>
                                <div class="form-actions" style="margin-bottom: 25px;">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button Text="Submit" ID="btnSubmit" OnClick="btnSubmit_OnClick" OnClientClick="return CheckValidation();" runat="server" class="btn green" />
                                            <a href="/Admin/AddCategory" class="btn default">Cancel</a>
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
