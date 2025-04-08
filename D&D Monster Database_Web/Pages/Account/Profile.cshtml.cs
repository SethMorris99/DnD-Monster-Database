using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Profile UserProfile { get; set; } = new Profile();

        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            PopulateUserProfile(userId);
        }

        private void PopulateUserProfile(int userId)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                // using the @ so that the formatting would be easier to read
                string cmdText = @"
                    SELECT 
                    UserFirstName, 
                    UserLastName,
                    UserDisplayName,
                    UserEmail,
                    ProfileImageURL, 
                    AccountType.AccountTypeName, 
                    LastLoginTime
                    FROM SystemUser
                    INNER JOIN AccountType ON SystemUser.AccountTypeID = AccountType.AccountTypeID
                    WHERE SystemUserID = @userId";



                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserProfile.FirstName = reader.GetString(0);        // UserFirstName
                    UserProfile.LastName = reader.GetString(1);         // UserLastName
                    UserProfile.Username = reader.GetString(2);         // UserDisplayName
                    UserProfile.Email = reader.GetString(3);            // UserEmail
                    UserProfile.ProfileImageURL = reader.GetString(4);  // ProfileImageURLs
                    UserProfile.AccountType = reader.GetString(5);      // AccountTypeName
                    UserProfile.LastLoginTime = reader.GetDateTime(6);  // LastLoginTime
                }

                reader.Close();
            }
        }
    }
}
