using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ForgotPasswordBindingModel
    {
        public string Email { get; set; }
        public long CountryCode { get; set; }
        public string Phone { get; set; }
    }
    
}