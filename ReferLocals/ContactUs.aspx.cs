using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Common.SendEmailWithGenericTemplate(Common.FromEmail, Common.EmailSubjectContactUs, "Name: " + txtName.Value + "<br/>" + "Email: " + txtEmail.Value + "<br/>Phone: " + txtPhone.Value + "<br/>Message: <br/>" + txtMessage.Value.Replace("\r\n", "<br/>"));
            txtEmail.Value = "";
            txtName.Value = "";
            txtPhone.Value = "";
            txtMessage.Value = "";
            lbMsg.Text = "Thank you for contacting us";
        }
    }
}