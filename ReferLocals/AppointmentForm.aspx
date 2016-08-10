<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AppointmentForm.aspx.cs" Inherits="ReferLocals.AppointmentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="userAppointementForm1">
        <div class="container">
            <div class="col-md-9">
                <div id="divFormControls" runat="server" class="user_date">
                    <%if (Convert.ToInt32(Session[DataAccess.HelperClasses.SessionKeys.UserType]) == 1)
                        {%>
                    <h1 class="userdateh1">Congratulations <%=Session[DataAccess.HelperClasses.SessionKeys.UserName] %> </h1>
                    <h2 class="userdate_h2">You have just accepted to work with one off the top pro </h2>
                    <%} %>
                    <h3 class="userdate_h3">Please provide three available times slots that would work best for your appointement with the Professional </h3>

                    <div class="form-group">
                        <label class="control-label col-md-3">Appointment 1</label>
                        <div class="col-md-6">
                            <div class="input-group date form_datetime" id="datetimepicker1">
                                <input id="txtAppointment1" runat="server" clientidmode="Static" type="text" size="16" class="form-control">
                                <span class="input-group-btn">
                                    <%--<button class="btn default date-reset" type="button"><i class="fa fa-times"></i></button>--%>
                                    <button class="btn default date-set" type="button"><i class="fa fa-calendar"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="orform">or </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Appointment 2</label>
                        <div class="col-md-6">
                            <div class="input-group date form_meridian_datetime" id="datetimepicker2">
                                <input id="txtAppointment2" runat="server" clientidmode="Static" type="text" size="16" class="form-control">
                                <span class="input-group-btn">
                                    <%--<button class="btn default date-reset" type="button"><i class="fa fa-times"></i></button>--%>
                                    <button class="btn default date-set" type="button"><i class="fa fa-calendar"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="orform">or </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Appointment 3</label>
                        <div class="col-md-6">
                            <div class="input-group date form_meridian_datetime" id="datetimepicker3">
                                <input id="txtAppointment3" runat="server" clientidmode="Static" type="text" size="16" class="form-control">
                                <span class="input-group-btn">
                                    <%--<button class="btn default date-reset" type="button"><i class="fa fa-times"></i></button>--%>
                                    <button class="btn default date-set" type="button"><i class="fa fa-calendar"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>


                    <div class="form-group fguserappoitment">
                        <label class="col-md-3 control-label">Messages</label>
                        <div class="col-md-8">
                            <textarea id="txtMessage" runat="server" clientidmode="Static" rows="3" class="form-control fc1"></textarea>
                        </div>
                    </div>


                    <div class="btn_appointment">
                        <asp:Button ID="btnRequest" runat="server" CssClass="btn green" Text="Request Appointment" OnClick="btnRequest_Click" />
                    </div>


                </div>
                <div class="col-md-10 col-md-offset-1" id="divThanks" runat="server" visible="false">
                    <div class="Thanku_div">
                        <h2 class="thanksText">Thank You for sending appointment request on referlocals.<br />
                            You will receive updates soon. </h2>
                    </div>
                </div>
            </div>


            <div class="col-md-3">
                <div class="advertise_bg">
                    <div class="Add_advertise"><a href="#">
                        <img src="/images/advetise_img3.jpg" alt="advertise image">
                    </a></div>
                    <div class="Add_advertise"><a href="#">
                        <img src="/images/advetise_img2.jpg" alt="advertise image">
                    </a></div>
                    <div class="Add_advertise"><a href="#">
                        <img src="/images/advetise_img1.jpg" alt="advertise image">
                    </a></div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
 
    <script src="/js/moment.min.js" type="text/javascript" ></script>
    <script src="/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker(
                
                {
              //  format: "dd MM yyyy - HH:ii P",
                showMeridian: true,
                autoclose: true,
                todayBtn: true
                }
            );
            $('#datetimepicker2').datetimepicker(

                {
                    showMeridian: true,
                    autoclose: true,
                    todayBtn: true
                }
            );
            $('#datetimepicker3').datetimepicker(

                {
                   // format: "dd MM yyyy - HH:ii P",
                    showMeridian: true,
                    autoclose: true,
                    todayBtn: true
                }
            );
        });
    </script>
</asp:Content>
