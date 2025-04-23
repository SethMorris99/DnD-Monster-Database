using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;


namespace D_D_Monster_Database_Web.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public Registration NewUser { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //Validate User Input
            if (ModelState.IsValid)
            {
                //Save to Database 
                //1. Create a connection to the databse 
               // string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MonsterDatabase;Trusted_Connection = True;";
                
                //make a local variable 
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    //2. Create a command to insert the data
                    string cmdText = "INSERT INTO SystemUser (AccountTypeID,UserFirstName,UserLastName,UserDisplayName,UserProfileImage,ProfileImageURL, UserEmail, UserPassword, LastLoginTime) VALUES (@AccountTypeID, @UserFirstName,@UserLastName,@UserDisplayName,@UserProfileImage,@ProfileImageURL, @UserEmail, @UserPassword, @LastLoginTime)";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@AccountTypeID", 1);
                    cmd.Parameters.AddWithValue("@UserFirstName", NewUser.firstName);
                    cmd.Parameters.AddWithValue("@UserLastName", NewUser.lastName);
                    cmd.Parameters.AddWithValue("@UserDisplayName", NewUser.Username);
                    cmd.Parameters.AddWithValue("@UserProfileImage", "default.jpg");
                    cmd.Parameters.AddWithValue("@ProfileImageURL", AppHelper.GetDefaultProfilePicture());
                    cmd.Parameters.AddWithValue("@UserEmail", NewUser.Email);
                    cmd.Parameters.AddWithValue("@UserPassword", AppHelper.GeneratePasswordHash(NewUser.Password));
                    cmd.Parameters.AddWithValue("@LastLoginTime", DateTime.Now);
                    // check if the password and confirm password match
                    // no need to use hashing or hide that the error was due to password mismatch
                    // since it is registration and not login
                    if (NewUser.Password != NewUser.ConfirmPassword)
                    {
                        ModelState.AddModelError("PasswordError", "Passwords do not match.");
                        return Page();
                    }
                    if(NewUser.Password.Length < 10)
                    {
                        ModelState.AddModelError("PasswordError", "Password must be at least 10 characters long.");
                        return Page();
                    }

                    //3. Execute the command 
                    cmd.ExecuteNonQuery();
                }
                //Redirect to Sign_In Page
                return RedirectToPage("/Account/Sign_In");
            }
            else
            {
                //Redirect the user 
                return Page();
            }
        }
    }
}
