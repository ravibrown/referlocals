using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ReferProfessional : System.Web.UI.Page
    {
        public static string CheckPage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string Type = Page.RouteData.Values["type"] as string;
                if (Type.ToLower() == "search")
                {
                    CheckPage = "search";
                }
                else if(Type.ToLower() == "searchjob")
                {
                    CheckPage = "searchjob";
                }
                else
                {
                    CheckPage = "";
                }
            }
        }
    }
}