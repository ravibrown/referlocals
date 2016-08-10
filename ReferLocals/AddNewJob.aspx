<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddNewJob.aspx.cs" Inherits="ReferLocals.AddNewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="/css/profile.min.css" rel="stylesheet" type="text/css" />
    <style>
        .chksubcategory input {
        
        float:left;
        }
       .chksubcategory label {
            font-size: 18px;
    float: left;
    width: auto;
    margin-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Search Result Mid HTML ====================================-->

    <div class="jobmaintext">
        <div class="container">
            <h1>Add New job </h1>
        </div>
    </div>

    <div class="Addnew_job">
        <div class="container">
            <div class="col-md-9">
                <div id="divJobForm" runat="server" class="Addnewjob_form">

                    <div class="col-md-8 form-group jobform">
                        <label>Job title</label>
                        <asp:TextBox ID="txtTitle" MaxLength="100" runat="server" CssClass="form-control" placeholder="Job title" />
                    </div>

                    <div class="col-md-8 form-group jobform">
                        <label>Profesional Needed</label>
                        <div class="check_Box" style="width:100%;">
                            <asp:CheckBoxList ID="chkSubCategories" Width="100%" RepeatColumns="2" CssClass="chksubcategory" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <div class="col-md-8 form-group jobform" style="position: relative;">
                        <label>Location</label>
                        <%-- <asp:DropDownList ID="drpJobLocation" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0" />
                        </asp:DropDownList>--%>
                        <input type="text" id="txtWorkSearchCity"  placeholder="Enter City or Zipcode.." runat="server" clientidmode="Static" value="" class="form-control" />
                        <div id="divWorkLoader" style="display: none; top: 58px;">
                            <img src="/images/377.gif" />
                        </div>
                        <div id="div_WorkSearchContent">
                        </div>
                    </div>

                    <div class="col-md-8 form-group jobform">
                        <label>Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" CssClass="form-control" placeholder="Address" />
                    </div>

                    <div class="col-md-8 form-group jobform">
                        <label>Description</label>
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" MaxLength="1000" CssClass="form-control textbg" runat="server" Rows="5" />
                    </div>

                    <div class="col-md-8 form-group jobform">
                        <label>Image</label>
                        <div data-provides="fileinput" class="fileinput fileinput-new" style="width: 100%;">
                            <div id="divImgPreivew" style="width: 100%; height: 150px;" data-trigger="fileinput" class="fileinput-preview thumbnail">
                                <img id="imgPreview" runat="server" clientidmode="Static" />
                            </div>
                            <div>
                                <span class="btn red btn-outline btn-file"><span class="fileinput-new">Browse </span><span class="fileinput-exists">Change </span>
                                    <asp:FileUpload ID="FileImage" runat="server" />
                                </span><a data-dismiss="fileinput" class="btn red fileinput-exists" href="javascript:;">Remove </a>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-8 form-group jobform">
                        <p class="sharejob">We will not share your contact info unless you accept a quote </p>
                    </div>

                    <div style="margin-bottom: 25px;" class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="btnSave" Text="Publish" CssClass="btn" OnClick="btnSave_OnClick" OnClientClick="return CheckValidation();" runat="server" />
                                <a class="btn default" href="/AddNewJob">Cancel</a>
                            </div>
                        </div>
                    </div>

                    <div id="divMsgToProfessional" runat="server" style="margin-bottom: 25px;" class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                To post a job, <a href="/login">login</a> as a user
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-10 col-md-offset-1" id="divJobThanks" runat="server" visible="false">
                    <div class="Thanku_div">
                        <h2 class="thanksText">Thank You for posting job on referlocals.<br />
                            You will receive quotes from professionals soon. </h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="advertise_bg">
                     <div class="Add_advertise app_bg_img">

                        <p class="textapp_text1">Post a Job faster </p>
                        <p class="textapp_text2">Install the <span>App </span>today </p>
                        <div class="col-md-10 col-md-offset-1 btnappimg"><a href="<%=DataAccess.HelperClasses.Common.IOSAppLink %>" target="_blank">
                            <img src="/images/image_app2.jpg" alt="">
                        </a></div>
                        <%--<div class="col-md-10 col-md-offset-1 btnappimg"><a href="#">
                            <img src="/images/image_app3.jpg" alt="">
                        </a></div>--%>
                    </div>
                   <div class="Add_advertise">

                        <div class="videolink">
                            <iframe width="100%" height="185" src="https://www.youtube.com/embed/JSKm7homfxw" frameborder="0" allowfullscreen></iframe>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--==================================== End Search Result Mid HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="/js/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="/js/profile.min.js" type="text/javascript"></script>
    <script>

        function CheckValidation() {
            if (!IsLoggedIn) {
                window.location.href = "/Login?ReturnUrl=" + (location.pathname + location.search).substr(1);
                return false;
            }
            else {
                var ErrorMessage = "";
                var Title = $("#<%=txtTitle.ClientID%>").val();
                var Description = $("#<%=txtDescription.ClientID%>").val();
                var Location = $("#hdnLocationID").val(); <%--$('#<%=drpJobLocation.ClientID%> option:selected').val();--%>
                var Address = $("#<%=txtAddress.ClientID%>").val();

                if (Title == "") {
                    
                        ErrorMessage += "Please enter title"+"\r\n";
                    
                    $('#<%=txtTitle.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtTitle.ClientID%>').css("border-color", "#c2cad8");
                }

                if (!validateCheckBoxList()) {
                   
                        ErrorMessage += "Please select at least 1 category"+"\r\n";
                    
                }



                if (Description == "") {
                   
                        ErrorMessage += "Please enter description"+"\r\n";
                    
                    $('#<%=txtDescription.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtDescription.ClientID%>').css("border-color", "#c2cad8");
                }


                if (Location == "0") {
                    
                        ErrorMessage += "Please select location"+"\r\n";
                    
                    $('#<%=txtWorkSearchCity.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtWorkSearchCity.ClientID%>').css("border-color", "#c2cad8");
                }

                if (Address == "") {
                    
                        ErrorMessage += "Please enter address"+"\r\n";
                    
                    $('#<%=txtAddress.ClientID%>').css("border-color", "red");
                }
                else {
                    $('#<%=txtAddress.ClientID%>').css("border-color", "#c2cad8");
                }



                if (ErrorMessage != "") {
                   bootbox.alert(ErrorMessage);
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        function validateCheckBoxList() {
            var isAnyCheckBoxChecked = false;

            // ::: Step-1 & 2 ::: Let's get all the CheckBoxes inside the CheckBoxList.
            var checkBoxes = document.getElementById("<%= chkSubCategories.ClientID %>").getElementsByTagName("input");

            // ::: Step-3 ::: Now let's Loop through the Children.
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox") {
                    if (checkBoxes[i].checked) {
                        // ::: Step-4 ::: If current CheckBox is checked, then show alert.
                        // Break the Loop.
                        isAnyCheckBoxChecked = true;
                        break;
                    }
                }
            }
            return isAnyCheckBoxChecked;
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
    <asp:HiddenField ID="hdnLocationID" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hdnJobStatus" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hdnImageUrl" runat="server" ClientIDMode="Static" Value="0" />
</asp:Content>
