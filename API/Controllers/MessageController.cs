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
    public class MessageController : ApiController
    {
        [Route("UpdateIsViewed")]
        public ResultData UpdateIsViewed(MessageIsViewedBindingModel model)
        {
            Messages msg = new Messages();
            msg.UpdateMessagesIsViewed(model.ThreadID,model.UserID);
            return new ResultData { ResultDescription = "Updated Successfully", ResultStatus = 1 };
        }
        [Route("Send")]
        public InboxDataContract Send(MessageBindingModel model)
        {
            Messages msg = new Messages();
            return msg.Save(model.SenderID, model.ReceiverID, model.Message, model.JobID, model.IsJobMessage, model.ThreadID);
        }

        [Route("GetMyInbox")]
        public InboxDataContractListWrapper GetMyInbox(long? userID, int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetMyInbox(userID, pageIndex, pageSize);
        }

        [Route("GetInboxMessages")]
        public InboxDataContractListWrapper GetInboxChat(long? userID, long? threadID, int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetChat(userID, threadID, pageIndex, pageSize);
        }
        [Route("GetJobMessages")]
        public JobMessagesDataContractListWrapper GetJobMessages(long? userID, int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetJobMessages(userID, pageIndex, pageSize);
        }
        [Route("GetJobMessagesByJobID")]
        public InboxDataContractListWrapper GetJobMessagesByJobID(long? jobID, long? userID, int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetJobMessagesByJobID(jobID, userID, pageIndex, pageSize);
        }


    }
}
