using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class FlagBindingModel
    {
        [Required]
        public long? UserID { get; set; }
        [Required]
        public long? FlagTypeID { get; set; }
        [Required]
        public DataAccess.HelperClasses.HelperEnums.FlagType FlagType{ get; set; }

        public string FlagReason { get; set; }
    }

    
}