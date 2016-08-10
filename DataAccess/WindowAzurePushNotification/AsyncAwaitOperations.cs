using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PushSharp;
//using PushSharp.Android;
//using PushSharp.Apple;
//using PushSharp.Core;
using Newtonsoft.Json.Linq;
using DataAccess.Classes;
using DataAccess.HelperClasses;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace DataAccess.WindowAzurePushNotification
{
    public class AsyncAwaitOperations
    {
        User cm;
        public AsyncAwaitOperations()
        {
            cm = new User();
        }



        public async Task AddUpdateWindowAzureTokenForUser(Int64 userid,string deviceToken,string platform)
        {
          
            User cm = new User();
           
            Task.Run(() =>
            {
               
                cm.AddUpdateWindowAzureTokenForUser(userid,deviceToken,platform);
            });
           
        }
        public async Task AddUpdateWindowAzureTokenForPro(Int64 userid, string deviceToken, string platform)
        {

            User cm = new User();

            Task.Run(() =>
            {

                cm.AddUpdateWindowAzureTokenForPro(userid, deviceToken, platform);
            });

        }


    }
}
