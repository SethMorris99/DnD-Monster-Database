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
                    conn.Open();

                    // Check if the user is an admin (AccountTypeID = 1)
                    string roleCheckQuery = "SELECT AccountTypeID FROM SystemUser WHERE SystemUserID = @SystemUserID";
                    SqlCommand roleCheckCmd = new SqlCommand(roleCheckQuery, conn);
                    roleCheckCmd.Parameters.AddWithValue("@SystemUserID", id);
                    int accountTypeId = Convert.ToInt32(roleCheckCmd.ExecuteScalar());

                    if (accountTypeId == 1)
                    {

                        int adminCount = GetAdminCount(conn);

                        if (adminCount <= 1)
                        {
                            TempData["ErrorMessage"] = "Cannot delete the only remaining admin.";
                            return RedirectToPage();

                        }
                    }
                    string cmdText = "DELETE FROM SystemUser WHERE SystemUserID = @SystemUserID";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@SystemUserID", id);
                    cmd.ExecuteNonQuery();
                }

                // Refresh the user list after deletion
                PopulateUserList();

                TempData["SuccessMessage"] = "User deleted successfully.";
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
        public IActionResult OnPostChangeRole(int UserID, int NewRole)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                conn.Open();

                // Get current role of the user
                string getRoleQuery = "SELECT AccountTypeID FROM SystemUser WHERE SystemUserID = @UserID";
                SqlCommand getRoleCmd = new SqlCommand(getRoleQuery, conn);
                getRoleCmd.Parameters.AddWithValue("@UserID", UserID);
                int currentRole = (int)getRoleCmd.ExecuteScalar();

                // Prevent demoting the only admin
                if (currentRole == 1 && NewRole != 1)
                {
                    int adminCount = GetAdminCount(conn);
                    if (adminCount <= 1)
                    {
                        TempData["ErrorMessage"] = "Cannot demote the only remaining admin.";
                        return RedirectToPage();

                    }
                }

                // Update role
                string updateQuery = "UPDATE SystemUser SET AccountTypeID = @NewRole WHERE SystemUserID = @UserID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@NewRole", NewRole);
                updateCmd.Parameters.AddWithValue("@UserID", UserID);
                updateCmd.ExecuteNonQuery();
            }

            TempData["SuccessMessage"] = "User role updated successfully.";
            return RedirectToPage();
        }


        // get number of admins useful for ensuring we keep 1
        private int GetAdminCount(SqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM SystemUser WHERE AccountTypeID = 1";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }


    }
}
