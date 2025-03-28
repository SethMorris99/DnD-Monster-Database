using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Account
{
    public class Sign_InModel : PageModel
    {
        [BindProperty]
        public Sign_In Login { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Check if the user is in the database
                //If the user is in the database, redirect to the home page
                //If the user is not in the database, redirect to the sign in page
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString())) 
                { 
                    string cmdText = "SELECT UserID UserPassword FROM [SystemUser] WHERE UserEmail = @UserEmail";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@UserEmail", Login.Username);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string passwordHash = reader.GetString(1);
                        if (AppHelper.VerifyPassword(Login.Username, passwordHash))
                        {
                            return RedirectToPage("Account/Profile");
                        }
                        else
                        {
                            ModelState.AddModelError("LoginError", "Invalid credintials.");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "Invalid credintials.");
                        return Page();
                    }
                }
            }
           else
           {
               return Page();
           }
        }
    }
}
