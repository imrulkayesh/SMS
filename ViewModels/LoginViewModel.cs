using System.ComponentModel.DataAnnotations;

namespace SMS.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? UserId { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string? Password { get; set; }
    }
}
