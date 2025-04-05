using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class ProfileModel : PageModel
    {
        // Simulating fetching user data from the database.
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MemberSince { get; set; }

        public void OnGet()
        {
            // Simulate fetching data from the database or user service.
            UserName = User.Identity.Name;
            Email = "user@example.com"; // Replace with dynamic data from your database
            MemberSince = "January 2025"; // Replace with actual registration date
        }
    }
}