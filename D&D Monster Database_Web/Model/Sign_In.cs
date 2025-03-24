using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class Sign_In
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username {  get; set; }
        [Required(ErrorMessage = "Please enter your password")]

        public string Password { get; set; }
    }
}
