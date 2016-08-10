
using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ProfessionalProfileBindingModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string Appartment{ get; set; }
        public long CityId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long CountryCode { get; set; }
        public string CompanyName { get; set; }
        public string Image { get; set; }
        public string WebSite { get; set; }
        public List<long> CitiesIServeIds { get; set; }
        public List<long> SubCategoryIds { get; set; }
    }
    public class ProfessionalSubcategoryBindingModel
    {
        public List<long?> SubCategoryIds { get; set; }
        public long UserId { get; set; }
    }
}
