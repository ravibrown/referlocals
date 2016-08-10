using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataContracts
{
    public class JobMessagesDataContractListWrapper
    {
        public List<JobMessagesDataContract> Inbox { get; set; }
        public int Count { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class JobMessagesDataContract : ResultData
    {
        public int UnreadMsgCount { get; set; }
        public bool IsRepliedByLoggedInUser { get; set; }
        public long? ThreadID { get; set; }
        public long? ID { get; set; }
        public string Message { get; set; }
        public long? loggedInUser { get; set; }
        [IgnoreDataMember]
        public DateTime? MessageOn { get; set; }
        public string FormattedDateString
        {
            get
            {
                return MessageOn.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");

            }

        }
        public string FormattedMessageOn { get { return MessageOn.Value.ToLongDateString() + " " + MessageOn.Value.ToLongTimeString(); } }
        public string JobTitle { get; set; }
        public string JobImage { get; set; }
        public long? JobID { get; set; }
    }


    public class InboxDataContractListWrapper
    {
        public List<InboxDataContract> Inbox { get; set; }
        public int Count { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class InboxDataContract:ResultData
    {
        public int UnreadMsgCount { get; set; }
        public bool IsRepliedByLoggedInUser { get; set; }
        public long? ThreadID { get; set; }
        public long? ID { get; set; }
        public string Message { get; set; }
        public long? loggedInUser { get; set; }
        [IgnoreDataMember]
        public DateTime? MessageOn{ get; set; }
        public string FormattedDateString
        {
            get
            {
                return MessageOn.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");

            }

        }
        public string FormattedMessageOn { get { return MessageOn.Value.ToLongDateString()+" "+ MessageOn.Value.ToLongTimeString(); } }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public long? userType { get; set; }
        public  long? UserID { get; set; }
        public long? JobID { get; set; }
    }

    public class NewUpdateWithPush : ResultData
    {
        public NewUserUpdates NewUserUpdates { get; set; }
        public NewProfessionalUpdates NewProfessionalUpdates { get; set; }

    }
    public class MessageDataContractForPushNotification : NewUpdateWithPush
    {
        public bool? IsJobMessage { get; set; }
        public bool IsRepliedByLoggedInUser { get; set; }
        public long? ThreadID { get; set; }
        public long? ID { get; set; }
        public string Message { get; set; }
        public long? loggedInUser { get; set; }
        [IgnoreDataMember]
        public DateTime? MessageOn { get; set; }
        public string FormattedDateString
        {
            get
            {
                return MessageOn.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");

            }

        }
        public string FormattedMessageOn { get { return MessageOn.Value.ToLongDateString() + " " + MessageOn.Value.ToLongTimeString(); } }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public long? UserID { get; set; }
        public long? JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobImage { get; set; }
    }


}
