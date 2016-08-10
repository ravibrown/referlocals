using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ResetPasswordBindingModel
    {
        [Required]
        public long UserID { get; set; }
        [Required]
        public string VerificationCode { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
    public class ChangePwdBindingModel
    {
        [Required]
        public long UserID { get; set; }
        [Required]
        public string OldPassword{ get; set; }
        [Required]
        public string NewPassword { get; set; }

    }

}