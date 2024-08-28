using System.ComponentModel.DataAnnotations;
namespace SMS.ViewModels
{
    public class UserViewModel
    {
        [Display(Name ="User Name")]
        [Required(ErrorMessage ="{0} is Required")]
        public string? UserName { get; set; }
        [Display(Name ="Designation")]
        [Required(ErrorMessage ="{0} is Required")]
        public string? Designation { get; set; }
        [Display(Name ="User Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? UserId { get; set; }
        [Display(Name ="Password")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? Password { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? NewPassword { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? ConfirmPassword { get; set; }
        [Display(Name ="Old Password")]
        [Required(ErrorMessage = "{0} is Required")]
        public string? OldPassword { get; set; }  

    }
}
