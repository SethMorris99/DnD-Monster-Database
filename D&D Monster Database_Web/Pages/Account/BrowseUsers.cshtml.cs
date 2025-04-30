using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class BrowseUsersModel : PageModel
    {
        public List<AccountView> Users { get; set; } = new List<AccountView>();
        public void OnGet()
        {
            PopulateUserList();
        }

        private void PopulateUserList()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"
            SELECT 
                SystemUserID,
                UserFirstName, 
                UserLastName, 
                UserDisplayName, 
                UserEmail, 
                ProfileImageURL, 
                AccountType.AccountTypeName, 
                LastLoginTime
            FROM SystemUser
            INNER JOIN AccountType ON SystemUser.AccountTypeID = AccountType.AccountTypeID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users.Add(new AccountView
                    {
                        SystemUserID = reader["SystemUserID"].ToString(),  // SystemUserID
                        FirstName = reader["UserFirstName"].ToString(),    // UserFirstName
                        LastName = reader["UserLastName"].ToString(),      // UserLastName
                        Username = reader["UserDisplayName"].ToString(),   // UserDisplayName
                        Email = reader["UserEmail"].ToString(),            // UserEmail
                        ProfileImageURL = reader["ProfileImageURL"].ToString(), // ProfileImageURL
                        AccountType = reader["AccountTypeName"].ToString(), // AccountTypeName
                        LastLoginTime = Convert.ToDateTime(reader["LastLoginTime"]) // LastLoginTime
                    });
                }

                reader.Close();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                if (User.IsInRole("Admin") == false)
                {
                    return RedirectToPage("/Account/AccessDenied");
                }
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    string cmdText = "DELETE FROM SystemUser WHERE SystemUserID = @SystemUserID";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@SystemUserID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Refresh the user list after deletion
                PopulateUserList();

                // Redirect back to the same page to reflect changes
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error deleting user: {ex.Message}");

                // Optionally, add a model error to display a message to the user
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");
                return Page();
            }
        }
    }
}
