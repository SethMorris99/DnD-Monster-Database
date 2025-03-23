using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class Registration
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}