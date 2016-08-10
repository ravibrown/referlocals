
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class UserSignupBindingModel
    {

        
        public string Password { get; set; }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //PhoneNumber
        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        //CountryCode
        private Int64? _CountryCode = 0;


        public Int64? CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }



    }
}
