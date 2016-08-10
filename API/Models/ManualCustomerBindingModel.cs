using DataAccess.HelperClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ManualCustomerNoteDataBindingModel
    {
        [Required]
        public string Notes { get; set; }
        [Required]
        public long? CustomerID { get; set; }
        [Required]
        public HelperEnums.CustomerType CustomerType { get; set; }
    }
    public class ManualCustomerBindingModel
    {

        public long? ID { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string Apartment { get; set; }
        public long? LocationID { get; set; }
        public long? UserID { get; set; }
        public string CountryCode { get; set; }
    }


}