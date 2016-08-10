<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddReferal.aspx.cs" Inherits="ReferLocals.AddReferal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divDataNotFound" runat="server" visible="false"> <div class="container">
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="row">
                                <h3 style="color: #0094ff">User not found</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div></div>
    <div id="divData" runat="server" class="Referred_pro3">
        <div class="container">
            <div class="col-md-10 col-md-offset-1" style="min-height: 150px;">
                <div id="divReferalForm" runat="server" class="referd_page_background">
                    <h1>Welcome <%=Convert.ToString(HttpContext.Current.Session["UserName"]) %> </h1>
                    <h2>Your referral will help other people know a bit more about
                        <asp:Label ID="lblName" Text="" runat="server" />
                    </h2>
                    <div class="formSidePosition" >
                        <div class="form-group" style="position:relative;">
                            <label>Which City/Zipcode this Professional provided service?</label>
                             <input type="text" id="txtWorkSearchCity" runat="server" clientidmode="Static" placeholder="Enter City or ZipCode.." value="" class="form-control input-lg" />
                                        <div id="divWorkLoader" style="display: none; top: 58px;">
                                            <img src="/images/377.gif" />
                                        </div>
                                        <div id="div_WorkSearchContent">
                                        </div>
                            <%--<asp:DropDownList ID="drpCity" class="form-control input-lg" runat="server">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>--%>
                        </div>

                        <div class="form-group">
                            <label>Were You Satisfied with your Professional's Service?</label>
                            <asp:DropDownList ID="drpSatisfied" class="form-control input-lg" runat="server">
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label>Would You Refer this Professional?</label>
                            <asp:DropDownList ID="drpRefered" class="form-control input-lg" runat="server">
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label for="comment">Comments</label>
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" CssClass="form-control textbg" runat="server" />
                        </div>
                    </div>
                    <div class="button_form_div">
                        <asp:Button ID="btnSave" OnClick="btnSave_OnClick" OnClientClick="return CheckValidation();" Text="Submit Referral" CssClass="btn default blue btn-lg" runat="server" />
                    </div>
                </div>
                <div class="col-md-10 col-md-offset-1" id="divReferalThanks" runat="server" visible="false">
                    <div class="Thanku_div">
                        <h2 class="thanksText">Thank you for referring <%=lblName.Text %>.
                            <br>
                            Your Valuable referral will help us a lot in evaluating this professional  </h2>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        function CheckValidation() {
            var ErrorMessage = "";
            var Comment = $("#<%=txtComment.ClientID%>").val();
            var City =$("#hdnLocationID").val();<%--$('#<%=drpCity.ClientID%> option:selected').val();--%>

            if (Comment == "") {
                
                    ErrorMessage += "Please enter comment"+"\r\n";
                
                $('#<%=txtComment.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtComment.ClientID%>').css("border-color", "#c2cad8");
            }


            if (City == "0") {
                
                    ErrorMessage += "Please select city"+"\r\n";
                
                $('#<%=txtWorkSearchCity.ClientID%>').css("border-color", "red");
            }
            else {
                $('#<%=txtWorkSearchCity.ClientID%>').css("border-color", "#c2cad8");
            }

            if (ErrorMessage != "") {
                bootbox.alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }
        }
        $(document).ready(function () {
            var timeout;
            $('#txtWorkSearchCity').keyup(function () {
                clearTimeout(timeout);
                var txtval = $(this).val();
                if (txtval.length > 1) {
                    $("#divWorkLoader").show();
                    timeout = setTimeout(function () { GetWorkLocation(txtval); }, 1000);
                }
                else {
                    $("#div_WorkSearchContent").empty();
                    $("#divWorkLoader").hide();
                }
            });

        });

        function SetWorkLocation(btn) {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtWorkSearchCity").val(text);
            $("#hdnLocationID").val(id);
            $("#div_WorkSearchContent").css("display", "none");
            $("#div_WorkSearchContent").empty();
        }
        function GetWorkLocation(searchContent) {
            if (searchContent != "") {
                $.ajax({
                    url: '/Index.aspx/GetLocations',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "Keyword":"' + searchContent + '"}',
                    success: function (result) {
                        if (result.d != null) {
                            if (result.d.length > 0) {
                                $("#divWorkLoader").hide();
                                $("#div_WorkSearchContent").empty();
                                $("#div_WorkSearchContent").hide();
                                $("#WorkCitySearch_template").tmpl(result.d).appendTo("#div_WorkSearchContent");
                                $("#div_WorkSearchContent").slideDown('slow');
                            }
                            else {
                                $("#divWorkLoader").hide();
                                $("#div_WorkSearchContent").empty();
                                $("#div_WorkSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                                $("#div_WorkSearchContent").slideDown('slow');
                            }
                        }
                        else {
                            $("#divWorkLoader").hide();
                            $("#div_WorkSearchContent").empty();
                            $("#div_WorkSearchContent").html('<label class="workautocomplete_section">No Search Result Found!</label>');
                            $("#div_WorkSearchContent").slideDown('slow');
                        }
                    }
                });
            }
            else {
                $("#divWorkLoader").hide();
                $("#div_WorkSearchContent").css("display", "none");
                $("#div_WorkSearchContent").empty();
            }
        }

    </script>
     <script id="WorkCitySearch_template" type="text/html">
        <label class="workautocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetWorkLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <asp:HiddenField ID="hdnLocationID"  runat="server" ClientIDMode="Static" Value="0" />
</asp:Content>
