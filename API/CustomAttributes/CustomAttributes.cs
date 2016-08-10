using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.CustomAttributes
{
    public class CustomAttributes
    {
        public class OneRequeriedFromBoth: ValidationAttribute
        {

        }
    }
}