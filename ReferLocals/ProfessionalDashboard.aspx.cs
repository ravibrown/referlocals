using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ProfessionalDashboard : System.Web.UI.Page
    {
        public User userData;
        public static string jsonForCitiesIServe;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((HelperEnums.UserType)SessionService.Pull(SessionKeys.UserType) == HelperEnums.UserType.User)
                {
                    Response.Redirect("/user_dashboard");
                }
                else
                {
                    var userID = SessionService.Pull(SessionKeys.UserId);
                    BindUserData(userID);
                    BindAppointmentCount(userID);
                }
            }
        }
        private void BindAppointmentCount(long userID)
        {
            if (userID > 0)
            {
                Appointment appointment = new Appointment();
                spanAppointmentCount.InnerHtml=Convert.ToString( appointment.GetUpcomingAppointmentCountByUserID(userID,SessionService.Pull(SessionKeys.UserType)));
            }
        }
        private void BindUserData(long userID)
        {

            
            if (userID > 0)
            {
                User user = new User();
                userData = user.GetUserDetails(userID);
                if (userData != null)
                {
                    jsonForCitiesIServe = JsonConvert.SerializeObject(userData.CitiesIServe, Formatting.None,
                               new JsonSerializerSettings()
                               {
                                   ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                               });
                }
            }
        }
        [WebMethod]
        public static bool FlagReferral(long ID)
        {
            Referal referral = new Referal();
           return referral.FlagReferral(ID);
        }
        [WebMethod]
        public static string GetCitiesOfServe(string state)
        {
            try
            {
                List<DropDownsCityZip> lst_city = new List<DropDownsCityZip>();
                UserCityMapping obj = new UserCityMapping();
                obj.UserId = (Int64)SessionService.Pull("UserId");
                lst_city = obj.GetDropDownAllCitiesServe();
                var json = JsonConvert.SerializeObject(lst_city, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static void RemoveCityIServe(string cityID)
        {
            UserCityMapping obj = new UserCityMapping();
            var userID = SessionService.Pull(SessionKeys.UserId);
            obj.DeleteCityMappingByUserIdAndCityID(userID,Convert.ToInt64(cityID));
        }
        [WebMethod]
        public static void AddCityIServe(string cityID)
        {
            UserCityMapping obj = new UserCityMapping();
            var userID = SessionService.Pull(SessionKeys.UserId);
            obj.CityId= Convert.ToInt64(cityID);
            obj.UserId = userID;
            obj.Add(obj);
        }
        [WebMethod]
        public static void SetCitiesOfServe(string cities)
        {
            UserCityMapping obj = new UserCityMapping();
            var userID = SessionService.Pull(SessionKeys.UserId);
            obj.DeleteListByUserId(userID);

            if (!string.IsNullOrEmpty(cities))
            {
                string[] lst_cities = cities.Split(',');
                if (lst_cities != null)
                {
                    foreach (var item in lst_cities)
                    {
                        if (item != "")
                        {
                            UserCityMapping city = new UserCityMapping();
                            city.UserId = userID;
                            city.CityId = Convert.ToInt64(item);
                            city.IsApprovedByAdmin = true;
                            city.Add(city);
                        }
                    }
                }
            }

        }

        [WebMethod(EnableSession = true)]
        public static ReferalListWrapper GetWhoReferedMe(int pageIndex, int pageSize)
        {
            Referal referal = new Referal();
            return referal.GetWhoReferedMe(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }

        [WebMethod(EnableSession = true)]
        public static EstimateListWrapper GetEstimates(int pageIndex, int pageSize)
        {
            Quote quote = new Quote();
            return quote.GetEstimatesForProfessional(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }

        [WebMethod(EnableSession = true)]
        public static QuoteWithUserListWrapper GetAppointmentRequests(int pageIndex, int pageSize)
        {
            Quote quote = new Quote();
            return quote.GetAppointmentRequestsForProfessional(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
        [WebMethod(EnableSession = true)]
        public static AppointmentWithDatesDataContract GetRequestedDatesByAppointmentID(long appointmentID)
        {
            Appointment quote = new Appointment();
            return quote.GetAppointmentDatesByAppointmentID(appointmentID);
        }

    }
}