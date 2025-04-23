using Microsoft.Data.SqlClient;
using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterDB_Business;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.SourceBooks
{
    [Authorize]
    public class SourceBookListModel : PageModel
    {
        public List<SourceBook> SourceBookList { get; set; } = new List<SourceBook>();
        public void OnGet()
        {
            PopulateSourceBookList();
        }

        private void PopulateSourceBookList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    conn.Open();
                    string sql = "SELECT SourceBookID, Title, Edition, YearPublished, PageNumber FROM SourceBook ORDER BY Title";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SourceBook sourceBook = new SourceBook
                                {
                                    SourceBookID = Convert.ToInt32(reader["SourceBookID"]),
                                    Title = reader["Title"].ToString(),
                                    Edition = reader["Edition"].ToString(),
                                    YearPublished = Convert.ToInt32(reader["YearPublished"]),
                                    PageNumber = Convert.ToInt32(reader["PageNumber"])
                                };
                                SourceBookList.Add(sourceBook);
                            }
                        }
                    }
                }
            }
            catch 
            {
                // Handle exception (e.g., log it)
                throw;
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
                    string sql = "DELETE FROM SourceBook WHERE SourceBookID = @SourceBookID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SourceBookID", id);
                        cmd.ExecuteNonQuery();

                    }
                }
                return RedirectToPage("SourceBookList");
            }
            catch 
            {
                throw;
            }

            // If we reach here, something went wrong
            return Page();
        }
    }
}
