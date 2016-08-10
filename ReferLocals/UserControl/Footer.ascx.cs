using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.UserControl
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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
    }
}