using DataAccess.Classes;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static InboxDataContractListWrapper GetChat(long? threadID, int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetChat(SessionService.Pull(SessionKeys.UserId), threadID, pageIndex, pageSize);
        }
        [WebMethod]
        public static InboxDataContract SendMessage(long? threadID, string message)
        {
            Messages msg = new Messages();
            var receiverID = msg.GetReceiverID(threadID,SessionService.Pull(SessionKeys.UserId));
            var jobID = msg.GetJobIDByThreadID(threadID);
            bool isJobMsg = false;
            if (jobID.HasValue)
            {
                isJobMsg = true;
            }
          return  msg.Save(SessionService.Pull(SessionKeys.UserId), receiverID, message, jobID, isJobMsg,threadID);
        }
    }
}