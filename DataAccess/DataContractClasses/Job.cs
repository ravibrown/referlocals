using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataContracts
{
    public class JobListWrapper
    {
        public List<DataContracts.Job> Jobs { get; set; }
        public int JobCount { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class Job
    {
        public string UrlFriendlyTitle
        {
            get { return Common.TextToFriendlyUrl(Title); }
        }
        public int JobQuoteCount { get; set; }
        public long Id { get; set; }
        public long? SubCategoryId { get; set; }
        public List<string> SubCategoryName { get; set; }
        public long? CityID { get; set; }
        public string CityName { get; set; }

        public string ZipName { get; set; }
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public long? UserId { get; set; }

        public bool? IsApprovedByAdmin { get; set; }
        public int? JobStatus { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        private string _CreatedDateString;
        public string CreatedDateString
        {
            get
            {
                _CreatedDateString = CreatedDate.GetValueOrDefault().ToLongDateString();
                return _CreatedDateString;
            }

        }

        public long? CreatedBy { get; set; }





    }
    public class UserJobDataContractList
    {
        public List<UserJobDataContract> Jobs { get; set; }
        public int JobCount { get; set; }
        public bool HideShowMore { get; set; }
        
    }
    public class JobDataContractList
    {
        public List<JobDataContract> Jobs { get; set; }
        public int totalCount { get; set; }
    }
    public class UserJobDataContract:JobDataContract
    {
        public List<ProfessionalUrls> SubcategoryNames { get; set; }
        //public List<long?> SubcategoryIDs { get; set; }
        public int? JobStatus { get; set; }
        public int QuoteCount { get; set; }
        public int NewQuoteCount { get; set; }
    }
    public class JobWithEstimateDataContract : UserJobDataContract
    {
        
        public long? QuoteID { get; set; }
        public decimal? Estimate { get; set; }
        public string Comment{ get; set; }
    }
        public class JobDataContract
    {
        //public string UrlFriendlyTitle
        //{
        //    get { return Common.TextToFriendlyUrl(Title); }
        //}
        //public int JobQuoteCount { get; set; }
        
        public long Id { get; set; }
        public string JobUrl { get { return "/jobdetail/" + Id + "/" + Common.TextToFriendlyUrl(Title); } }
        public bool IsFavorite { get; set; }
        public bool IsFlag { get; set; }
        //public List<string> SubCategoryName { get; set; }
        public long? UserID { get; set; }
        public string CityName { get; set; }
        public long? LocationID { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone{ get; set; }
        public string ZipName { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string Title { get; set; }
        public string FormattedTitle { get {return Common.TextToFriendlyUrl(Title); } }
        public string Description { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }
        public long? LoggedInUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
        private string _CreatedDateString;
        public string CreatedDateString
        {
            get
            {
                _CreatedDateString = CreatedDate.GetValueOrDefault().ToLongDateString();
                return _CreatedDateString;
            }

        }

        public string FormattedDateString
        {
            get
            {
                return  CreatedDate.GetValueOrDefault().ToString();

            }

        }
        public string FormattedDateTimeString
        {
            get
            {
                return String.Format("{0:m}", CreatedDate.GetValueOrDefault()) + " | "+ String.Format("{0:t}", CreatedDate.GetValueOrDefault())  ;
                
            }

        }


    }

}
