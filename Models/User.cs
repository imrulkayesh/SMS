using System.ComponentModel.DataAnnotations;

namespace SMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="User Id")]
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public string? UserName { get; set; }
        public string? Designation { get; set; }
        // Add other properties as needed
    }
}
