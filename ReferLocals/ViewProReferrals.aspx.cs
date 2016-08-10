using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ViewProReferrals : System.Web.UI.Page
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
        public static IList GetReferList(int index, int take, int IsReferd)
        {
            List<PropReferal> list_category = new List<PropReferal>();


            Referal obj = new Referal();

            obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            obj.UserId = UserId;
            if (IsReferd == 0)
            {
                obj.CheckIsRefered = (int)HelperEnums.BooleanValues.Disapproved;
            }
            else
            {
                obj.CheckIsRefered = (int)HelperEnums.BooleanValues.Approved;
            }

            list_category = obj.GetAllReferals(UserId, (HelperEnums.BooleanValues)IsReferd, take, index); //obj.GetAllForAjax();
            if (list_category != null && list_category.Count > 0)
            {
                for (int i = 0; i < list_category.Count; i++)
                {
                    States city = new States();
                    city.Id = Convert.ToInt64(list_category[i].UserCityId);
                    if (city.GetRecord())
                    {
                        User usr = new User();
                        usr.Id = Convert.ToInt64(list_category[i].UserId);
                        if (usr.GetRecord())
                        {
                            list_category[i].UserCityName = city.City;
                            list_category[i].UserZipName = city.State;
                            list_category[i].UserName = usr.FirstName + " " + usr.LastName;
                        }
                    }
                }
            }


            return list_category;
        }
    }
}