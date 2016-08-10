using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.UserControl
{
    public partial class TryOurBeta : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTryOurBeta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                EmailMessage message = new EmailMessage();
                message.To = txtEmail.Text;
                message.Subject = "Thank you for your interest";

                string professional = string.Empty;
                if (chkIsProfession.Checked == true)
                {
                    message.Message = Common.CreateComingSoonProfessionalEmailBody();
                    professional = "This user is a professional";
                }
                else
                {
                    message.Message = Common.CreateComingSoonEmailBody();
                }
                Common.SendEmail(message);

                EmailMessage messageToAdmin = new EmailMessage();
                messageToAdmin.To =Common.FromEmail ;
                messageToAdmin.Subject = "New Enquiry";
                
                messageToAdmin.Message = "" + txtEmail.Text + " showed interest in us.<br/>" + professional + "<br/><br> Thanks<br/>Team Referlocals";
                Common.SendEmail(messageToAdmin);
            }
               Response.Redirect("http://"+Common.WebsiteHostName+"/login?email="+txtEmail.Text+"#register");

        }
    }
}