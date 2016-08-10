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
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {


        [Route("Send")]
        public ResultData Send(AppointmentBindingModel model)
        {
            if (model.AppointmentDates.Count > 0)
            {

                Appointment appointment = new Appointment();
                if (model.ID > 0)
                {
                    appointment.DeleteAppointmentDates(model.ID);
                }

                var appointmentID = appointment.Save(model.ID, model.QuoteID, model.Message,false, model.ProfessionalID
                     , model.IsRescheduled, model.RescheduledByUser, model.RescheduledByProfessional, model.UserId);
                //var appointmentID = appointment.Save(model.ID, model.QuoteID, model.Message, false, model.ProfessionalID
                //    , false, false, false, model.UserId);



                if (appointmentID > 0)
                {
                    foreach (var item in model.AppointmentDates)
                    {
                        appointment.SaveAppointmentDate(appointmentID, item);
                    }
                    Quote quote = new Quote();
                    quote.AcceptRejectQuote(model.QuoteID.GetValueOrDefault(), HelperEnums.QuoteStatus.Accepted, model.IsRescheduled.GetValueOrDefault());

                }
                return new ResultData { ResultDescription = "success", ResultStatus = 1 };
            }
            else
            {
                return new ResultData { ResultDescription = "Please select atlease one date", ResultStatus = 0 };
            }
        }

        [Route("GetAppointmentDatesByQuoteID")]
        [HttpGet]
        public AppointmentWithDatesDataContract GetAppointmentDatesByQuoteID(long quoteID)
        {
            
            Appointment appointment = new Appointment();
            return appointment.GetAppointmentDatesByQuoteID(quoteID);
        }
        [Route("AcceptAppointmentDate")]
        public ResultData AcceptAppointmentDate(long id,HelperEnums.UserType userType)
        {

            Appointment appointment = new Appointment();
           var data= appointment.AcceptAppointmentDate(id,userType);
            if (data == 0)
            {
                return new ResultData { ResultDescription = "invalid appointment date id", ResultStatus = 0 };
            }
           return new ResultData { ResultDescription = "success", ResultStatus = 1 };
        }

        [Route("GetPastAppointmentsForGivenDate")]
        [HttpGet]
        public AppointmentDatesWithJobListWrapper GetPastAppointmentsForGivenDate(HelperEnums.UserType userType,
            long userID, int pageIndex,
            int pageSize, DateTime givenDate)
        {
            Appointment appointment = new Appointment();
            return appointment.GetPastAppointmentsForGivenDate( userType,
                 userID, pageIndex, pageSize, givenDate);
        }

        [Route("GetAppointmentsForGivenDate")]
        [HttpGet]
        public List<AppointmentDatesWithJobDataContract> GetAppointmentsForGivenDate(HelperEnums.UserType userType,long userID, HelperEnums.DateType dateType, int pageIndex, int pageSize,DateTime givenDate, int month = 0)
        {
            Appointment appointment = new Appointment();
            return appointment.GetAppointmentsForGivenDate((HelperEnums.DateType)(dateType), userType,
                 userID, pageIndex, pageSize,givenDate, month);
        }
        [Route("GetAppointmentsRequestForProfessional")]
        [HttpGet]
        public QuoteWithUserListWrapper GetAppointmentsRequestForProfessional( long professionalID, int pageIndex, int pageSize)
        {
            Quote appointment = new Quote();
            
            return appointment.GetAppointmentRequestsForProfessional(professionalID, pageIndex, pageSize);
                
        }

        [Route("GetAppointmentsRequestForUser")]
        [HttpGet]
        public QuoteListWrapper GetAppointmentsRequestForUser( long userID, int pageIndex, int pageSize)
        {
            Quote appointment = new Quote();

            return appointment.GetAppointmentRequestsForUser(userID, pageIndex, pageSize);

        }

        [Route("GetCustomerList")]
        [HttpGet]
        public AppointmentRequestUserDataList GetUserDataForAppointmentsRequestForProfessional(long professionalID, int pageIndex, int pageSize)
        {
            Quote appointment = new Quote();

            return appointment.GetUserDataAppointmentRequestsForProfessional(professionalID, pageIndex, pageSize);

        }

    }
}
