using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Classes;
using System.Web.Services;
using DataContracts;
using Newtonsoft.Json;
using DataAccess.DataContractClasses;

namespace ReferLocals
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        public User userData;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((HelperEnums.UserType)SessionService.Pull(SessionKeys.UserType) == HelperEnums.UserType.Professional)
                {
                    Response.Redirect("/professional_dashboard");
                }
                else
                {
                    var userID = SessionService.Pull(SessionKeys.UserId);
                    BindUserData(userID);
                    BindAppointmentCount(userID);
                }
            }
        }
        private void BindUserData(long userID)
        {

          
            if (userID > 0)
            {
                User user = new User();
                userData = user.GetUserDetails(userID);
                
            }
        }
        private void BindAppointmentCount(long userID)
        {
            if (userID > 0)
            {
                Appointment appointment = new Appointment();
                spanAppointmentCount.InnerHtml = Convert.ToString(appointment.GetUpcomingAppointmentCountByUserID(userID, SessionService.Pull(SessionKeys.UserType)));
            }
        }
        [WebMethod(EnableSession = true)]
        public static void ChangeJobStatus(int jobStatus, long jobID)
        {
            Jobs jobs = new Jobs();
            jobs.ChangeJobStatus(jobStatus, jobID,0);
        }

        [WebMethod(EnableSession = true)]
        public static JobListWrapper GetUserJobs(int jobStatus, int pageIndex, int pageSize)
        {
            Jobs jobs = new Jobs();

            return jobs.GetJobsByUserID(jobStatus, SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);

        }
        [WebMethod(EnableSession = true)]
        public static ReferalListWrapper GetReferalsByUser(int pageIndex, int pageSize)
        {
            Referal referal = new Referal();
            return referal.GetAllReferalsByUserID(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }

        [WebMethod(EnableSession = true)]
        public static QuoteListWrapper GetAppointmentRequests(int pageIndex, int pageSize)
        {
            Quote quote = new Quote();
            return quote.GetAppointmentRequestsForUser(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
        [WebMethod(EnableSession = true)]
        public static AppointmentWithDatesDataContract GetRequestedDatesByAppointmentID(long appointmentID)
        {
            Appointment quote = new Appointment();
            return quote.GetAppointmentDatesByAppointmentID(appointmentID);
        }


        [WebMethod(EnableSession = true)]
        public static long AcceptAppointmentDate(long appointmentDateID)
        {
            Appointment appointment = new Appointment();
            return appointment.AcceptAppointmentDate(appointmentDateID, (HelperEnums.UserType)(SessionService.Pull(SessionKeys.UserType)));

        }
        [WebMethod(EnableSession = true)]
        public static AppointmentDatesWithJobListWrapper GetAppointments(int dateType, int pageIndex, int pageSize, int month = 0)
        {
            Appointment appointment = new Appointment();
           return appointment.GetAppointments((HelperEnums.DateType)(dateType), (HelperEnums.UserType)(SessionService.Pull(SessionKeys.UserType)),
                SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize, month);
        }
    }
}