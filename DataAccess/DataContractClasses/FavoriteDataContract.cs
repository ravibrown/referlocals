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
    public class FavoriteJobListWrapper
    {
        public List<FavoriteJobDataContract> Jobs { get; set; }
        public int Count { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class FavoriteJobDataContract
    {
        public long? FavoriteID { get; set; }
        public long? JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public string JobDescription { get; set; }
        public HelperEnums.JobStatus JobStatus { get; set; }
        public string JobImage { get; set; }
        public string UrlFriendlyTitle
        {
            get { return Common.TextToFriendlyUrl(JobTitle); }
        }
    }

    public class FavoriteProfessionalListWrapper
    {
        public List<FavoriteProfessionalDataContract> Professionals{ get; set; }
        public int Count { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class FavoriteUserListWrapper
    {
        public List<FavoriteUserDataContract> Users { get; set; }
        public int Count { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class FavoriteUserDataContract: CommonFavoriteUserData
    {
        public long? ThreadID{ get; set; }
        public string UserLocation { get; set; }

    }
    public class FavoriteProfessionalDataContract: CommonFavoriteUserData
    {
        public long? ThreadID { get; set; }
        public List<ProfessionalUrls> Categories { get; set; }

    }
    public class CommonFavoriteUserData
    {
        public bool IsFavoriteByMe { get; set; }
        public long? FavoriteID { get; set; }
        public long? UserID { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }

    }
}
