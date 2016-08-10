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
    [RoutePrefix("api/Estimate")]
    public class EstimateController : ApiController
    {


        [Route("Send")]
        public ResultData Send(EstimateBindingModel model)
        {
            Quote quote = new Quote();
            quote.Save(model.ID, model.JobID, model.ProfessionalID, model.Estimate, model.Comments, model.Status);
            return new ResultData { ResultDescription = "success", ResultStatus = 1 };
        }
        [Route("GetEstimatesByJob")]
        [HttpGet]
        public QuoteListWrapper EstimatesByJob(long jobID, int pageIndex, int pageSize)
        {
            Quote quote = new Quote();
            return quote.GetJobQuotes(jobID, pageIndex, pageSize, 0);

        }
        [Route("AcceptRejectQuote")]
        public ResultData AcceptRejectQuote(EstimateDeclineBindingModel model)
        {
            Quote quote = new Quote();
            quote.AcceptRejectQuote(model.QuoteID,model.Status,false);
            return new ResultData { ResultDescription = "success", ResultStatus = 1 };

        }
        [Route("GetEstimatesForProfessional")]
        [HttpGet]
        public EstimateListWrapper GetEstimatesForProfessional(long userID,HelperEnums.QuoteStatus status, int pageIndex, int pageSize)
        {
            Quote quote = new Quote();
           return quote.GetEstimatesForProfessional(userID, status, pageIndex, pageSize);
            

        }
        

    }
}
