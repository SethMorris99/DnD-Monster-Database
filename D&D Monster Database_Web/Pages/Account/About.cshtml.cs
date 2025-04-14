using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class AboutModel : PageModel
    {
        public string Layout { get; private set; }

        public void OnGet()
        {
            // Check if the user is authenticated
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Layout = "~/Pages/Shared/_Layout.Authenticated.cshtml";
            }
            else
            {
                Layout = "~/Pages/Shared/_Layout.cshtml";
            }
        }
    }
}
