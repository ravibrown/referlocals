using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContractClasses
{

    public class AppointmentDataContract
    {

        public long? ID { get; set; }
        public long? QuoteID { get; set; }
        public string Message { get; set; }
        public bool? IsAcceptedByProfessional { get; set; }
        public long? ProfessionalID { get; set; }
        public long? UserID { get; set; }
        public bool? IsRescheduled { get; set; }
        public bool? RescheduledByUser { get; set; }
        public bool? RescheduledByProfessional { get; set; }
        public DateTime? AddedOn { get; set; }
    }
    public class AppointmentDatesListWrapper
    {
        public long? QuoteID { get; set; }
        public long? ID { get; set; }

        public List<AppointmentDatesDataContrant> Dates { get; set; }
        public long? AppointmentID { get; set; }
    }
    public class AppointmentWithDatesDataContract : AppointmentDataContract
    {
        public List<AppointmentDatesDataContrant> Dates { get; set; }
    }

    public class AppointmentDatesDataContrant
    {
        public long? ID { get; set; }
        public bool? IsAcceptedByUser { get; set; }
        public bool? IsAcceptedByProfessional { get; set; }
        public long? AppointmentID { get; set; }
        public string AppointmentDate
        {
            get
            {
                //return AppointmentDateTime.GetValueOrDefault().ToString("MMMM dd, yyyy");
                return AppointmentDateTime.GetValueOrDefault().Date.ToString("yyyy-MM-dd");
                //return String.Format("{0:u}", AppointmentDateTime.GetValueOrDefault().Date);

            }
        }

        public DateTime? AppointmentDateTime { get; set; }
        public string AppointmentTime { get { return AppointmentDateTime.GetValueOrDefault().ToString("hh:mm tt"); } }

    }

    public class AppointmentDatesWithJobDataContract : AppointmentDatesDataContrant
    {
        public long? QuoteID{ get; set; }
        public long? JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobAddress { get; set; }
        public string JobLocation { get; set; }
        public string UrlFriendlyJobTitle { get { return Common.TextToFriendlyUrl(JobTitle); } }
        public long? UserID { get; set; }
        public long? ProfessionalID { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class AppointmentDatesWithJobListWrapper
    {
        public List<AppointmentDatesWithJobDataContract> DatesWithJob { get; set; }
        public bool HideShowMore { get; set; }
        public int AppointmentCount { get; set; }
    }


}
