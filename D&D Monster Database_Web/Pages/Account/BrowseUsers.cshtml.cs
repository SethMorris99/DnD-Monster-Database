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
                UserFirstName, 
                UserLastName, 
                UserDisplayName, 
                UserEmail, 
                UserPassword, 
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
                        FirstName = reader.GetString(0),        // UserFirstName
                        LastName = reader.GetString(1),         // UserLastName
                        Username = reader.GetString(2),         // UserDisplayName
                        Email = reader.GetString(3),            // UserEmail
                        Password = reader.GetString(4),         // UserPassword
                        ProfileImageURL = reader.GetString(5),  // ProfileImageURL
                        AccountType = reader.GetString(6),      // AccountTypeName
                        LastLoginTime = reader.GetDateTime(7)   // LastLoginTime
                    });
                }

                reader.Close();
            }
        }
    }
}
