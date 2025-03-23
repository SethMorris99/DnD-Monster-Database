using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class Sign_InModel : PageModel
    {
        public Sign_In Login { get; set; }
        public void OnGet()
        {
        }
    }
}
