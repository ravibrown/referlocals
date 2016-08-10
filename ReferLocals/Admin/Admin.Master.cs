using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (!Common.GetSession())
            {
                Response.Redirect("/Admin/Login?ReturnUrl=" + url);
            }
            else if (Common.RoleId != (int)HelperEnums.Role.Admin)
            {
                Response.Redirect("/Admin/Login?ReturnUrl=" + url);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}