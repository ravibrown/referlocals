using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class FavoriteBindingModel
    {
        [Required]
        public long? UserID { get; set; }
        [Required]
        public long? FavoriteTypeID { get; set; }
        [Required]
        public DataAccess.HelperClasses.HelperEnums.FavoriteType FavoriteType{ get; set; }
        
        [Required]
        public bool Status { get; set; }
    }

    
}