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
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_InIt(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (Session[SessionKeys.UserId] == null && (url.ToLower().Contains("jobinbox") || url.ToLower().Contains("messages") || url.ToLower().Contains("inbox") || url.ToLower().Contains("changepassword")||url.ToLower().Contains("profileimage") ||url.ToLower().Contains("jobquotes") || url.ToLower().Contains("favorites") || url.ToLower().Contains("followers")))
            {
                Response.Redirect("/login?ReturnUrl=" + url);
            }
            lblLocation.Text = Session["LocationName"] == null ? DataAccess.HelperClasses.Common.DefaultLocationName : Convert.ToString(Session["LocationName"]);
            //lblLocation.Text = Session["LocationId"] == null ? DataAccess.HelperClasses.Common.DefaultLocationName : Convert.ToString(Session["LocationName"]);
            //else if (!SessionService.HasKey("UserId"))
            //{
            //    Response.Redirect("/Login?ReturnUrl=" + url);
            //}
            //else if ((Int64)SessionService.Pull("RoleId") == (int)HelperEnums.Role.Admin)
            //{
            //    Response.Redirect("/Login?ReturnUrl=" + url);
            //}
            if (SessionService.Pull(SessionKeys.UserId) > 0)
            {
                if (SessionService.Pull(SessionKeys.UserType) == (int)HelperEnums.UserType.User)
                {
                    aForProfessional.HRef = "/user_dashboard";
                }
                else
                {
                    aForProfessional.HRef = "/Professional_dashboard";
                }

            }
            else
            {
                aForProfessional.HRef = "/ForProfessionals";
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindDropDownList();

            }
        }

        public void BindDropDownList()
        {
            if (SessionService.HasKey("UserId") && !SessionService.HasKey("CheckSession"))
            {
                States state = new States();
                state.Id = Convert.ToInt64(Common.LocationId);
               HttpContext.Current.Session["LocationId"] = Common.LocationId;
                if (state.GetRecord())
                {
                    HttpContext.Current.Session["LocationName"] = state.City + " " + state.State;// + " " + state.Zip;
                }
            }
            else if (SessionService.HasKey("LocationId"))
            {
                States state = new States();
                state.Id = (Int64)SessionService.Pull("LocationId");
                if (state.Id > 0)
                {
                    if (state.GetRecord())
                    {
                        HttpContext.Current.Session["LocationName"] = state.City + " " + state.State;// + " " + state.Zip;
                    }
                }

            }
            //else
            //{
            //    Session["LocationId"] = Common.DefaultLocationId;
            //    Session["LocationName"] = Common.DefaultLocationName;
            //}
        }


    }
}