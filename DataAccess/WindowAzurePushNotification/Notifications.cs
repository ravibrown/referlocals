using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.NotificationHubs;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using DataAccess.HelperClasses;

namespace DataAccess.WindowAzurePushNotification
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        public Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://referlocalspushnotifications.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=/bVP/YHjm+aBq8ONPENDI8qVn+kkIHa3V33Xh3KCK0E=",
                                                                         "referlocalsPushNotification");
            
        }


        public async Task<HttpStatusCode> SendWindowAzurePushNotification(string pns, string to_tag, string alert, HelperClasses.HelperEnums.PushNotificationType type, string jsonStr)
        {
            try
            {
                //pns = "131afe75d9083dfb60f2cfde49af5e8be28bbd719f77096c6fa8eab6eb83220a";
                //message = "testing testing";
                // string message = "testing testing";
                //var user = "ravi"; //HttpContext.Current.User.Identity.Name;
                //string[] userTag = new string[2];
                //userTag[0] = "username:" + to_tag;
                //userTag[1] = "from:" + user;
                string userTag = to_tag;

                Microsoft.Azure.NotificationHubs.NotificationOutcome outcome = null;
                HttpStatusCode ret = HttpStatusCode.InternalServerError;

                switch (pns.ToLower())
                {
                    case "wns":
                        // Windows 8.1 / Windows Phone 8.1
                        var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                     alert + "</text></binding></visual></toast>";
                        outcome = await Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
                        break;
                    case "ios":
                        // iOS
                        var alert11 = "{\"aps\":{\"content-available\":1,\"alert\":\"" + alert + "\",\"PushNotificationType:\":\"" + (int)type + "\",\"jsonStr\":" + jsonStr + "}}";
                        outcome = await Instance.Hub.SendAppleNativeNotificationAsync(alert11, userTag);
                        break;
                    case "gcm":
                        // Android
                        var notif = "{ \"data\":{\"alert\":\"" + alert + "\",\"PushNotificationType:\":\"" + (int)type + "\",\"jsonStr\":" + jsonStr + "}}";
                        outcome = await Instance.Hub.SendGcmNativeNotificationAsync(notif, userTag);
                        break;
                }

                if (outcome != null)
                {
                    if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                        (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                    {
                        Common.SendEmailWithGenericTemplate("ravi@impact-works.com", "error in referlocals", outcome.State.ToString());
                        ret = HttpStatusCode.OK;
                    }
                }

                return ret;

            }
            catch (Exception ex)
            {
                Common.SendEmailWithGenericTemplate("ravi@impact-works.com", "error in referlocals", ex.Message);
                return HttpStatusCode.BadRequest;
            }


        }

    }
    public class NotificationsForPro
    {
        public static NotificationsForPro Instance = new NotificationsForPro();

        public NotificationHubClient Hub { get; set; }

        public NotificationsForPro()
        {
            //Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://referlocalspushnotificationforpro.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=lt7ucZM3WGn8B6yNIsa9KjnxjQWAhelQEdzB2fdbBZM=",
            //                                                             "referlocalsPushNotificationForPro");
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://livepushnotificationforpro.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=o9f2T0x3B71EDICNexSXQviA284KI1gYg4Mo7my65wQ=",
                                                                         "LivePushNotificationForPro");
            
        }

        public async Task<HttpStatusCode> SendWindowAzurePushNotification(string pns, string to_tag, string alert, HelperClasses.HelperEnums.PushNotificationType type, string jsonStr)
        {
            try
            {
                //pns = "131afe75d9083dfb60f2cfde49af5e8be28bbd719f77096c6fa8eab6eb83220a";
                //message = "testing testing";
                // string message = "testing testing";
                //var user = "ravi"; //HttpContext.Current.User.Identity.Name;
                //string[] userTag = new string[2];
                //userTag[0] = "username:" + to_tag;
                //userTag[1] = "from:" + user;
                string userTag = to_tag;

                Microsoft.Azure.NotificationHubs.NotificationOutcome outcome = null;
                HttpStatusCode ret = HttpStatusCode.InternalServerError;

                switch (pns.ToLower())
                {
                    case "wns":
                        // Windows 8.1 / Windows Phone 8.1
                        var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                     alert + "</text></binding></visual></toast>";
                        outcome = await Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
                        break;
                    //case "apns":
                    case "ios":
                        // iOS
                        var alert11 = "{\"aps\":{\"content-available\":1,\"alert\":\"" + alert + "\",\"PushNotificationType:\":\"" + (int)type + "\",\"jsonStr\":" + jsonStr + "}}";
                        outcome = await Instance.Hub.SendAppleNativeNotificationAsync(alert11, userTag);
                        break;
                    //case "gcm":
                    case "android":
                        // Android
                        var notif = "{ \"data\":{\"alert\":\"" + alert + "\",\"PushNotificationType:\":\"" +(int) type + "\",\"jsonStr\":" + jsonStr + "}}";
                        outcome = await Instance.Hub.SendGcmNativeNotificationAsync(notif, userTag);
                        break;
                }

                if (outcome != null)
                {
                    if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                        (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                    {
                        ret = HttpStatusCode.OK;
                    }
                }

                return ret;

            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }


        }

    }
}
