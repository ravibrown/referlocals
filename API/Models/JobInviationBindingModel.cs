using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class JobInviationBindingModel
    {

        public long ProfessionalID { get; set; }
        public string Username { get; set; }
        public string JobUrls { get; set; }
    }
}