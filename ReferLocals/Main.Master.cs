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
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var test = DataAccess.HelperClasses.Common.UserImage;
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

            CountryCode code = new CountryCode();
            List<CountryCode> lst = code.GetAll();
            if (lst != null && lst.Count > 0)
            {
                drpCountryCode.DataSource = lst;
                drpCountryCode.DataTextField = "TeleCode";
                drpCountryCode.DataValueField = "TeleCode";
                drpCountryCode.DataBind();
            }
            }
    }
}