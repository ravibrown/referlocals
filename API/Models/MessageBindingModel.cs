using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MessageIsViewedBindingModel
    {
        [Required]
        public long? ThreadID { get; set; }

        [Required]
        public long? UserID{ get; set; }
    }
    public class MessageBindingModel
    {
        [Required]
        public long? SenderID { get; set; }
        [Required]
        public long? ReceiverID { get; set; }
        [Required]
        public string   Message{ get; set; }
        public long? JobID{ get; set; }
        [Required]
        public bool? IsJobMessage { get; set; }
        public long? ThreadID { get; set; }
    }

    
}