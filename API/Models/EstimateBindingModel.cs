using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class EstimateBindingModel
    {
        
        public long? ID { get; set; }
        [Required]
        public long? JobID { get; set; }
        [Required]
        public long? ProfessionalID { get; set; }
        [Required]
        public decimal? Estimate { get; set; }
        public string Comments { get; set; }
        public DataAccess.HelperClasses.HelperEnums.QuoteStatus Status { get; set; }
    }
    public class EstimateDeclineBindingModel
    {
        public long QuoteID { get; set; }
        public DataAccess.HelperClasses.HelperEnums.QuoteStatus Status { get; set; }
    }
    
}