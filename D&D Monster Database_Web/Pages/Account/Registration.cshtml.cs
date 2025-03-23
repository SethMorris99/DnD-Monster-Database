using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public Registration NewUser { get; set; } = new Registration(); 
        
        public void OnGet()
        {
            NewUser.Username = "John";
        }
        public void OnPost()
        {

        }
    }
}
