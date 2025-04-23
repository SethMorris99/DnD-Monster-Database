using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.SourceBooks
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class EditSourceBookModel : PageModel
    {

        public SourceBook CurrentSourceBook { get; set; } = new SourceBook();
        public void OnGet(int id)
        {
            PopulateSourceBookDetails(id);
        }

        

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    conn.Open();
                    string sql = "UPDATE SourceBook SET Title = @Title, Edition = @Edition, YearPublished = @YearPublished, PageNumber = @PageNumber WHERE SourceBookID = @SourceBookID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", CurrentSourceBook.Title);
                        cmd.Parameters.AddWithValue("@Edition", CurrentSourceBook.Edition);
                        cmd.Parameters.AddWithValue("@YearPublished", CurrentSourceBook.YearPublished);
                        cmd.Parameters.AddWithValue("@PageNumber", CurrentSourceBook.PageNumber);
                        cmd.Parameters.AddWithValue("@SourceBookID", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToPage("SourceBookList");
            }
            else
            {
                return Page();
            }
        }

        private void PopulateSourceBookDetails(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    conn.Open();
                    string sql = "SELECT SourceBookID, Title, Edition, YearPublished, PageNumber FROM SourceBook WHERE SourceBookID = @SourceBookID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SourceBookID", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CurrentSourceBook.SourceBookID = Convert.ToInt32(reader["SourceBookID"]);
                                CurrentSourceBook.Title = reader["Title"].ToString();
                                CurrentSourceBook.Edition = reader["Edition"].ToString();
                                CurrentSourceBook.YearPublished = Convert.ToInt32(reader["YearPublished"]);
                                CurrentSourceBook.PageNumber = Convert.ToInt32(reader["PageNumber"]);
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
    }
}
