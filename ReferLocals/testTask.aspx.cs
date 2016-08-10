using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class testTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DateTime date1 = new DateTime(2010, 3, 14, 2, 30, 0, DateTimeKind.Local);
            
            DateTime utcDate1 = date1.ToUniversalTime();
            DateTime date2 = utcDate1.ToLocalTime();
            Console.WriteLine("{0} --> {1}", date1, date2);
            //Task.Run(() =>
            //{
            //    string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
            //    string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();

            //    var client = new SmtpClient("smtp.gmail.com", 587)
            //    {


            //        Credentials = new NetworkCredential(FromEmail, EmailPassword),
            //        EnableSsl = true
            //    };
            //    MailMessage mailMessage = new MailMessage();
            //    mailMessage.IsBodyHtml = true;
            //    mailMessage.To.Add("ravi@impact-works.com");
            //    mailMessage.From = new MailAddress("support@referlocals.com", "Referlocals");
            //    mailMessage.Subject = "test";
            //    mailMessage.Body = "test";

            //    client.Send(mailMessage);
            //});
        }
    }
}