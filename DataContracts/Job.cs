using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class JobListWrapper
    {
        public List<Job> Jobs { get; set; }
        public int JobCount { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class Job
    {
        public string UrlFriendlyTitle {
            get { return HttpUtility.(Title); } }
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
            get {
                _CreatedDateString = CreatedDate.GetValueOrDefault().ToLongDateString();
                return _CreatedDateString; }
           
        }

        public long? CreatedBy { get; set; }





    }
}
