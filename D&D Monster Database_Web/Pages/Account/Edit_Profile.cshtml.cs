using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.Account
{
    [Authorize]
    public class Edit_ProfileModel : PageModel
    {
        [BindProperty]
        public Profile UserProfile { get; set; } = new Profile();

        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            PopulateUserProfile(userId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return Page(); // Stay on Edit_Profile if errors
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"
                UPDATE SystemUser
                SET 
                    UserFirstName = @FirstName,
                    UserLastName = @LastName,
                    UserDisplayName = @Username,
                    UserEmail = @Email,
                    ProfileImageURL = @ProfileImageURL
                WHERE SystemUserID = @UserId";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@FirstName", UserProfile.FirstName);
                cmd.Parameters.AddWithValue("@LastName", UserProfile.LastName);
                cmd.Parameters.AddWithValue("@Username", UserProfile.Username);
                cmd.Parameters.AddWithValue("@Email", UserProfile.Email);
                cmd.Parameters.AddWithValue("@ProfileImageURL", UserProfile.ProfileImageURL ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("/Account/Profile"); // Only after successful save
        }


        private void PopulateUserProfile(int userId)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"
                    SELECT 
                    UserFirstName, 
                    UserLastName,
                    UserDisplayName,
                    UserEmail,
                    ProfileImageURL
                    FROM SystemUser
                    WHERE SystemUserID = @userId";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserProfile.FirstName = reader.GetString(0);
                    UserProfile.LastName = reader.GetString(1);
                    UserProfile.Username = reader.GetString(2);
                    UserProfile.Email = reader.GetString(3);
                    UserProfile.ProfileImageURL = reader.IsDBNull(4) ? "" : reader.GetString(4);
                }

                reader.Close();
            }
        }
    }
}
