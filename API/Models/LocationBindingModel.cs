using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LocationBindingModel
    {
        [Required]
        public long? UserID { get; set; }
        [Required]
        public long? LocationID{ get; set; }
        
    }

    
}