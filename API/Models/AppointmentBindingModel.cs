using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class AppointmentBindingModel
    {

        public long? ID { get; set; }
        [Required]
        public long? QuoteID { get; set; }
        public string Message { get; set; }
        //public bool? IsAcceptedByProfessional { get; set; }
        [Required]
        public long? ProfessionalID { get; set; }
        [Required]
        public bool? IsRescheduled { get; set; }
        [Required]
        public bool? RescheduledByUser { get; set; }
        [Required]
        public bool? RescheduledByProfessional { get; set; }
        [Required]
        public long? UserId { get; set; }

        public List<DateTime?> AppointmentDates { get; set; }
    }
}