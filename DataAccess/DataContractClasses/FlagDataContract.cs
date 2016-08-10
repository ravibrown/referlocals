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
    public class FlagJobListWrapper
    {
        public List<FlagJobDataContract> Jobs { get; set; }
        
        public bool HideShowMore { get; set; }
    }
    public class FlagJobDataContract
    {
        public long? FlagID { get; set; }
        public long? JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public HelperEnums.JobStatus JobStatus { get; set; }
        public string JobImage { get; set; }
        public string UrlFriendlyTitle
        {
            get { return Common.TextToFriendlyUrl(JobTitle); }
        }
    }

    public class FlagProfessionalListWrapper
    {
        public List<FlagProfessionalDataContract> Professionals{ get; set; }

        public bool HideShowMore { get; set; }
    }
    public class FlagUserListWrapper
    {
        public List<FlagUserDataContract> Users { get; set; }

        public bool HideShowMore { get; set; }
    }
    public class FlagUserDataContract: CommonFlagUserData
    {  
        public string UserLocation { get; set; }

    }
    public class FlagProfessionalDataContract: CommonFlagUserData
    {
        public List<ProfessionalUrls> Categories { get; set; }

    }
    public class CommonFlagUserData
    {
        public long? FlagID { get; set; }
        public long? UserID { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }

    }
}
