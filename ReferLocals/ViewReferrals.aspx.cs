using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ViewReferrals : System.Web.UI.Page
    {
        public static string UniqueId = "";
        public static Int64 UserId = 0;
        public void BindDropDownList()
        {
            CountryCode code = new CountryCode();
            List<CountryCode> lst = code.GetAll();
            if (lst != null && lst.Count > 0)
            {
                drpCountryCode.DataSource = lst;
                drpCountryCode.DataTextField = "TeleCode";
                drpCountryCode.DataValueField = "TeleCode";
                drpCountryCode.DataBind();

                drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
                if (SessionService.Pull(SessionKeys.UserId) > 0)
                {
                    aShareMyReferrals.HRef = "#divShareThisProfileModal";
                    aShareMyReferrals.Attributes.Remove("onclick");
                }
                else
                {
                    aShareMyReferrals.HRef = "javascript:void(0);";
                    aShareMyReferrals.Attributes.Add("onclick", "ShowLoginAlert('Please login to share referrals');");

                }
                if (Page.RouteData.Values.Count > 0)
                {
                    BindData(Convert.ToString(Page.RouteData.Values["ID"]));

                }
            }
        }
        public void BindData(string id)
        {
            User obj = new User();
            obj.UniqueId = id;
            List<User> lst = obj.GetAll();
            if (lst != null && lst.Count > 0)
            {
                if (lst[0].IsApprovedByAdmin == false && lst[0].IsVerified == false)
                {
                    Response.Redirect("/index");
                }
                else
                {
                    UserId = lst[0].Id;
                    lblFirstName.Text = lst[0].FirstName;
                    lblLastName.Text = lst[0].LastName;
                }
            }
            
        }

        [WebMethod]
        public static ReferalListWrapper GetReferList(int index, int take, int IsReferd)
        {
            Referal obj = new Referal();
            var referals = obj.GetAllReferalsBySenderID(UserId, index, take, (HelperEnums.BooleanValues)IsReferd); //obj.GetAllForAjax();
            return referals;
        }
    }
}