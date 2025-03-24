using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        
        public Registration NewUser { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save to database (add database logic here)
            return RedirectToPage("Sign_In");
        }
    }
}
