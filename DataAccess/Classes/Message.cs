using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Classes
{
    public class Messages : dbContext
    {
        public long? ID { get; set; }
        public long? SenderID { get; set; }
        public string Message { get; set; }
        public long? JobID { get; set; }
        public bool? IsJobMessage { get; set; }
        public DateTime? MessageOn { get; set; }

        private void SaveThreadParticipants(long? threadID, long? userID)
        {
            tbl_ThreadParticipants participants = new tbl_ThreadParticipants();
            participants.ThreadID = threadID;
            participants.UserID = userID;
            db.tbl_ThreadParticipants.Add(participants);
            db.SaveChanges();

        }

        public long? GetJobIDByThreadID(long? threadID)
        {
            var msg = db.tbl_Messages.FirstOrDefault(p => p.ThreadID == threadID);
            if (msg != null)
            {
                return msg.JobID;
            }
            return null;
        }

        private long? SaveThread(long? senderID, long? receiverID, bool? isJobMsg, long? jobID)
        {
            tbl_Threads thread = new tbl_Threads();
            thread.CreatedOn = DateTime.UtcNow;
            thread.IsJobThread = isJobMsg;
            thread.JobID = jobID;
            db.tbl_Threads.Add(thread);
            db.SaveChanges();
            if (thread.ID > 0)
            {
                SaveThreadParticipants(thread.ID, senderID);
                SaveThreadParticipants(thread.ID, receiverID);
                return thread.ID;
            }
            return 0;
        }
        private long? GetThreadID(long? senderID, long? receiverID, bool? isJobMsg, long? jobID)
        {
            var threadID = (from p in db.tbl_ThreadParticipants

                            where (p.UserID == senderID || p.UserID == receiverID)
                            && p.tbl_Threads.IsJobThread == isJobMsg
                            && p.tbl_Threads.JobID == jobID
                            group p by p.ThreadID into participants
                            select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault();
            if (threadID.HasValue)
            {
                if (threadID.Value > 0)
                {
                    return threadID.Value;
                }
                return SaveThread(senderID, receiverID, isJobMsg, jobID);
            }
            return SaveThread(senderID, receiverID, isJobMsg, jobID);

        }


        /// <summary>
        /// if you dont have threadID then pass 0 or null
        /// </summary>
        public InboxDataContract Save(long? senderID, long? receiverID, string message, long? jobID, bool? isJobMessage, long? threadID = 0)
        {
            if (threadID.HasValue)
            {
                if (threadID.Value == 0)
                {
                    threadID = GetThreadID(senderID, receiverID, isJobMessage, jobID);
                }
            }
            // var threadID = GetThreadID(senderID, receiverID,isJobMessage);
            if (threadID > 0)
            {
                tbl_Messages objMessage = new tbl_Messages();
                objMessage.MessageOn = DateTime.UtcNow;
                objMessage.IsViewed = false;
                objMessage.SenderID = senderID;
                objMessage.ThreadID = threadID;
                objMessage.IsDeletedByReceiver = false;
                objMessage.IsDeletedBySender = false;
                objMessage.Message = message;
                objMessage.JobID = jobID;
                objMessage.IsJobMessage = isJobMessage;
                db.tbl_Messages.Add(objMessage);
                db.SaveChanges();
                var participants = db.tbl_User.Where(p => p.Id == senderID || p.Id == receiverID).ToList();
                var loggedInUserData = new tbl_User();
                var receiverUserData = new tbl_User();
                if (participants.Count > 0)
                {
                    loggedInUserData = participants.FirstOrDefault(p => p.Id == senderID);
                    receiverUserData = participants.FirstOrDefault(p => p.Id == receiverID);
                }
                tbl_Jobs jobData = null;
                if (jobID.GetValueOrDefault() > 0)
                {
                    jobData = db.tbl_Jobs.FirstOrDefault(p => p.Id == jobID);

                }
                var data = new InboxDataContract
                {
                    JobID = jobID,
                    ResultDescription = "Message Sent Successfully",
                    ResultStatus = 1,
                    loggedInUser = senderID,
                    ThreadID = threadID,
                    ID = objMessage.ID,
                    Message = objMessage.Message,
                    MessageOn = objMessage.MessageOn,
                    UserID = objMessage.SenderID,
                    Username = loggedInUserData.FirstName + " " + loggedInUserData.LastName,
                    UserImage = !string.IsNullOrEmpty(loggedInUserData.Image) ? Common.UserImagesPath + loggedInUserData.Image : Common.NoImageIcon
                };
                var dataForPush = new MessageDataContractForPushNotification
                {
                    IsJobMessage = isJobMessage,
                    JobID = jobID,
                    JobImage = jobData != null ? !string.IsNullOrEmpty(jobData.Image) ? Common.JobImagesPath + jobData.Image : Common.JobDefaultImage : "",
                    JobTitle = jobData != null ? Common.TextToFriendlyUrl(jobData.Title) : "",
                    ResultDescription = "Message Sent Successfully",
                    ResultStatus = 1,
                    loggedInUser = senderID,
                    ThreadID = threadID,
                    ID = objMessage.ID,
                    Message = objMessage.Message,
                    MessageOn = objMessage.MessageOn,
                    UserID = objMessage.SenderID,
                    Username = loggedInUserData.FirstName + " " + loggedInUserData.LastName,
                    UserImage = !string.IsNullOrEmpty(loggedInUserData.Image) ? Common.UserImagesPath + loggedInUserData.Image : Common.NoImageIcon,
                    NewUserUpdates = receiverUserData.Type==1? new User().GetNewUserUpdates(receiverUserData.Id):null,
                    NewProfessionalUpdates =receiverUserData.Type==2? new User().GetNewProfessionalUpdates(receiverUserData.Id):null,
                };
                var emailbody = Common.EmailBodyOnMessageReceived.Replace("##ReceiverUsername##", receiverUserData.FirstName + " " + receiverUserData.LastName)
                    .Replace("##SenderUsername##", loggedInUserData.FirstName + " " + loggedInUserData.LastName)
                    .Replace("##MessageLink##", Common.WebsiteHostNameForLink + "/inbox");
                Common.SendEmailWithGenericTemplate(receiverUserData.Email, Common.EmailSubjectOnMessageReceived, emailbody);

                NotificationSetting notificationSettings = new NotificationSetting();
                var notificationSettingData = notificationSettings.GetByUserID(receiverID);
                if (notificationSettingData != null)
                {
                    
                        if (notificationSettingData.NewMessage.GetValueOrDefault())
                        {
                            Task.Run(async () =>
                            {
                                if (receiverUserData.Type == (int)HelperEnums.UserType.Professional)
                                {
                                    var jsonString = JsonConvert.SerializeObject(dataForPush);
                                    WindowAzurePushNotification.NotificationsForPro pro = new WindowAzurePushNotification.NotificationsForPro();
                                    if (isJobMessage.Value)
                                    {
                                        await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new job message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                    }
                                    else
                                    {
                                        await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        var jsonString = JsonConvert.SerializeObject(dataForPush);
                                        WindowAzurePushNotification.Notifications userPush = new WindowAzurePushNotification.Notifications();
                                        if (isJobMessage.Value)
                                        {
                                            await userPush.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new job message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                        }
                                        else
                                        {
                                            await userPush.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            });
                        }
                }
                else
                {
                    Task.Run(async () =>
                    {
                        if (receiverUserData.Type == (int)HelperEnums.UserType.Professional)
                        {
                            var jsonString = JsonConvert.SerializeObject(dataForPush);
                            WindowAzurePushNotification.NotificationsForPro pro = new WindowAzurePushNotification.NotificationsForPro();
                            if (isJobMessage.Value)
                            {
                                await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new job message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                            }
                            else
                            {
                                await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                            }
                        }
                        else
                        {
                            try
                            {
                                var jsonString = JsonConvert.SerializeObject(dataForPush);

                                WindowAzurePushNotification.Notifications userPush = new WindowAzurePushNotification.Notifications();
                                if (isJobMessage.Value)
                                {
                                    await userPush.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new job message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                }
                                else
                                {
                                    await userPush.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        //var jsonString = JsonConvert.SerializeObject(dataForPush);
                        //WindowAzurePushNotification.NotificationsForPro pro = new WindowAzurePushNotification.NotificationsForPro();
                        //if (isJobMessage.Value)
                        //{
                        //    await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new job message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                        //}
                        //else
                        //{
                        //    await pro.SendWindowAzurePushNotification(receiverUserData.Platform, Convert.ToString(receiverUserData.Id), "You have a new message from " + data.Username, HelperEnums.PushNotificationType.Message, jsonString);
                        //}
                    });
                }


                return data;//new ResultData { ResultDescription = "Message Sent Successfully", ResultStatus = 1 };
            }
            else
            {
                return new InboxDataContract { ResultDescription = "Oops.. some technical problem", ResultStatus = 0 };
            }


        }

        public long? GetReceiverID(long? threadID, long? senderID)
        {
            var data = db.tbl_ThreadParticipants.Where(p => p.ThreadID == threadID).ToList();

            if (data != null)
            {
                var receiver = data.FirstOrDefault(p => p.UserID != senderID);
                if (receiver != null)
                {
                    return receiver.UserID;
                }
                return 0;
            }
            return 0;
        }
        public InboxDataContractListWrapper GetChat(long? userID, long? threadID, int pageIndex, int pageSize)
        {
            UpdateMessagesIsViewed(threadID,userID);

            var query = (from m in db.tbl_Messages
                         join u in db.tbl_User
                         on m.SenderID equals u.Id
                         where m.ThreadID == threadID
                         && m.tbl_Threads.tbl_ThreadParticipants.Count(p => p.UserID == userID) > 0
                         select new InboxDataContract
                         {
                             JobID = m.JobID,
                             IsRepliedByLoggedInUser = userID == m.SenderID,
                             loggedInUser = userID,
                             ThreadID = m.ThreadID,
                             ID = m.ID,
                             Message = m.Message,
                             MessageOn = m.MessageOn,
                             UserID = m.SenderID,
                             Username = u.FirstName + " " + u.LastName,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon
                         });
            var countInboxMsgs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countInboxMsgs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList().OrderBy(p => p.ID).ToList();

            InboxDataContractListWrapper inbox = new InboxDataContractListWrapper();
            inbox.Count = countInboxMsgs;
            if (totalpages <= (pageIndex + 1))
            {
                inbox.HideShowMore = true;
            }
            else
            {
                inbox.HideShowMore = false;
            }

            inbox.Inbox = data;
            return inbox;
        }
        public void UpdateMessagesIsViewed(long? threadID,long? userID)
        {
            db.Database.ExecuteSqlCommand("update tbl_Messages set isviewed=1 where threadID=@threadID and senderID!=@userID", new object[]{ new SqlParameter("@threadID", threadID),new SqlParameter("@userID", userID)});
        }
        public InboxDataContractListWrapper GetMyInbox(long? userID, int pageIndex, int pageSize)
        {

            var query = (from m in db.tbl_Messages
                         join u in db.tbl_User
                         on m.SenderID equals u.Id
                         join p in db.tbl_ThreadParticipants
                         on m.ThreadID equals p.ThreadID

                         where (from msgParticipants in db.tbl_ThreadParticipants
                                join msg in db.tbl_Messages
                                on msgParticipants.ThreadID equals msg.ThreadID
                                where msgParticipants.UserID == userID && msg.IsJobMessage == false
                                group msg by msgParticipants.ThreadID into participants
                                select participants.Max(p => p.ID)
                                ).Contains(m.ID)
                                && m.SenderID != p.UserID
                         select new InboxDataContract
                         {
                             UnreadMsgCount=db.tbl_Messages.Count(p=>p.IsViewed==false&&p.ThreadID==m.ThreadID&&m.SenderID!=userID),
                             IsRepliedByLoggedInUser = userID == m.SenderID,
                             ThreadID = m.ThreadID,
                             ID = m.ID,
                             userType=p.UserID==userID?m.tbl_User.Type:p.tbl_User.Type,
                             Message = m.Message,
                             MessageOn = m.MessageOn,
                             UserID = p.UserID == userID ? m.SenderID : p.UserID,
                             Username = p.UserID == userID ? m.tbl_User.FirstName + " " + m.tbl_User.LastName : p.tbl_User.FirstName + " " + p.tbl_User.LastName,
                             UserImage = p.UserID == userID ? !string.IsNullOrEmpty(m.tbl_User.Image) ? Common.UserImagesPath + m.tbl_User.Image : Common.NoImageIcon : !string.IsNullOrEmpty(p.tbl_User.Image) ? Common.UserImagesPath + p.tbl_User.Image : Common.NoImageIcon
                         });





            var countInboxMsgs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countInboxMsgs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            InboxDataContractListWrapper inbox = new InboxDataContractListWrapper();
            inbox.Count = countInboxMsgs;
            if (totalpages <= (pageIndex + 1))
            {
                inbox.HideShowMore = true;
            }
            else
            {
                inbox.HideShowMore = false;
            }

            inbox.Inbox = data;
            return inbox;
        }
        public JobMessagesDataContractListWrapper GetJobMessages(long? userID, int pageIndex, int pageSize)
        {

            var query = (from m in db.tbl_Messages
                         join u in db.tbl_User
                         on m.SenderID equals u.Id
                         join p in db.tbl_ThreadParticipants
                         on m.ThreadID equals p.ThreadID
                         join j in db.tbl_Jobs
                        on m.JobID equals j.Id
                         where (from msgParticipants in db.tbl_ThreadParticipants
                                join msg in db.tbl_Messages
                                on msgParticipants.ThreadID equals msg.ThreadID
                                where msgParticipants.UserID == userID && msg.IsJobMessage == true
                                group msg by msg.JobID into participants
                                select participants.Max(p => p.ID)
                                ).Contains(m.ID)
                                && m.SenderID != p.UserID
                         select new JobMessagesDataContract
                         {
                             UnreadMsgCount = db.tbl_Messages.Count(p => p.IsViewed == false && p.JobID== j.Id&& m.SenderID != userID),
                             IsRepliedByLoggedInUser = userID == m.SenderID,
                             loggedInUser = userID,
                             ThreadID = m.ThreadID,
                             ID = m.ID,
                             Message = m.Message,
                             MessageOn = m.MessageOn,
                             JobID = j.Id,
                             JobTitle = j.Title,
                             JobImage = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage
                         });





            var countInboxMsgs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countInboxMsgs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            JobMessagesDataContractListWrapper inbox = new JobMessagesDataContractListWrapper();
            inbox.Count = countInboxMsgs;
            if (totalpages <= (pageIndex + 1))
            {
                inbox.HideShowMore = true;
            }
            else
            {
                inbox.HideShowMore = false;
            }

            inbox.Inbox = data;
            return inbox;
        }
        public InboxDataContractListWrapper GetJobMessagesByJobID(long? jobID, long? userID, int pageIndex, int pageSize)
        {

            var query = (from m in db.tbl_Messages
                         join u in db.tbl_User
                         on m.SenderID equals u.Id
                         join p in db.tbl_ThreadParticipants
                         on m.ThreadID equals p.ThreadID

                         where (from msgParticipants in db.tbl_ThreadParticipants
                                join msg in db.tbl_Messages
                                on msgParticipants.ThreadID equals msg.ThreadID
                                where msgParticipants.UserID == userID && msg.IsJobMessage == true && msg.JobID == jobID
                                group msg by msgParticipants.ThreadID into participants
                                select participants.Max(p => p.ID)
                                ).Contains(m.ID)
                                && m.SenderID != p.UserID
                         select new InboxDataContract
                         {
                             UnreadMsgCount = db.tbl_Messages.Count(p => p.IsViewed == false && p.ThreadID == m.ThreadID&&p.JobID==jobID && m.SenderID != userID),
                             JobID = jobID,
                             IsRepliedByLoggedInUser = userID == m.SenderID,
                             ThreadID = m.ThreadID,
                             ID = m.ID,
                             Message = m.Message,
                             MessageOn = m.MessageOn,
                             UserID = p.UserID == userID ? m.SenderID : p.UserID,
                             Username = p.UserID == userID ? m.tbl_User.FirstName + " " + m.tbl_User.LastName : p.tbl_User.FirstName + " " + p.tbl_User.LastName,
                             UserImage = p.UserID == userID ? !string.IsNullOrEmpty(m.tbl_User.Image) ? Common.UserImagesPath + m.tbl_User.Image : Common.NoImageIcon : !string.IsNullOrEmpty(p.tbl_User.Image) ? Common.UserImagesPath + p.tbl_User.Image : Common.NoImageIcon
                         });





            var countInboxMsgs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countInboxMsgs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            InboxDataContractListWrapper inbox = new InboxDataContractListWrapper();
            inbox.Count = countInboxMsgs;
            if (totalpages <= (pageIndex + 1))
            {
                inbox.HideShowMore = true;
            }
            else
            {
                inbox.HideShowMore = false;
            }

            inbox.Inbox = data;
            return inbox;
        }
    }

}
