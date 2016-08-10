using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class NotificationSetting : dbContext
    {

        public ResultData Save(long? id, long? userID, bool? newFollowers, bool? recommendations, bool? newMessage,
            bool? estimateFeedback, bool? appointmentRequest)
        {
            //tbl_NotificationSettings setting = new tbl_NotificationSettings();

            //if (id.GetValueOrDefault() > 0)
            //{
            //    setting = db.tbl_NotificationSettings.FirstOrDefault(p => p.ID == id);
            //}
            tbl_NotificationSettings setting = db.tbl_NotificationSettings.FirstOrDefault(p => p.UserID == userID);
            if (setting == null)
            {
                setting = new tbl_NotificationSettings();
            }
            setting.UpdatedOn = DateTime.UtcNow;

            setting.NewFollowers = newFollowers;
            setting.NewMessage = newMessage;
            setting.Recommendations = recommendations;
            setting.AppointmentRequest = appointmentRequest;
            setting.EstimateFeedback = estimateFeedback;

            setting.UserID = userID;
            if (setting.ID == 0)
            {
                db.tbl_NotificationSettings.Add(setting);
            }
            //if (id.GetValueOrDefault() == 0)
            //{
            //    db.tbl_NotificationSettings.Add(setting);
            //}
            db.SaveChanges();

            return new ResultData { ResultDescription = "Saved Successfully", ResultStatus = 1 };
        }

        public NotificationSettingDataContract GetByUserID(long? userID)
        {
            var data = (from p in db.tbl_NotificationSettings
                        where p.UserID == userID
                        select new NotificationSettingDataContract
                        {
                            UserID = p.UserID,
                            AppointmentRequest = p.AppointmentRequest,
                            EstimateFeedback = p.EstimateFeedback,
                            ID = p.ID,
                            NewFollowers = p.NewFollowers,
                            NewMessage = p.NewMessage,
                            Recommendations = p.Recommendations
                        }).FirstOrDefault();
            return data;
        }
    }

}
