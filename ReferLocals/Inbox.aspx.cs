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
    public partial class Inbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static InboxDataContractListWrapper GetInbox (int pageIndex, int pageSize)
        {
            Messages msg= new Messages();
            return msg.GetMyInbox(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
        [WebMethod]
        public static InboxDataContractListWrapper GetJobInbox(long? jobID,int pageIndex, int pageSize)
        {
            Messages msg = new Messages();
            return msg.GetJobMessagesByJobID(jobID,SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
    }
}