using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class jobquotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static QuoteListWrapper GetJobQuotes(long jobID,int pageIndex,int pageSize,int orderBy)
        {
            Quote quote = new Quote();
            return quote.GetJobQuotes(jobID, pageIndex, pageSize, orderBy);
        }
        [WebMethod]
        public static void AcceptRejectQuote(long id,bool status)
        {
            Quote quote = new Quote();
            var QuoteStatus =status==true? HelperEnums.QuoteStatus.Accepted:HelperEnums.QuoteStatus.Rejected;
             quote.AcceptRejectQuote(id,QuoteStatus,false);
            
        }
        [WebMethod]
        public static void SendMessage(long msgReceiverID,long jobID, string msg)
        {
            Messages message = new Messages();
            message.Save(SessionService.Pull(SessionKeys.UserId), msgReceiverID, msg, jobID, true);
        }

    }
}