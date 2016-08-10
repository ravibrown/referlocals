using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContractClasses
{

    public class ReferralDataContract:ResultData
    {
        public List<ProfessionalDataContract> UserList { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsHavingSameCategory { get; set; }
        public long UserID{ get; set; }
    }
    public class TopReferralDataContract 
    {
        public List<ProfessionalDataContract> UserList { get; set; }
        
        public bool HideShowMore{ get; set; }
    }
    public class ReferralWithUserDataWrapper
    {
        public List<ReferralWithUserData> ReferralList { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class ReferralWithUserData
    {
        
        public HelperEnums.UserType UserType{ get; set; }
        public string   Location { get; set; }
        public long? LocationID { get; set; }
        public bool IsFavorite { get; set; }
        public string Comment { get; set; }
        public long ID { get; set; }
        public bool? IsFlag { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public string ReferralMessage { get; set; }
        public long? UserID { get; set; }
        public bool? IsSatisfied { get; set; }
        public bool? IsReferred { get; set; }
        public string ShareUrl { get; set; }
        public List<ProfessionalUrls> SubCategories { get; set; }
    }
}
