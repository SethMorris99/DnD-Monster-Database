using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


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
                string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MonsterDatabase;Trusted_Connection = True;";
                SqlConnection conn = new SqlConnection(connectionString);

                //2. Create a command to insert the data
                string cmdText = "INSERT INTO SystemUser (SystemUserID, AccountTypeID,UserFirstName,UserLastName,UserProfileImage, UserEmail, UserPassword) VALUES (@SystemUserID, @AccountTypeID, @UserFirstName,@UserLastName,@UserProfileImage, @UserEmail, @UserPassword)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@SystemUserID", 1);
                cmd.Parameters.AddWithValue("@AccountTypeID", 1);
                cmd.Parameters.AddWithValue("@UserFirstName", NewUser.Username);
                cmd.Parameters.AddWithValue("@UserLastName", NewUser.Username);
                cmd.Parameters.AddWithValue("@UserProfileImage", "default.jpg");
                cmd.Parameters.AddWithValue("@UserEmail", NewUser.Email);
                cmd.Parameters.AddWithValue("@UserPassword", NewUser.Password);

                //3. Execute the command 
                cmd.ExecuteNonQuery();

                //4. Close the connection 
                conn.Close();

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
