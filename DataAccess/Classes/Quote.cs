using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Quote : dbContext
    {
        public void Save(QuoteDataContract quote)
        {
            tbl_Quotes tblquote = new tbl_Quotes();
            if (quote.ID.GetValueOrDefault() > 0)
            {
                tblquote = db.tbl_Quotes.FirstOrDefault(p => p.ID == quote.ID);
            }
            else
            {
                tblquote.AddedOn = DateTime.UtcNow;
            }
            tblquote.IsViewed = false;
            tblquote.JobID = quote.JobID;
            tblquote.ProfessionalID = quote.ProfessionalID;
            tblquote.Estimate = quote.Estimate;
            tblquote.Comments = quote.Comments;

            tblquote.Status = (int)HelperClasses.HelperEnums.QuoteStatus.Nothing;
            if (quote.ID.GetValueOrDefault() == 0)
            {
                db.tbl_Quotes.Add(tblquote);
            }
            db.SaveChanges();
            SendEmailOnEstimateReceive(tblquote.ID);
        }

        private void SendEmailOnEstimateReceive(long quoteID)
        {
            var quoteData = db.tbl_Quotes.Include("tbl_Jobs").Include("tbl_User").FirstOrDefault(p => p.ID == quoteID);
            if (quoteData != null)
            {
                var username = quoteData.tbl_Jobs.tbl_User.FirstName + " " + quoteData.tbl_Jobs.tbl_User.LastName;
                var professionalName = quoteData.tbl_User.FirstName + " " + quoteData.tbl_User.LastName;
                var jobtitle = Common.TextToFriendlyUrl(quoteData.tbl_Jobs.Title);
                string emailBody = Common.EmailBodyOnEstimateReceive.Replace("##Username##", username).Replace("##ProfessionalName##", professionalName).Replace("##JobLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + quoteData.JobID + "/" + jobtitle).Replace("##EstimateLink##", Common.WebsiteHostNameForLink + "/jobquotes/" + quoteData.JobID + "/" + jobtitle);
                Common.SendEmailWithGenericTemplate(quoteData.tbl_Jobs.tbl_User.Email, Common.EmailSubjectOnEstimateReceive, emailBody);
            }
            #region PushNoitification

            Task.Run(async () =>
            {
                
                var userData = quoteData.tbl_Jobs.tbl_User;


                var dataForPush = new NewUpdateWithPush
                {
                    NewProfessionalUpdates = new User().GetNewProfessionalUpdates(userData.Id)

                };
                var jsonString = JsonConvert.SerializeObject(dataForPush);
                DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                {
                    await pro.SendWindowAzurePushNotification(userData.Platform, Convert.ToString(userData.Id), quoteData.tbl_User.FirstName + " " + quoteData.tbl_User.LastName + " sent you an estimate for your job", HelperEnums.PushNotificationType.EstimateReceived, jsonString);
                }

            });

            #endregion
        }
        public void Save(long? ID, long? jobID, long? professionalID, decimal? estimate, string comments, HelperEnums.QuoteStatus status)
        {
            tbl_Quotes tblquote = new tbl_Quotes();
            if (ID.GetValueOrDefault() > 0)
            {
                tblquote = db.tbl_Quotes.FirstOrDefault(p => p.ID == ID);
            }
            else
            {
                tblquote.AddedOn = DateTime.UtcNow;
            }
            tblquote.IsViewed = false;
            tblquote.JobID = jobID;
            tblquote.ProfessionalID = professionalID;
            tblquote.Estimate = estimate;
            tblquote.Comments = comments;

            tblquote.Status = (int)status;
            if (ID.GetValueOrDefault() == 0)
            {
                db.tbl_Quotes.Add(tblquote);
            }
            db.SaveChanges();
            SendEmailOnEstimateReceive(tblquote.ID);
        }
        public QuoteDataContract GetQuote(long quoteID)
        {
            var quoteData = db.tbl_Quotes.FirstOrDefault(p => p.ID == quoteID);

            var obj = Mapper.Map<tbl_Quotes, QuoteDataContract>(quoteData);
            if (quoteData != null)
            {
                obj.UserID = quoteData.tbl_Jobs.tbl_User.Id;

            }
            return obj;
        }

        public QuoteDataContract GetQuoteByJobAndProfessionalID(long professionalID, long jobID)
        {
            var quoteData = db.tbl_Quotes.FirstOrDefault(p => p.JobID == jobID && p.ProfessionalID == professionalID);
            return Mapper.Map<tbl_Quotes, QuoteDataContract>(quoteData);
        }
        public void AcceptRejectQuote(long quoteID, HelperClasses.HelperEnums.QuoteStatus quoteStatus, bool isReschedule)
        {
            tbl_Quotes tblquote = db.tbl_Quotes.Include("tbl_User").FirstOrDefault(p => p.ID == quoteID);
            tblquote.Status = (int)quoteStatus;
            db.SaveChanges();
            if (quoteStatus == HelperEnums.QuoteStatus.Accepted)
            {
                Jobs job = new Jobs();
                job.ChangeJobStatus((int)HelperEnums.JobStatus.Done, tblquote.JobID.GetValueOrDefault(), tblquote.ProfessionalID.GetValueOrDefault());

                #region PushNoitification
                if (!isReschedule)
                {
                    Task.Run(async () =>
                    {
                        var proData = tblquote.tbl_User;
                        NotificationSetting notificationSettings = new NotificationSetting();
                        var notificationSettingData = notificationSettings.GetByUserID(proData.Id);
                        if (notificationSettingData != null)
                        {
                            if (notificationSettingData.AppointmentRequest.GetValueOrDefault())
                            {
                                var userData = tblquote.tbl_Jobs.tbl_User;
                                var username = userData.FirstName + " " + userData.LastName;

                                var jsonString = "{}";
                                DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                {
                                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " accepted your Estimate and sent you Appointment request", HelperEnums.PushNotificationType.EstimateAccepted, jsonString);
                                }
                            }

                        }
                        else
                        {
                            var userData = tblquote.tbl_Jobs.tbl_User;
                            var username = userData.FirstName + " " + userData.LastName;

                            var jsonString = "{}";
                            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                            {
                                await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " accepted your Estimate and sent you Appointment request", HelperEnums.PushNotificationType.EstimateAccepted, jsonString);
                            }
                        }
                    });
                }
                #endregion
            }


            else
            {
                var userData = tblquote.tbl_Jobs.tbl_User;
                #region PushNoitification
                if (!isReschedule)
                {
                    Task.Run(async () =>
                {
                    tbl_User proData = tblquote.tbl_User;
                    NotificationSetting notificationSettings = new NotificationSetting();
                    var notificationSettingData = notificationSettings.GetByUserID(tblquote.tbl_User.Id);
                    if (notificationSettingData != null)
                    {
                        if (notificationSettingData.EstimateFeedback.GetValueOrDefault())
                        {

                            var jsonString = "{}";
                            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                            {
                                await pro.SendWindowAzurePushNotification(tblquote.tbl_User.Platform, Convert.ToString(tblquote.tbl_User.Id), userData.FirstName + " " + userData.LastName + " declined your Estimate. You can always Revise and Resend an Estimate.", HelperEnums.PushNotificationType.EstimateDeclined, jsonString);
                            }
                        }

                    }
                    else
                    {

                        var username = userData.FirstName + " " + userData.LastName;

                        var jsonString = "{}";
                        DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                        {
                            await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " declined your Estimate. You can always Revise and Resend an Estimate.", HelperEnums.PushNotificationType.EstimateDeclined, jsonString);
                        }
                    }
                });
                }
                #endregion
                Common.SendEmailWithGenericTemplate(tblquote.tbl_User.Email, Common.EmailSubjectOnQuoteDecline, Common.EmailBodyOnQuoteDecline
                    .Replace("##ProfessionalName##", tblquote.tbl_User.FirstName + " " + tblquote.tbl_User.LastName)
                                    .Replace("##Username##", userData.FirstName + " " + userData.LastName)
                                    .Replace("##ReviseEstimateLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + tblquote.tbl_Jobs.Id + "/" + Common.TextToFriendlyUrl(tblquote.tbl_Jobs.Title))
                                    );
            }
        }
        public QuoteListWrapper GetJobQuotes(long jobID, int pageIndex, int pageSize, int orderby)
        {
            var quoteCount = db.tbl_Quotes.Count(p => p.JobID == jobID && p.Status == (int)HelperEnums.QuoteStatus.Nothing);
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(quoteCount) / Convert.ToDecimal(pageSize));


            var query = (from q in db.tbl_Quotes
                         join u in db.tbl_User
                         on q.ProfessionalID equals u.Id
                         where q.JobID == jobID && q.Status == (int)HelperEnums.QuoteStatus.Nothing
                         select new QuoteWithProfessionalDataContract
                         {
                             ThreadID = (from p in db.tbl_ThreadParticipants
                                         where (p.UserID == q.ProfessionalID || p.UserID == q.tbl_Jobs.UserId)
                                         && p.tbl_Threads.IsJobThread == true
                                         //&& p.tbl_Threads.JobID == null
                                         group p by p.ThreadID into participants
                                         select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),
                             ProfessionalID = q.ProfessionalID,
                             JobID = q.JobID,
                             ID = q.ID,
                             AddedOn = q.AddedOn,
                             Comments = q.Comments,
                             Estimate = q.Estimate,
                             Status = q.Status,
                             ProfessionalName = u.FirstName + " " + u.LastName,
                             ProfessionalImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,

                         });
            if (orderby == 0)
            {
                query = query.OrderBy(p => p.Estimate);
            }
            else
            {
                query = query.OrderByDescending(p => p.Estimate);
            }

            query = query.Skip(pageSize * pageIndex).Take(pageSize);
            var data = query.ToList();

            QuoteListWrapper quotes = new QuoteListWrapper();
            quotes.QuoteCount = quoteCount;
            if (totalpages <= (pageIndex + 1))
            {
                quotes.HideShowMore = true;
            }
            else
            {
                quotes.HideShowMore = false;
            }
            quotes.QuoteCount = quoteCount;
            quotes.QuoteList = data;
            UpdateQuotesIsViewed(data.Select(p => p.ID).ToList());
            return quotes;


        }
        private void UpdateQuotesIsViewed(List<long?> quoteIDs)
        {
            var quoteIDsCSV = String.Join(",", quoteIDs.Select(x => x.ToString()).ToArray());

            db.Database.ExecuteSqlCommand("update tbl_quotes set isviewed=@isviewed where id in(" + quoteIDsCSV + ")", new SqlParameter("@isviewed", 1));

        }

        public QuoteWithUserListWrapper GetAppointmentRequestsForProfessional(long userID, int pageIndex, int pageSize)
        {

            var query = (from p in db.tbl_Jobs
                         join q in db.tbl_Quotes
                         on p.Id equals q.JobID
                         join a in db.tbl_Appointments
                         on q.ID equals a.QuoteID
                         where q.ProfessionalID == userID && q.Status == (int)(HelperEnums.QuoteStatus.Accepted)
                         && a.tbl_AppointmentDates.Count(d => d.IsAcceptedByUser == true || d.IsAcceptedByProfessional == true) == 0
                         select new QuoteWithUserDataContract
                         {
                             AppointmentID = a.ID,
                             RescheduledByProfessional = a.RescheduledByProfessional,
                             RescheduledByUser = a.RescheduledByUser,
                             JobTitle = p.Title,
                             JobID = p.Id,
                             ProfessionalID = q.ProfessionalID,
                             ProfessionalUniqueID = q.tbl_User.UniqueId,
                             ID = q.ID,
                             AddedOn = q.AddedOn,
                             Comments = q.Comments,
                             Estimate = q.Estimate,
                             Status = q.Status,
                             UserName = p.tbl_User.FirstName + " " + p.tbl_User.LastName,
                             UserImage = !string.IsNullOrEmpty(p.tbl_User.Image) ? Common.UserImagesPath + p.tbl_User.Image : "",
                             UserID = p.tbl_User.Id,
                         });

            var QuotesCount = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(QuotesCount) / Convert.ToDecimal(pageSize));


            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            QuoteWithUserListWrapper quotes = new QuoteWithUserListWrapper();
            quotes.QuoteCount = QuotesCount;
            if (totalpages <= (pageIndex + 1))
            {
                quotes.HideShowMore = true;
            }
            else
            {
                quotes.HideShowMore = false;
            }
            quotes.QuoteCount = QuotesCount;
            quotes.QuoteList = data;
            UpdateAppointmentRequestIsViewed(data.Select(p => p.AppointmentID).ToList());
            return quotes;
        }
        public AppointmentRequestUserDataList GetUserDataAppointmentRequestsForProfessional(long userID, int pageIndex, int pageSize)
        {

            var query = (from p in db.tbl_Jobs
                         join q in db.tbl_Quotes
                         on p.Id equals q.JobID
                         join a in db.tbl_Appointments
                         on q.ID equals a.QuoteID
                         join s in db.tbl_State
                         on p.UserId equals s.Id
                         where q.ProfessionalID == userID && q.Status == (int)(HelperEnums.QuoteStatus.Accepted)
                         //&& a.tbl_AppointmentDates.Count(d => d.IsAcceptedByUser == true || d.IsAcceptedByProfessional == true) == 0
                         select new AppointmentRequestUserData
                         {
                             ThreadID = (from p in db.tbl_ThreadParticipants
                                         where (p.UserID == userID || p.UserID == p.tbl_User.Id)
                                         && p.tbl_Threads.IsJobThread == false
                                         //&& p.tbl_Threads.JobID == null
                                         group p by p.ThreadID into participants
                                         select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),

                             UserName = p.tbl_User.FirstName + " " + p.tbl_User.LastName,
                             UserImage = !string.IsNullOrEmpty(p.tbl_User.Image) ? Common.UserImagesPath + p.tbl_User.Image : "",
                             UserID = p.tbl_User.Id,
                             Address = p.tbl_User.StreetAddress,
                             Location = s.City + ", " + s.State,
                             Email = p.tbl_User.Email,
                             Phonenumber = p.tbl_User.CountryCode + p.tbl_User.PhoneNumber,
                             QuoteID = q.ID
                         });

            var userCount = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(userCount) / Convert.ToDecimal(pageSize));


            var data = query.OrderByDescending(p => p.QuoteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            AppointmentRequestUserDataList users = new AppointmentRequestUserDataList();
            users.UserCount = userCount;
            if (totalpages <= (pageIndex + 1))
            {
                users.HideShowMore = true;
            }
            else
            {
                users.HideShowMore = false;
            }

            users.UserList = data;
          
            return users;
        }

        private void UpdateAppointmentRequestIsViewed(List<long?> appointmentIDs)
        {
            var appointmentIDsCSV = String.Join(",", appointmentIDs.Select(x => x.ToString()).ToArray());


            db.Database.ExecuteSqlCommand("update tbl_appointments set isviewed=@isviewed where id in(" + appointmentIDsCSV + ")", new SqlParameter("@isviewed", 1));


        }
        public QuoteListWrapper GetAppointmentRequestsForUser(long userID, int pageIndex, int pageSize)
        {

            var query = (from p in db.tbl_Jobs
                         join q in db.tbl_Quotes
                         on p.Id equals q.JobID
                         join a in db.tbl_Appointments
                         on q.ID equals a.QuoteID
                         where p.UserId == userID && q.Status == (int)(HelperEnums.QuoteStatus.Accepted)
                         && a.tbl_AppointmentDates.Count(d => d.IsAcceptedByUser == true || d.IsAcceptedByProfessional == true) == 0
                         select new QuoteWithProfessionalDataContract
                         {
                             JobID = p.Id,
                             JobImage = !string.IsNullOrEmpty(p.Image) ? Common.JobImagesPath + p.Image : Common.JobDefaultImage,
                             UserID = p.UserId,
                             AppointmentID = a.ID,
                             RescheduledByProfessional = a.RescheduledByProfessional,
                             RescheduledByUser = a.RescheduledByUser,
                             JobTitle = p.Title,
                             ProfessionalID = q.ProfessionalID,
                             ProfessionalUniqueID = q.tbl_User.UniqueId,
                             ID = q.ID,
                             AddedOn = q.AddedOn,
                             Comments = q.Comments,
                             Estimate = q.Estimate,
                             Status = q.Status,
                             ProfessionalName = q.tbl_User.FirstName + " " + q.tbl_User.LastName,
                             ProfessionalImage = !string.IsNullOrEmpty(q.tbl_User.Image) ? Common.UserImagesPath + q.tbl_User.Image : "",
                         });

            var QuotesCount = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(QuotesCount) / Convert.ToDecimal(pageSize));


            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            QuoteListWrapper quotes = new QuoteListWrapper();
            quotes.QuoteCount = QuotesCount;
            if (totalpages <= (pageIndex + 1))
            {
                quotes.HideShowMore = true;
            }
            else
            {
                quotes.HideShowMore = false;
            }
            quotes.QuoteCount = QuotesCount;
            quotes.QuoteList = data;
            return quotes;
        }

        public EstimateListWrapper GetEstimatesForProfessional(long userID, int pageIndex, int pageSize)
        {

            var query = (from p in db.tbl_Jobs
                         join q in db.tbl_Quotes
                         on p.Id equals q.JobID
                         where q.ProfessionalID == userID
                         && p.JobStatus != (int)HelperEnums.JobStatus.Cancel
                         select new EstimateDataContract
                         {
                             Location = p.tbl_State.City + ", " + p.tbl_State.State,
                             JobTitle = p.Title,
                             JobStatus = p.JobStatus,
                             JobID = p.Id,
                             ID = q.ID,
                             Comments = q.Comments,
                             Estimate = q.Estimate,
                             EstimateStatus = q.Status
                         });

            var estimateCount = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(estimateCount) / Convert.ToDecimal(pageSize));


            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            EstimateListWrapper estimates = new EstimateListWrapper();
            estimates.EstimateCount = estimateCount;
            if (totalpages <= (pageIndex + 1))
            {
                estimates.HideShowMore = true;
            }
            else
            {
                estimates.HideShowMore = false;
            }
            estimates.EstimateCount = estimateCount;
            estimates.EstimateList = data;
            return estimates;
        }
        public EstimateListWrapper GetEstimatesForProfessional(long userID, HelperEnums.QuoteStatus status, int pageIndex, int pageSize)
        {

            var query = (from p in db.tbl_Jobs
                         join q in db.tbl_Quotes
                         on p.Id equals q.JobID
                         where q.ProfessionalID == userID && q.Status == (int)status
                         && p.JobStatus != (int)HelperEnums.JobStatus.Cancel
                         select new EstimateDataContract
                         {
                             Location = p.tbl_State.City + ", " + p.tbl_State.State,
                             JobTitle = p.Title,
                             JobStatus = p.JobStatus,
                             JobID = p.Id,
                             ID = q.ID,
                             Comments = q.Comments,
                             Estimate = q.Estimate,
                             EstimateStatus = q.Status,
                             JobAddress = p.Address,
                             JobImage = !string.IsNullOrEmpty(p.Image) ? Common.JobImagesPath + p.Image : Common.JobDefaultImage,
                             JobLocation = p.tbl_State.City + ", " + p.tbl_State.State,
                             UserEmailAddress = p.tbl_User.Email,
                             UserPhone = p.tbl_User.PhoneNumber,
                             IsAccepted = q.tbl_Appointments.FirstOrDefault() != null ? q.tbl_Appointments.FirstOrDefault().tbl_AppointmentDates.Count(d => d.IsAcceptedByUser == true || d.IsAcceptedByProfessional == true) > 0 ? true : false : false
                         });

            var estimateCount = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(estimateCount) / Convert.ToDecimal(pageSize));


            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            EstimateListWrapper estimates = new EstimateListWrapper();
            estimates.EstimateCount = estimateCount;
            if (totalpages <= (pageIndex + 1))
            {
                estimates.HideShowMore = true;
            }
            else
            {
                estimates.HideShowMore = false;
            }
            estimates.EstimateCount = estimateCount;
            estimates.EstimateList = data;
            return estimates;
        }
        public void ChangeAllQuotesStatusToPendingByJobID(long jobID)
        {
            db.Database.ExecuteSqlCommand("delete from  tbl_appointments where quoteID in(select ID from tbl_quotes where jobID=" + jobID + ")");
            db.Database.ExecuteSqlCommand("update tbl_quotes set status=0 where status=1 and jobID=" + jobID + "");

        }
    }

}
