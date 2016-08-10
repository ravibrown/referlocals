using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Message")]
    public class NotificationSettingController : ApiController
    {

        [Route("Save")]
        public ResultData Save(NotificationSettingBindingModel model)
        {
            NotificationSetting setting= new NotificationSetting();
            return setting.Save(model.ID, model.UserID, model.NewFollowers, model.Recommendations, model.NewMessage, model.EstimateFeedback, model.AppointmentRequest);
        }
        [Route("GetByUserID")]
        public NotificationSettingDataContract GetByUserID(long? userID)
        {
            NotificationSetting setting = new NotificationSetting();
            return setting.GetByUserID(userID);
        }

    }
}
