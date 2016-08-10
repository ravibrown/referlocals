using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class NotificationSettingBindingModel
    {
        [Required]
        public long? UserID { get; set; }
        
        public long? ID { get; set; }

        [Required]
        public bool? NewMessage { get; set; }
        [Required]
        public bool? Recommendations { get; set; }
        [Required]
        public bool? NewFollowers { get; set; }
        [Required]
        public bool? EstimateFeedback { get; set; }
        [Required]
        public bool? AppointmentRequest { get; set; }
    }

    
}