using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using SendGrid;
using Plivo.API;
using RestSharp;
using System.Reflection;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data.SqlClient;

namespace DataAccess.HelperClasses
{
    public class Common
    {

        #region "Properties"
        
        public static string HomeCardImagePath = "/images/HomeCardImages/";
        public static string HomeCardIconImagePath = "/images/HomeCardIconImages/";
        public static string HomeVideoPath = "/images/HomeVideo/";
        public static string CategoryImagesPath = "/images/CategoryImages/";
        public static string JobImagesPath = "/images/JobImages/";
        public static string JobDefaultImage = "/images/job.png";

        public static string SubCategoryImagesPath = "/images/SubCategoryImages/";
        public static string TestimonialImagesPath = "/images/TestimonialImages/";
        public static string UserImagesPath = "/images/UserImages/";
        public static string NoImageIcon = "/images/no-image-icon.png";
        public static string NoImageIconWithoutPath = "/no-image-icon.png";
        

        public static string AdminUserName = ConfigurationManager.AppSettings["AdminUserName"].ToString();
        public static string AdminPassword = ConfigurationManager.AppSettings["AdminPassword"].ToString();
        public static string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
        public static string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();
        public static string IOSAppLink = "https://itunes.apple.com/nz/developer/groupinc-consulting-llc/id1133937318";

        //public static string TextMsgToProOnRefer="Hello ##ProfessionalName##, ##Username## Referred you on Referlocals. View your Referral ##ProfileUrl## . Download the Referlocals App today "+Common.IOSAppLink + " NOTE: We need to have "Claim your Profile Button" where we will send them confirmation code on their mobile device if they decide to claim. then they can register.";
        public static string EmailSubjectContactUs = "Contact Us";

        public static string EmailSubjectOnJobInvitation = "Job View Invitation";
        public static string EmailBodyOnJobInvitation = "Hello <br/><br/> ##Username## has invited you to view job ##Joblink##. You can ##Login## and send an Estimate for this Job.";

        
        public static string EmailBodyOnMessageReceived = "Hello ##ReceiverUsername##:<br/><br/> You have a new Message from ##SenderUsername##. Click here to view ##MessageLink##";
        public static string EmailSubjectOnMessageReceived = "New Message On Referlocals";
        public static string EmailSubjectOnReferral = "Referred On Referlocals";
        public static string EmailBodyOnReferral = "Hello ##ProfessionalName##: ##Username## just Referred you on Referlocals. Check out now : ##ProfileLink##";

        public static string EmailBodyOnAppointmentRescheduleToUser= "Hello ##Username##:<br/><br/> ##ProfessionalName## requested to reschedule the appointment for ##JobLink## Click here to view ##AppointmentLink##";
        public static string EmailSubjectOnAppointmentRescheduleToUser= "Appointment Rescheduled";


        public static string EmailBodyOnAppointmentRescheduleToPro= "Hello ##ProfessionalName##: ##Username## requested to reschedule the appointment for ##JobLink## Click here to view ##AppointmentLink##";
        public static string EmailSubjectOnAppointmentRescheduleToPro = "Appointment Rescheduled";

        public static string EmailSubjectOnAppointmentConfirm = "Appointment confirmed";
        public static string EmailBodyOnAppointmentConfirm= "Hello ##Username##: <br/><br/>##ProfessionalName## just confirmed the appointment for the Job ##JobLink##. The Appointment is scheduled on ##AppointmentDateTime## for the appointment. You can view your appointment confirmation by going to ##AppointmentLink##";

        public static string EmailSubjectOnQuoteAcceptToProfessional = "Quote Accepted";
        public static string EmailBodyOnQuoteAcceptToProfessional ="Hello ##ProfessionalName##:<br/><Br/> ##Username## just accepted your Quote and sent you appointment request.You can respond to the appointment request(and if there are some questions to your potential client) by clicking on ##AppointmentLink## .<br/> Here is the Contact Details of ##Username##<br/><br/>FirstName: ##FirstName##<Br/>LastName: ##LastName##<Br/>Phone Number: ##PhoneNumber##<br/>Email: ##Email##<br/>Address: ##Address##<br/><br/> Few things to consider:<Br/> 1. Make sure you get all the information regarding the job in order to keep your Estimate same( in case more work is required than anticipated then let them know).<br/> 2. This Person will be providing a Referral(positive or Negative) based on the experience.";

        public static string EmailSubjectOnQuoteAccept = "Guidlines";
        public static string EmailBodyOnQuoteAccept = "Hello ##Username##:<br/><br/> Once you Accept the Estimate, you should make sure :<br/><br/> 1. The Professional understand full scope of the work.If there is increased scope then Final Quote might change.E.g you think the pipe is leaking but there is bigger underlying problem which need more work.<br/> 2. If you need to reschedule the appointment then give Professional sufficient amount of notice ( atleast 24 hours) <br/>3. Refer the Professional(Positive or Negative) once he finishes up the job so that other people can benefit from your experience.";

        public static string EmailBodyOnAppointmentRequest="Hello ##Username##:<Br/><br/> ##ProfessionalName## requested to reschedule the appointment for ##JobLink## Click here to view ##AppointmentLink## ";

        public static string EmailSubjectOnEstimateReceive = "Received An Estimate On Your Job";
        public static string EmailBodyOnEstimateReceive= "Hello ##Username##:<br/><br/> ##ProfessionalName## has sent you an Estimate for your <a href='##JobLink##'>job</a>. You can view the Estimate by going to ##EstimateLink## . Your Contact information will only be shared once you accept the Estimate. If you have any questions regarding the Estimate then you may send the message directly to the professional.";

        public static string EmailSubjectOnQuoteDecline= "Estimate Declined";
        public static string EmailBodyOnQuoteDecline = "Hello ##ProfessionalName##:<br/> <br/>##Username## has declined your Estimate. If you wish to Revise and Resubmit your Estimate you can clik here ##ReviseEstimateLink## ";

        public static string EmailSubjectOnPostJobToUser = "Job Posted Successfully";
        public static string EmailBodyOnPostJobToUser= "Hello ##Username##:<br/><br/> Your Job ##Title## has been posted under ##Category##. Your Job Url is ##JobLink##.";
        public static string SubjectJobInvitation = "##Username## invited you to view a job posted at Referlocals";
        public static string SubjectJobLink = "##Username## referred you a job posted at Referlocals";
        public static string BodyJobLink = "Hey, ##Username## shared this job with you posted at referlocals <br/>##JOBURL##";
        public static string BodyJobInvitation= "Hey, ##Username## invited you to view this job posted at referlocals <br/>##JOBURL##<br/><br/>Thanks <br/>Refer Locals Team";
        public static string SMSBodyJobInvitation = "Hey, ##Username## invited you to view this job posted at referlocals "+Environment.NewLine+ "##JOBURL##" + Environment.NewLine + "" + Environment.NewLine + "Thanks " + Environment.NewLine + "Refer Locals Team";
        public static string SubjectProfileLink = "##Username## referred you a professional at Referlocals";
        public static string BodyProfileLink = "Hey, ##Username## shared this professional profile with you posted at referlocals <br/>##JOBURL##";

        public static string SubjectProAppFeedback = "Refer Locals Pro App Feedback";
        public static string SubjectUserAppFeedback = "Refer Locals User App Feedback";
        public static string SubjectFlagReferral = "Refer Locals Referral flagged";
        public static string EmailBodyFlagReferral = "Hi<br/> A referral was flagged on Refer Locals.<br/><br/>Thanks<br/>Refer Locals Team";
        public static string ForgetPasswordSubject = "Refer Locals Forget Password";
        public static string SignupEmailSubject = "ReferLocals - Thank you from our CEO";
        public static string OTPSubject = "Refer Locals Verification";

        public static string SMSBodyForUserSignup = "Welcome to ReferLocals! Now connecting North Texas Residents with locals service providers." + Environment.NewLine
                                                                + "Appstore Link" + Environment.NewLine
                                                                + "ReferLocals for Consumers: http://bit.ly/rls4c" + Environment.NewLine + Environment.NewLine
                                                                + "ReferLocals for Professionals: http://bit.ly/rls4p" + Environment.NewLine
                                                                + "Suggested next steps:" + Environment.NewLine
                                                                + "-Complete your Profile" + Environment.NewLine + Environment.NewLine
                                                                + "-Recommend a Professional" + Environment.NewLine
                                                                + "-Invite friends and family to ReferLocals" + Environment.NewLine
                                                                + "-Post a Job and hire a top local professional for your next project" + Environment.NewLine + Environment.NewLine

                                                                + "Regards" + Environment.NewLine
                                                                + "ReferLocals Team";

        public static string SMSBodyForProfessionalSignup = "Welcome to ReferLocals! Now connecting North Texas Residents with locals service providers."+Environment.NewLine
                                                                +"Appstore Link"+Environment.NewLine
                                                                + "ReferLocals for Professionals: http://bit.ly/rls4p"+Environment.NewLine
                                                                + "ReferLocals for Consumers: http://bit.ly/rls4c" + Environment.NewLine+Environment.NewLine

                                                                +"Suggested next steps:"+Environment.NewLine
                                                                +"-Complete your Profile"+ Environment.NewLine+ Environment.NewLine
                                                                +"-Add your client securely on App.Request a Review, add their likes and preferences"+ Environment.NewLine
                                                                +"-Invite friends, family and colleagues to ReferLocals"+ Environment.NewLine
                                                                + "-Search for local jobs posted by people near you"+ Environment.NewLine+ Environment.NewLine

                                                                +"Regards"+ Environment.NewLine
                                                                +"ReferLocals Team";

        public static string ForgetPasswordBodyMessage = "Please put this OTP ##OTP## code on display screen and reset your password";
        public static string VerifyBodyMessage = "Please put this OTP ##OTP## code on display screen and verify your account";
        public static string PilvoAuthId = ConfigurationManager.AppSettings["PilvoAuthID"].ToString();
        public static string PilvoAuthToken = ConfigurationManager.AppSettings["PilvoAuthToken"].ToString();
        public static string PilvoFromNumber = ConfigurationManager.AppSettings["PilvoFromNumber"].ToString();
        public static string DefaultUserPassword = "referlocal123";
        public static string DefaultLocationId = "32634";
        //public static string DefaultLocationName = "Frisco TX 75034";
        public static string DefaultLocationName = "Frisco TX";
        public static Int64 SelectedCountryCode = 1;
        public static string WebsiteHostNameForLink = "https://www.referlocals.com";
        public static string WebsiteHostName = "www.referlocals.com";
        public static string WebsiteLiveHostName = "www.referlocals.com";
        //UserId
        private static Int64 _UserId = 0;
        public static Int64 UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //RoleId
        private static Int64 _RoleId = 0;
        public static Int64 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //LocationId
        private static Int64 _LocationId = 0;
        public static Int64 LocationId
        {
            get { return _LocationId; }
            set { _LocationId = value; }
        }
        private static string _Location= "";
        public static string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        //UserName
        private static string _UserName = "";
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //UserImage
        private static string _UserImage = "";
        public static string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }


        //UserEmail
        private static string _UserEmail = "";
        public static string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }

        //UserType
        private static int _UserType = 0;
        public static int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        //PhoneNumber
        private static string _PhoneNumber = "";
        public static string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        #endregion

        #region "Common fubnctions"
        public static string NewVerificationCode()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt32(buffer, 6).ToString();

        }
        public static string TextToFriendlyUrl(string s)
        {
            StringBuilder sb = new StringBuilder();
            bool wasHyphen = true;

            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLower(c));
                    wasHyphen = false;
                }
                else if (c != '\'' && !wasHyphen)
                {
                    sb.Append('-');
                    wasHyphen = true;
                }
            }

            // Avoid trailing hyphens
            if (wasHyphen && sb.Length > 0)
                sb.Length--;

            return sb.ToString();
        }
        public static string CreateUniqueId()
        {
            string code = string.Empty;
            Guid g;
            g = Guid.NewGuid();
            code = g.ToString().Substring(0, 6);
            return code;
        }

        public static string Encode(string value)
        {
            string encodedValue = string.Empty;
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            encodedValue = Convert.ToBase64String(encode);
            return encodedValue;
        }
        public static string Decode(string value)
        {
            string decodedValue = string.Empty;
            UTF8Encoding encode = new UTF8Encoding();
            Decoder Decode = encode.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(value);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decodedValue = new String(decoded_char);
            return decodedValue;
        }

        public static bool SetSession()
        {
            HttpContext.Current.Session["UserId"] = UserId;
            HttpContext.Current.Session["UserName"] = UserName;
            HttpContext.Current.Session["UserType"] = UserType;
            HttpContext.Current.Session["RoleId"] = RoleId;
            HttpContext.Current.Session["UserEmail"] = UserEmail;
            HttpContext.Current.Session["UserImage"] = UserImage;
            HttpContext.Current.Session["PhoneNumber"] = PhoneNumber;
            //HttpContext.Current.Session["LocationId"] = LocationId;
            //HttpContext.Current.Session["LocationName"] = Location;
            HttpContext.Current.Session["UserType"] = UserType;
            if (HttpContext.Current.Session["UserId"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Session["UserId"] = null;
            HttpContext.Current.Session["LocationId"] = null;
            HttpContext.Current.Session["LocationName"] = null;
            HttpContext.Current.Session["CheckSession"] = null;
            HttpContext.Current.Session["UserName"] = null;
            HttpContext.Current.Session["RoleId"] = null;
            HttpContext.Current.Session["UserEmail"] = null;
            HttpContext.Current.Session["UserImage"] = null;
            HttpContext.Current.Session["PhoneNumber"] = null;
            HttpContext.Current.Session["UserType"] = null;
            if (HttpContext.Current.Session["UserId"] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetSession()
        {
            bool flag = false;
            if (HttpContext.Current.Session["UserId"] != null)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["UserId"]) != 0)
                {
                    UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                    UserName = HttpContext.Current.Session["UserName"].ToString();
                    RoleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"]);
                    UserEmail = HttpContext.Current.Session["UserEmail"].ToString();
                    UserImage = HttpContext.Current.Session["UserImage"].ToString();
                    PhoneNumber = HttpContext.Current.Session["PhoneNumber"].ToString();
                    //UserType= HttpContext.Current.Session["UserType"].ToString();
                    flag = true;
                }
                else
                {
                    UserId = 0;
                    UserName = "";
                    RoleId = 0;
                    flag = false;
                }
            }
            else
            {
                UserId = 0;
                UserName = "";
                RoleId = 0;
                flag = false;
            }
            return flag;

        }

        public static void SetGlobalLocation(string locationID,string locationName)
        {
            HttpContext.Current.Session["LocationName"] = locationName;
            HttpContext.Current.Session["LocationId"] = locationID;
            Common.LocationId = Convert.ToInt64(locationID);
            Common.Location = locationName;
        }
        public static bool SendCodeThroughSms(string PhoneNumber = "", string Message = "")
        {
            string result = string.Empty;
            try
            {
                // Creating the Plivo Client
                RestAPI plivo = new RestAPI(PilvoAuthId, PilvoAuthToken);

                IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>()
            {
                { "src", PilvoFromNumber }, // Sender's phone number with country code
                { "dst", PhoneNumber }, // Receiver's phone number wiht country code
                { "text", Message }, // Your SMS text message
                // To send Unicode text
                // {"text", "こんにちは、元気ですか？"} // Your SMS text message - Japanese
                // {"text", "Ce est texte généré aléatoirement"} // Your SMS text message - French
                { "url", "https://api.plivo.com/v1/"}, // The URL to which with the status of the message is sent
                { "method", "POST"} // Method to invoke the url
            });

                // Prints the message details
                //Console.Write(resp.Content);
                
                // Print the message_uuid
                //Console.WriteLine(resp.Data.message_uuid[0]);

                // Print the api_id
                //Console.WriteLine(resp.Data.api_id);
                if (resp.ResponseStatus == ResponseStatus.Completed)
                {
                    return true;
                }
                return false;
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                return false;
            }
            return  false;
        }

        public static string CreateComingSoonEmailBody()
        {

            string body = string.Empty;
            
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/emailtemplates/comingsoon.html")))
            {

                body = reader.ReadToEnd();

            }

            //body = body.Replace("{UserName}", userName); //replacing the required things  

            //body = body.Replace("{Title}", title);

            //body = body.Replace("{message}", message);

            return body;

        }
        public static string CreateComingSoonProfessionalEmailBody()
        {

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/emailtemplates/comingsoonprofessionals.html")))
            {

                body = reader.ReadToEnd();

            }

            //body = body.Replace("{UserName}", userName); //replacing the required things  

            //body = body.Replace("{Title}", title);

            //body = body.Replace("{message}", message);

            return body;

        }
        public static string CreateUserSignupEmailBody(string firstname,string lastname)
        {

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/emailtemplates/usersignup.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("#firstname#", firstname); //replacing the required things  
            body = body.Replace("#lastname#", lastname); //replacing the required things  
            //body = body.Replace("{Title}", title);

            //body = body.Replace("{message}", message);

            return body;

        }
        public static string CreateProfessionalSignupEmailBody(string firstname,string lastname,string professionalprofilelink)
        {

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/emailtemplates/professionalsignup.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("#firstname#", firstname); //replacing the required things  
            body = body.Replace("#lastname#", lastname); //replacing the required things  
            body = body.Replace("#professionalprofilelink#", professionalprofilelink); //replacing the required things  

            return body;

        }
        //public static bool SendEmailWithSendinBlue()
        //{
        //    API sendinBlue = new mailinblue.API("your access key");
        //    Dictionary<string, Object> data = new Dictionary<string, Object>();
        //    Dictionary<string, string> to = new Dictionary<string, string>();
        //    to.Add("to@example.net", "to whom!");
        //    List<string> from_name = new List<string>();
        //    from_name.Add("from@email.com");
        //    from_name.Add("from email!");
        //    List<string> attachment = new List<string>();
        //    attachment.Add("https://domain.com/path-to-file/filename1.pdf");
        //    attachment.Add("https://domain.com/path-to-file/filename2.jpg");

        //    data.Add("to", to);
        //    data.Add("from", from_name);
        //    data.Add("subject", "My subject");
        //    data.Add("html", "This is the <h1>HTML</h1>");
        //    data.Add("attachment", attachment);

        //    Object sendEmail = sendinBlue.send_email(data);
        //    //Console.WriteLine(sendEmail);
        //}
        public static bool SendEmailWithGenericTemplate(string to,string subject, string message)
        {
            try
            {
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/emailtemplates/genericemailtemplate.html")))
                {

                    body = reader.ReadToEnd();

                }

                body = body.Replace("#Body#", message);
                //var client = new SmtpClient("smtp.gmail.com", 587)
                var client = new SmtpClient("smtp-relay.sendinblue.com", 587)
                {
                    Credentials = new NetworkCredential(FromEmail, EmailPassword),
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(FromEmail, "Referlocals");
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return SendEmailWithSendGrid(Message.To, Message.Subject, Message.Message);
            //try
            //{
            //    //configure the client
            //    SmtpClient client = new SmtpClient("smtp.gmail.com");
            //    client.Port = 587;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    //create credentials
            //    //NetworkCredential credentials = new NetworkCredential(FromEmail, EmailPassword);
            //    NetworkCredential credentials = new NetworkCredential("", "");
            //    client.EnableSsl = true;
            //   // client.Credentials = credentials;

            //    //create message
            //    var mail = new MailMessage("referlocals@gmail.com", Message.To);
            //    mail.IsBodyHtml = true;
            //    mail.Subject = Message.Subject;
            //    mail.Body = Message.Message;
            //    //ServicePointManager.ServerCertificateValidationCallback =
            //    //delegate(object s, X509Certificate certificate,
            //    //         X509Chain chain, SslPolicyErrors sslPolicyErrors)

            //    //    {
            //    //        return (true);
            //    //    };

            //    client.Send(mail);

            //    //client.Send(mail);
            //    return true;
            //}
            //catch (Exception ex) { return false; }
        }
        public static bool SendEmail(EmailMessage Message)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(FromEmail, EmailPassword),
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(Message.To);
                mailMessage.From = new MailAddress(FromEmail,"Referlocals");
                mailMessage.Subject = Message.Subject;
                mailMessage.Body = Message.Message;

                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return SendEmailWithSendGrid(Message.To, Message.Subject, Message.Message);
            //try
            //{
            //    //configure the client
            //    SmtpClient client = new SmtpClient("smtp.gmail.com");
            //    client.Port = 587;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    //create credentials
            //    //NetworkCredential credentials = new NetworkCredential(FromEmail, EmailPassword);
            //    NetworkCredential credentials = new NetworkCredential("", "");
            //    client.EnableSsl = true;
            //   // client.Credentials = credentials;

            //    //create message
            //    var mail = new MailMessage("referlocals@gmail.com", Message.To);
            //    mail.IsBodyHtml = true;
            //    mail.Subject = Message.Subject;
            //    mail.Body = Message.Message;
            //    //ServicePointManager.ServerCertificateValidationCallback =
            //    //delegate(object s, X509Certificate certificate,
            //    //         X509Chain chain, SslPolicyErrors sslPolicyErrors)
                
            //    //    {
            //    //        return (true);
            //    //    };
                
            //    client.Send(mail);
               
            //    //client.Send(mail);
            //    return true;
            //}
            //catch (Exception ex) { return false; }
        }

        public static bool SendEmailWithSendGrid(string to,string subject,string body)
        {
            try
            {
                MailAddress from=new MailAddress("support@referlocals.com");
                MailAddress toAddress=new MailAddress(to);
                MailAddress[] tolist=new MailAddress[1]{toAddress};
                SendGridMessage message = new SendGridMessage();
                message.From = from;
                message.To = tolist;
                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential("azure_00dd706f353fe985b11a73bf863a8a41@azure.com", "CMPQuPIKB45c4np");

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                // You can also use the **DeliverAsync** method, which returns an awaitable task.
                transportWeb.DeliverAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

       

    }

 

    #region "Extra Classes"
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class DropDowns
    {
        public long Id;
        public string Name;
    }

    public class DropDownsCityZip
    {
        public long Id;
        public string City;
        public string Zip;
        public string State;

        public long ID { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }

    }
    #endregion
}
