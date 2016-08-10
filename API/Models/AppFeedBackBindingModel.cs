using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AppFeedBackBindingModel
    {
        [Required]
        public long? UserID { get; set; }
        [Required]
        public string FeedBack { get; set; }
        [Required]
        public bool IsProAppFeedback { get; set; }
    }


}