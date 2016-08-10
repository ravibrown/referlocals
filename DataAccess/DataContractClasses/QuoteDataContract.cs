using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContractClasses
{

    public class EstimateDataContract
    {
        public string JobTitle { get; set; }
        public string UrlFriendlyJobTitle { get { return Common.TextToFriendlyUrl(JobTitle); } }
        public long? ID { get; set; }
        public long? JobID { get; set; }
        public decimal? Estimate { get; set; }
        public string Comments { get; set; }
        public int? EstimateStatus { get; set; }
        public int? JobStatus { get; set; }
        public string Location { get; set; }
        public string JobImage { get; set; }
        public string JobLocation { get; set; }
        public string JobAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserEmailAddress { get; set; }
        public bool IsAccepted { get; set; }
    }
    public class EstimateListWrapper
    {
        public List<EstimateDataContract> EstimateList { get; set; }
        public bool HideShowMore { get; set; }
        public int EstimateCount { get; set; }
    }
    public class QuoteDataContract
    {
        public string JobTitle { get; set; }
        public string JobImage { get; set; }
        public long? ID { get; set; }
        public long? JobID { get; set; }
        public bool? RescheduledByProfessional { get; set; }
        public bool? RescheduledByUser { get; set; }
        public long? ProfessionalID { get; set; }
        public string ProfessionalUniqueID { get; set; }
        public decimal? Estimate { get; set; }
        public string Comments { get; set; }
        public int? Status { get; set; }
        public DateTime? AddedOn { get; set; }
        public string AddedOnString { get { return AddedOn.GetValueOrDefault().ToLongDateString() + AddedOn.GetValueOrDefault().ToLongTimeString(); } }
        public long? AppointmentID { get; set; }
        public long? UserID { get; set; }

    }
    public class QuoteWithProfessionalDataContract : QuoteDataContract
    {
        public long? ThreadID{ get; set; }
        public string ProfessionalName { get; set; }
        public string ProfessionalImage { get; set; }
    }
    public class QuoteWithUserDataContract : QuoteDataContract
    {

        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
    public class QuoteWithUserListWrapper
    {
        public List<QuoteWithUserDataContract> QuoteList { get; set; }
        public bool HideShowMore { get; set; }
        public int QuoteCount { get; set; }
    }
    public class QuoteListWrapper
    {
        public List<QuoteWithProfessionalDataContract> QuoteList { get; set; }
        public bool HideShowMore { get; set; }
        public int QuoteCount { get; set; }
    }

    public class AppointmentRequestUserDataList
    {
        public List<AppointmentRequestUserData> UserList { get; set; }
        public bool HideShowMore { get; set; }
        public int UserCount { get; set; }
    }
    public class AppointmentRequestUserData
    {
        public long?ThreadID{ get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        [IgnoreDataMember]
        public long? QuoteID { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public long? UserID { get; set; }

    }

}
