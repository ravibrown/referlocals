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
    [RoutePrefix("api/Flag")]
    public class FlagController : ApiController
    {
       
        [Route("Save")]
        public ResultData Save(FlagBindingModel model)
        {
            Flag flag = new Flag();
           return flag.Save(model.UserID, model.FlagTypeID, model.FlagType,model.FlagReason);
        }

       
    }
}
