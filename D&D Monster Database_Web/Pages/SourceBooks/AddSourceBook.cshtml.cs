using Microsoft.Data.SqlClient;
using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.SourceBooks
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class AddSourceBookModel : PageModel
    {
        public SourceBook NewSourceBook { get; set; } = new SourceBook();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {

                    using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                    {
                        string cmdText = "INSERT INTO SourceBook (Title, Edition, YearPublished, PageNumber) VALUES (@Title, @Edition, @YearPublished, @PageNumber);SELECT SCOPE_IDENTITY();";
                        SqlCommand cmd = new SqlCommand(cmdText, conn);
                        conn.Open();
                        cmd.Parameters.AddWithValue("@Title", NewSourceBook.Title);
                        cmd.Parameters.AddWithValue("@Edition", NewSourceBook.Edition);
                        cmd.Parameters.AddWithValue("@YearPublished", NewSourceBook.YearPublished);
                        cmd.Parameters.AddWithValue("@PageNumber", NewSourceBook.PageNumber);
                        cmd.ExecuteNonQuery();
                    }

                    return RedirectToPage("SourceBookList");
                }
                catch
                {
                    throw;
                }
                
            }
            return Page();
        }
    }
}
