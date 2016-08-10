using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataContracts
{
    public class ManualCustomersDataContract
    {
        public long? ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string Apartment { get; set; }
        public string Location { get; set; }
        public long? UserID { get; set; }
        public string CountryCode { get; set; }
    }
    public class ManualCustomerIDDataContract:ResultData
    {
        public long ID { get; set; }
    }
    public class ManualCustomerWrapper
    {
        public List<ManualCustomersDataContract> Customers { get; set; }
        public int CustomerCount { get; set; }
        public bool HideShowMore { get; set; }
    }
    public class ManualCustomerNotesDataContract
    {
        public long? ID { get; set; }
        public string Notes { get; set; }
        public long? ManualCustomerID { get; set; }
        public HelperEnums.CustomerType CustomerType { get; set; }
    }
    public class ManualCustomerNotesWrapper
    {
        public List<ManualCustomerNotesDataContract> CustomerNotes { get; set; }
        public int NotesCount { get; set; }
        public bool HideShowMore { get; set; }
    }
}
