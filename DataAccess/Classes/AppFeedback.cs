using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class AppFeedback : dbContext
    {
        public ResultData Save(long? userID, string feedback, bool IsProAppFeedback)
        {
            var feedbackCount = db.tbl_AppFeedBack.Count(pp => pp.UserID == userID);
            if (feedbackCount > 0)
            {
                return new ResultData { ResultDescription = "You have already rated the app", ResultStatus = 0 };
            }
            else
            {
                tbl_AppFeedBack appfeedback = new tbl_AppFeedBack();
                appfeedback.AddedOn = DateTime.UtcNow;
                appfeedback.UserID = userID;
                appfeedback.FeedBack = feedback;
                db.tbl_AppFeedBack.Add(appfeedback);
                db.SaveChanges();
                User user = new User();
               var username= user.GetUsernameByUserID(userID.GetValueOrDefault());
                if (IsProAppFeedback)
                {
                    Common.SendEmailWithGenericTemplate(Common.FromEmail, Common.SubjectProAppFeedback, "Hi <br/>" + username + " submitted following feedback for Pro App:<br/><br/>" + feedback);
                }
                else
                {
                    Common.SendEmailWithGenericTemplate(Common.FromEmail, Common.SubjectUserAppFeedback, "Hi <br/>" + username + " submitted following feedback for User App:<br/><br/>" + feedback);
                }
                return new ResultData { ResultDescription = "Feedback submitted successfully", ResultStatus = 1 };
            }

        }
    }
}
