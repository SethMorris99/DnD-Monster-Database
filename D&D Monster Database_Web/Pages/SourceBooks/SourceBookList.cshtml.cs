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
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/Account/AccessDenied");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    conn.Open();

                    //check if any monsters use this source book
                    string checkSql = "SELECT COUNT(*) FROM Monster WHERE SourceBookID = @SourceBookID";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@SourceBookID", id);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // cannot delete if any monsters use it
                            TempData["ErrorMessage"] = "Cannot delete this source book because it is still used by one or more monsters.";
                            return RedirectToPage("SourceBookList");
                        }
                    }

                    //Safe to delete
                    string deleteSql = "DELETE FROM SourceBook WHERE SourceBookID = @SourceBookID";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteSql, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@SourceBookID", id);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("SourceBookList");
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error deleting sourcebook: {ex.Message}");

                // Optionally, add a model error to display a message to the user
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the sourcebook.");
                return Page();
            }
        }
    }
}
