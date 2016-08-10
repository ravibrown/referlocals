using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class SelectUser : System.Web.UI.Page
    {
        public static string Id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["Id"]!=null)
            {
                Id = Request.QueryString["Id"].ToString();
            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}