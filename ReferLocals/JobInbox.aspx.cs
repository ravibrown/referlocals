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
    public partial class JobInbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static JobMessagesDataContractListWrapper GetJobInbox (int pageIndex, int pageSize)
        {
            Messages msg= new Messages();
            return msg.GetJobMessages(SessionService.Pull(SessionKeys.UserId), pageIndex, pageSize);
        }
    }
}