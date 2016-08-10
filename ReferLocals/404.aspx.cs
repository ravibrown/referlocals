using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class _404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void Subscribe()
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {

                EmailMessage messageToAdmin = new EmailMessage();
                messageToAdmin.To = Common.FromEmail;
                messageToAdmin.Subject = "New Enquiry";

                messageToAdmin.Message = "" + txtEmail.Text + " showed interest in us.<br/><br> Thanks<br/>Team Referlocals";
                Common.SendEmail(messageToAdmin);
            }
            txtEmail.Text = "";
            //Response.Redirect("http://" + Common.WebsiteHostName + "/login?email=" + txtEmail.Text + "#register");

        }

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            Subscribe();
        }
    }

}