using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                // Check if the user is in the database
                // If the user is in the database, redirect to the home page
                // If the user is not in the database, redirect to the sign in page
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    string cmdText = "SELECT SystemUserID, AccountTypeID, UserPassword, UserDisplayName FROM [SystemUser] WHERE UserDisplayName = @UserDisplayName";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@UserDisplayName", Login.Username);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string passwordHash = reader.GetString(2);

                        // check if the password is valid
                        if (AppHelper.VerifyPassword(Login.Password, passwordHash))
                        {
                            // create a name identifier claim for SystemUserID
                            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, reader.GetInt32(0).ToString());

                            // create a name claim for UserDisplayName
                            var nameClaim = new Claim(ClaimTypes.Name, reader.GetString(2));

                            // build the claim list
                            var claims = new List<Claim> { userIdClaim, nameClaim };

                            int accountType = reader.GetInt32(1);

                            // Add role claim based on AccountType
                            if (accountType == 1)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                            }
                            else
                            {
                                claims.Add(new Claim(ClaimTypes.Role, "User"));
                            }

                            // create identity and principal for authentication
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            // sign in the user with cookie auth
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                            // update user login time
                            UpdateUserLoginTime(reader.GetInt32(0));

                            // redirect to profile page after successful login
                            return RedirectToPage("/Account/Profile");
                        }
                        else
                        {
                            // password did not match
                            ModelState.AddModelError("LoginError", "Invalid credentials.");
                            return Page();
                        }
                    }
                    else
                    {
                        // user not found
                        ModelState.AddModelError("LoginError", "Invalid credentials.");
                        return Page();
                    }
                }
            }
            else
            {
                return Page();
            }
        }
        private void UpdateUserLoginTime(int userId)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = "UPDATE SystemUser SET LastLoginTime = @loginTime WHERE SystemUserID = @userId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@loginTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
