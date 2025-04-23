using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class Registration
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public String firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public String lastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{10,}$",
        ErrorMessage = "Password must be at least 10 characters long, contain at least one number, and include both upper-case and lower-case letters.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}