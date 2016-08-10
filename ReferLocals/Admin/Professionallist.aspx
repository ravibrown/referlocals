<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Professionallist.aspx.cs" Inherits="ReferLocals.Admin.Professionallist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <!-- BEGIN PAGE HEADER-->

        <!-- BEGIN PAGE BAR -->
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li><a href="index.html">Dashboard</a> <i class="fa fa-angle-right"></i></li>
                <li><a href="#">User list</a> <i class="fa fa-angle-right"></i></li>
                <li><span>Datatables</span> </li>
            </ul>
        </div>
        <!-- END PAGE BAR -->
        <!-- BEGIN PAGE TITLE-->
        <h3 class="page-title">Registered user list
            <!--<small>editable datatable samples</small>-->
        </h3>
        <!-- END PAGE TITLE-->
        <!-- END PAGE HEADER-->
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet light portlet-fit bordered">
                    <div class="portlet-body">
                        <div id="sample_3_wrapper" class="dataTables_wrapper no-footer">

                          
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
                                                    
                                                </asp:DropDownList>
                                                entries</label>
                                        </div>

                                    </div>
                                    <div class="col-md-6 col-sm-12">
                                        <div id="sample_3_filter" class="dataTables_filter">
                                            <label>
                                                Search:
                <input type="search" class="form-control input-sm input-small input-inline" id="txtSearch" placeholder="" aria-controls="sample_3" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-scrollable">
                                    <table class="table table-striped table-hover table-bordered dataTable no-footer">
                                        <thead>
                                            <tr>
                                                <th># </th>
                                                
                                                <th>Name </th>
                                                <th>Profession</th>
                                                <th>Email </th>
                                                <th class="auto-style1">Phnone Number </th>
                                                <th>Address </th>
                                                <th>Cities Serving </th>
                                                <th>Action </th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyCategories">
                                            <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr data-name="<%#Eval("Id") %>">
                                                        <td><%#Container.ItemIndex+1%> </td>
                                                        <td><%#Eval("FirstName") %> <%#Eval("LastName") %></td>
                                                        <td>
                                                            <asp:Repeater ID="rptProfessions"  runat="server" DataSource='<%#Eval("ProfessionalUrls") %>'>
                                                            <ItemTemplate><%#Eval("SubCategoryName") %><br /></ItemTemplate>
                                                            </asp:Repeater></td>
                                                        <td><%#Eval("Email") %></td>
                                                        <td><%#!string.IsNullOrEmpty(Convert.ToString(Eval("PhoneNumber")))?Convert.ToString(Eval("CountryCode")+"-"+Convert.ToString(Eval("Phonenumber"))):"" %></td>
                                                        <td class="tableaddress"><%#Eval("Appartment") %>, <%#Eval("StreetAddress") %>, <%#Eval("CityName") %></td>
                                                        <td>
                                                            
                                                            <asp:Repeater ID="rptCitiesIServe"  runat="server" >
                                                            <ItemTemplate><%#Eval("CityName")%> <%#Eval("StateName")%><br /></ItemTemplate>
                                                            </asp:Repeater></td>
                                                        <td class="ancherwidth"><a class="btn dark btn-sm btn-outline sbold uppercase" href="single_user.html"><i class="fa fa-share"></i>View </a><span class="flag"><a href="#"><i class="fa fa-flag"></i></a></span></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="row">
                                    <div class="col-md-5 col-sm-12">
                                        <div class="dataTables_info" id="sample_3_info" role="status" aria-live="polite">
                                            Showing <asp:Label ID="lblFrom" Text="1" runat="server" /> to
                                            <asp:Label ID="lblTo" Text="5" runat="server" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
