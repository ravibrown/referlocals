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
    [RoutePrefix("api/Job")]
    public class JobController : ApiController
    {
        [Route("SendJobInvitation")]
        public ResultData SendJobInvitation(JobInviationBindingModel model)
        {
            string newJobUrl = string.Empty;

            User user = new DataAccess.Classes.User();
            var data = user.GetUserDetails(model.ProfessionalID);
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Email))
                {
                    foreach (var jobUrl in model.JobUrls.Split(','))
                    {
                        newJobUrl += "<br/>" + Common.WebsiteHostNameForLink + jobUrl + "<br/>";

                    }
                    EmailMessage msg = new EmailMessage();
                    msg.To = data.Email;
                    msg.Subject = Common.SubjectJobInvitation.Replace("##Username##", model.Username);


                    msg.Message = Environment.NewLine + Common.BodyJobInvitation.Replace("##Username##", model.Username)
                        .Replace("##JOBURL##", newJobUrl);
                    Common.SendEmail(msg);
                    return new ResultData { ResultDescription = "Invitation Sent Successfully", ResultStatus = 1 };
                }
                else
                {
                    foreach (var jobUrl in model.JobUrls.Split(','))
                    {
                        newJobUrl += Common.WebsiteHostNameForLink + jobUrl + Environment.NewLine;

                    }
                    Common.SendCodeThroughSms(data.CountryCode + data.PhoneNumber, Common.SMSBodyJobInvitation.Replace("##Username##", model.Username).Replace("##JOBURL##", newJobUrl));
                    return new ResultData { ResultDescription = "Invitation Sent Successfully", ResultStatus = 1 };
                }
            }
            return new ResultData { ResultDescription = "Invitee not found", ResultStatus = 0 };
        }

        [Route("Add")]
        public ResultData Add(JobBindingModel model)
        {
            Jobs job = new Jobs();
            return job.Save(model.JobTitle, model.Description, model.Image, model.Location, model.Address, model.JobID, model.UserId, model.SubCategoryIDs);
        }

        [Route("Search")]
        public JobDataContractList Search(JobSearchBindingModel searchModel)
        {
            Jobs job = new Jobs();
            return job.Search(searchModel.loggedInUserID, searchModel.SubCategoryIDs, searchModel.LocationId, searchModel.PageIndex, searchModel.PageSize);

        }
        [Route("GetAll")]
        public JobDataContractList GetAll(long loggedInUserID, int pageIndex, int pageSize)
        {
            Jobs job = new Jobs();
            return job.GetAllJobs(loggedInUserID, pageIndex, pageSize);

        }

        [Route("GetUserJobs")]
        [HttpGet]
        public UserJobDataContractList GetUserJobs(HelperEnums.JobStatus jobstatus, long userID, int pageIndex, int pageSize)
        {
            Jobs job = new Jobs();
            return job.GetUserJobs((int)jobstatus, userID, pageIndex, pageSize);

        }

        [Route("GetJobArchive")]
        [HttpGet]
        public UserJobDataContractList GetJobArchive(long userID, int pageIndex, int pageSize)
        {
            Jobs job = new Jobs();
            return job.GetJobArchive((int)HelperEnums.JobStatus.Done, userID, pageIndex, pageSize);

        }


        [Route("GetJobByJobID")]
        [HttpGet]
        public JobWithEstimateDataContract GetJobByJobID(long jobID, long userID)
        {
            Jobs job = new Jobs();
            return job.GetJobByJobID(jobID, userID);

        }

        [Route("ChangeJobStatus")]
        public ResultData ChangeJobStatus(JobStatusChangeBindingModel model)
        {
            Jobs job = new Jobs();
            return job.ChangeJobStatus((int)model.JobStatus, model.JobID.GetValueOrDefault());
        }
    }
}
