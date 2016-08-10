using System.ComponentModel.DataAnnotations;
using DataAccess.HelperClasses;
namespace API.Models
{
    public class SearchReferralBindingModel
    {
        public long SubCategoryID { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public long UserID { get; set; }
        public HelperEnums.UserType UserType   { get; set; }

    }
    public class ReferralBindingModel
    {
        public long UserID { get; set; }
        public HelperEnums.UserType UserType { get; set; }
        public bool IsSatisfied{ get; set; }
        public bool IsReferred { get; set; }
        public string Comment { get; set; }
        public long LocationID { get; set; }
        public long SenderID { get; set; }
    }

}