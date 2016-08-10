using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class AppointmentForm : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["quoteId"]))
            {
                btnRequest.Visible = true;
            }
            else
            {
                btnRequest.Visible = false;

            }

        }
        public void SaveAppointmentDates(long appointmentID)
        {
            Appointment appointment = new Appointment();
            Quote quote = new Quote();
            // var quoteData = quote.GetQuote(Convert.ToInt64(Request.QueryString["quoteId"]));
            if (!string.IsNullOrEmpty(txtAppointment1.Value) && appointmentID > 0)
            {
                appointment.SaveAppointmentDate(appointmentID, Convert.ToDateTime(txtAppointment1.Value));
            }
            if (!string.IsNullOrEmpty(txtAppointment2.Value) && appointmentID > 0)
            {
                appointment.SaveAppointmentDate(appointmentID, Convert.ToDateTime(txtAppointment2.Value));
            }
            if (!string.IsNullOrEmpty(txtAppointment3.Value) && appointmentID > 0)
            {
                appointment.SaveAppointmentDate(appointmentID, Convert.ToDateTime(txtAppointment3.Value));
            }
            bool isReschedule = false;
            if (!string.IsNullOrEmpty(Request.QueryString["RescheduledByProfessional"]) ||
                !string.IsNullOrEmpty(Request.QueryString["RescheduledByUser"]))
            {
                isReschedule = true;
            }
            quote.AcceptRejectQuote(Convert.ToInt64(Request.QueryString["quoteId"]), HelperEnums.QuoteStatus.Accepted, isReschedule);


            txtAppointment1.Value = "";
            txtAppointment2.Value = "";
            txtAppointment3.Value = "";
            txtMessage.Value = "";
            divFormControls.Visible = false;
            divThanks.Visible = true;

            if (SessionService.Pull(SessionKeys.UserType) == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "AppointmentRequestSentUser", "setTimeout(function(){ window.location.href='/User_dashboard' }, 5000);", true);
            }
            if (SessionService.Pull(SessionKeys.UserType) == 2)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "AppointmentRequestSentPro", "setTimeout(function(){ window.location.href='/Professional_dashboard' }, 5000);", true);
            }
        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointment1.Value) && string.IsNullOrEmpty(txtAppointment2.Value) && string.IsNullOrEmpty(txtAppointment3.Value))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "appointment", "bootbox.alert('Please select atleast one appointment date')", true);
            }
            else
            {
                Appointment appointment = new Appointment();

                Quote quote = new Quote();
                var quoteData = quote.GetQuote(Convert.ToInt64(Request.QueryString["quoteId"]));
                if (!string.IsNullOrEmpty(Request.QueryString["RescheduledByUser"]) && quoteData.UserID != SessionService.Pull(SessionKeys.UserId))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "appointment", "bootbox.alert('This appointment is not related to you')", true);
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["RescheduledByProfessional"]) && quoteData.ProfessionalID != SessionService.Pull(SessionKeys.UserId))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "appointment", "bootbox.alert('This appointment is not related to you')", true);
                }
                else
                {
                    long appointmentID = 0;
                    if (!string.IsNullOrEmpty(Request.QueryString["AppointmentID"]))
                    {
                        appointmentID = Convert.ToInt64(Request.QueryString["AppointmentID"]);
                        appointment.DeleteAppointmentDates(appointmentID);
                        if (!string.IsNullOrEmpty(Request.QueryString["RescheduledByUser"]))
                        {
                            appointmentID = appointment.Save(appointmentID, Convert.ToInt64(Request.QueryString["quoteId"]),
                            txtMessage.Value, false, quoteData.ProfessionalID,
                               true, true, false, SessionService.Pull(SessionKeys.UserId));

                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["RescheduledByProfessional"]))
                        {
                            appointmentID = appointment.Save(appointmentID, Convert.ToInt64(Request.QueryString["quoteId"]),
                            txtMessage.Value, false, SessionService.Pull(SessionKeys.UserId),
                               true, false, true, quoteData.UserID);
                        }
                    }
                    else
                    {
                        appointmentID = appointment.Save(0, Convert.ToInt64(Request.QueryString["quoteId"]), txtMessage.Value, false, quoteData.ProfessionalID,
                               false, false, false, SessionService.Pull(SessionKeys.UserId));

                    }

                    SaveAppointmentDates(appointmentID);
                    if (!string.IsNullOrEmpty(Request.QueryString["quoteId"]) && Request.QueryString.Keys.Count == 1)
                    {
                        Common.SendEmailWithGenericTemplate(SessionService.Pull<string>(SessionKeys.UserEmail),
                            Common.EmailSubjectOnQuoteAccept, Common.EmailBodyOnQuoteAccept.Replace("##Username##",
                            SessionService.Pull<string>(SessionKeys.UserName)));
                        User user = new User();
                        var professionalData = user.GetProfessionalByID(quoteData.ProfessionalID.GetValueOrDefault());
                        var userData = user.GetUserDetails(SessionService.Pull(SessionKeys.UserId));
                        if (professionalData != null&&userData!= null)
                        {
                            Common.SendEmailWithGenericTemplate(professionalData.Email, Common.EmailSubjectOnQuoteAcceptToProfessional,
                                Common.EmailBodyOnQuoteAcceptToProfessional
                                .Replace("##Username##", userData.FirstName + " " + userData.LastName)
                                .Replace("##FirstName##", userData.FirstName)
                                .Replace("##LastName##", userData.LastName)
                                .Replace("##ProfessionalName##", professionalData.FirstName + " " + professionalData.LastName)
                                .Replace("##PhoneNumber##", userData.CountryCode + userData.PhoneNumber)
                                .Replace("##Email##", userData.Email)
                                .Replace("##Address##", userData.StreetAddress + ", " + userData.CityName )
                                .Replace("##AppointmentLink#","<a href='"+Common.WebsiteHostNameForLink + "/" + "professional_dashboard"+"'>here</a>")
                                );
                        }
                        }
                }
            }
        }
    }
}