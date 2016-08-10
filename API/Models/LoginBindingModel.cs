using DataAccess.HelperClasses;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    

    public class SocialLoginBindingModel:ProfileBindingModel
    {
        public HelperEnums.UserType UserType { get; set; }
        public string SocialID { get; set; }
        public HelperEnums.SocialType SocialType{ get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string DeviceToken { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginBindingModel
    {
      
        public string Email { get; set; }
        public long CountryCode { get; set; }
        public string Phone { get; set; }

        [Required]
        public string Platform { get; set; }
        [Required]
        public string DeviceToken { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LogoutBindingModel
    {
        
        public string Platform { get; set; }

        
        public string DeviceToken { get; set; }

        [Required]
        public long UserID { get; set; }
    }
}