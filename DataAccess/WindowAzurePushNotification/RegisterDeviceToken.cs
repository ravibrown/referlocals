using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System;
using DataAccess.HelperClasses;
using System.Configuration;
using System.Net.Mail;

namespace DataAccess.WindowAzurePushNotification
{
    public class RegisterDeviceToken
    {
        private NotificationHubClient hub;

        public RegisterDeviceToken()
        {
            hub = Notifications.Instance.Hub;
        }

        public class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }
        }

        public async Task<string> RegisterWindowAzureIdForDeviceToken(string token = null)
        {
            string newRegistrationId = null;

            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            if (!string.IsNullOrEmpty(token))
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(token, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            return newRegistrationId;
        }

        public async Task<HttpStatusCode> UpdateWindowAzureTagsAndTokenById(string id, DeviceRegistration deviceUpdate)
        {
            HttpStatusCode status = HttpStatusCode.Conflict;
            
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "gcm":
                    registration = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            registration.RegistrationId = id;
            //var username = HttpContext.Current.User.Identity.Name;
            
            //add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
            //registration.Tags.Add("username:" + username);


            try
            {
              registration = await hub.CreateOrUpdateRegistrationAsync(registration);

                if (registration != null && registration.RegistrationId == id)
                {

                    status = HttpStatusCode.OK;
                   // await abc("ID Created");
                }
                else
                {
                    await abc("registration null");
                }

            }
            catch(MessagingException e)
            {
                await abc("creating ID exception");
                ReturnGoneIfHubResponseIsGone(e);
            }

            return status;
        }
        public async Task abc(string msg)
        {
            string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
            string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();

            var client = new SmtpClient("smtp.gmail.com", 587)
            {


                Credentials = new NetworkCredential(FromEmail, EmailPassword),
                EnableSsl = true
            };
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add("ravi@impact-works.com");
            mailMessage.From = new MailAddress("support@referlocals.com", "Referlocals");
            mailMessage.Subject = "test";
            mailMessage.Body = msg;

            client.Send(mailMessage);
        }
        public async Task<HttpStatusCode> Delete(string id)
        {
            await hub.DeleteRegistrationAsync(id);
            return HttpStatusCode.OK;
        }

        private static void ReturnGoneIfHubResponseIsGone(Exception e)
        {
            var webex = e.InnerException as WebException;
            if (webex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)webex.Response;
                if (response.StatusCode == HttpStatusCode.Gone)
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());
            }
        }

    }

    public class RegisterDeviceTokenForPro
    {
        private NotificationHubClient hub;

        public RegisterDeviceTokenForPro()
        {
            hub = NotificationsForPro.Instance.Hub;
        }

        public class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }
        }

        public async Task<string> RegisterWindowAzureIdForDeviceToken(string token = null)
        {
            string newRegistrationId = null;

            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            if (!string.IsNullOrEmpty(token))
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(token, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            return newRegistrationId;
        }

        public async Task<HttpStatusCode> UpdateWindowAzureTagsAndTokenById(string id, DeviceRegistration deviceUpdate)
        {
            HttpStatusCode status = HttpStatusCode.Conflict;

            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "gcm":
                    registration = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            registration.RegistrationId = id;
            //var username = HttpContext.Current.User.Identity.Name;

            //add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
            //registration.Tags.Add("username:" + username);


            try
            {
                registration = await hub.CreateOrUpdateRegistrationAsync(registration);

                if (registration != null && registration.RegistrationId == id)
                {
                    status = HttpStatusCode.OK;
                }

            }
            catch (MessagingException e)
            {
                ReturnGoneIfHubResponseIsGone(e);
            }

            return status;
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            await hub.DeleteRegistrationAsync(id);
            return HttpStatusCode.OK;
        }

        private static void ReturnGoneIfHubResponseIsGone(Exception e)
        {
            var webex = e.InnerException as WebException;
            if (webex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)webex.Response;
                if (response.StatusCode == HttpStatusCode.Gone)
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());
            }
        }

    }
}
