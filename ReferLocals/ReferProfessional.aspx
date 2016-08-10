<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReferProfessional.aspx.cs" Inherits="ReferLocals.ReferProfessional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Referred HTML ====================================-->

    <div class="Referred Referred_search">
        <div class="container">
            <h1 class="main_maintext"><%if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/refer"))
                                        {%>
                Choose the Professional Type you want to refer
                <%}
                                        else if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/search") && !Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/searchjob"))
                                        {%>
                Choose the Professional Type you want to search
                <%}
                                        else if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/searchjob"))
                                        {%>
                Choose the Job Type you want to search
                <%} %>
            </h1>
            <div id="divCategory">
            </div>
        </div>
    </div>
    <!--==================================== End Referred HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">    
    <script id="CategoryScript" type="text/html">
        <div class="search_h2">
            <h2><i class="fa fa-angle-double-right"></i>${Name} </h2>
        </div>
        <div class="small_images" id="CheckView${Id}">
            {{tmpl(lst_subcategory) "#SubCategoryScript"}}
        </div>
        {{if SubCategoryCount > 4}}
        <div class="stepbtn">
            <button class="btn green uppercase lineblue" id="btnViewMore${Id}" data-id="${Id}" onclick="return ShowSubCategoryList(this);" type="submit">View More</button>
        </div>
         {{else}}
        {{/if}}
    </script>
    <script id="SubCategoryScript" type="text/html">
        <div class="col-md-3">
            <div  class="imageSmall divCat${CategoryId}">
                <img src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath %>${Image}?height=265&width=255&mode=crop" alt="${Image}" />
                <%if (CheckPage.ToString().ToLower() == "searchjob"){%>
                <div class="smallBlack smallBlack_v">
                    <h2 class="">${Name} <a href="/jobboard?type=${String(Name.replace(/ /g,"-"))}"></a></h2>
                </div>
                <%}else if (CheckPage.ToString().ToLower() == "search"){ %>
                   <div class="smallBlack smallBlack_v">
                    <h2 class="">${Name} <a href="/SearchResult?type=${String(Name.replace(/ /g,"-"))}"></a></h2>
                </div>
                <%}else{ %>
                   <div class="smallBlack smallBlack_v">                    
                    <h2 class="">${Name} <a href="/SearchReferProfessional/${String(Name.replace(/ /g,"-"))}"></a></h2>
                </div>
                <%} %>
            </div>
        </div>
    </script>
    <script>
        $(document).ready(function () {
            ShowCategoryList();
        });
        var take = 4;
        var index = 0;
        var subIndex = 1;
        function ShowCategoryList() {
            index = index + 1;
            $.ajax({
                url: '/ProfessionalSection.aspx/GetCategories',
                type: "POST",
                data: '{ "index":"' + parseInt(index - 1) + '","take":"' + parseInt(take) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.length > 0) {
                        if (index - 1 == 0) {
                            $("#divCategory").empty();
                        }
                        $("#CategoryScript").tmpl(result.d).appendTo("#divCategory");
                    }
                    else {
                        if (index - 1 == 0) {
                            $("#divCategory").html('<div style="width:100%;text-align:left;font-size:14px;font-weight:bold"><font color="black">No Record.</font></div>');
                        }
                    };
                }
            });
            return false;
        }

        function ShowSubCategoryList(btn) {
            var id = $(btn).attr("data-id");
            var countSubCats = $(".divCat" + id).length;
            subIndex =Math.floor(countSubCats / take);
            subIndex = subIndex + 1;
            $.ajax({
                url: '/ProfessionalSection.aspx/GetSubCategories',
                type: "POST",
                data: '{ "index":"' + parseInt(subIndex - 1) + '","take":"' + parseInt(take) + '","categoryid":"' + parseInt(id) + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d.length > 0) {
                        if (subIndex - 1 == 0) {
                            $("#CheckView" + id).empty();
                        }
                        $("#SubCategoryScript").tmpl(result.d).appendTo("#CheckView" + id);
                        if (result.d.length == take) {
                            $("#btnViewMore" + id).css("display", "block");
                        }
                        else {
                            $("#btnViewMore" + id).css("display", "none");
                        }
                    }
                    else {
                        $("#btnViewMore" + id).css("display", "none");
                    };
                }
            });
            return false;
        }
    </script>
</asp:Content>
