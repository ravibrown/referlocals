using DataAccess.Classes;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class UserJobs : System.Web.UI.Page
    {
        public static long userID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values.Count > 0)
                {
                    BindDropDownList();
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
                    userID = lst[0].Id;
                    lblUsername.Text = lst[0].FirstName + " " + lst[0].LastName;

                }
            }

        }
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
        [WebMethod]
        public static UserJobDataContractList GetUserJobs(int pageIndex, int pageSize)
        {
            Jobs job = new Jobs();
            return job.GetUserJobs((int)HelperEnums.JobStatus.Open, userID, pageIndex, pageSize,SessionService.Pull(SessionKeys.UserId));
        }
    }
}