using System.Data.SqlClient;
using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.SourceBooks
{
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
                    using(SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                    {
                        conn.Open();
                        string sql = "INSERT INTO SourceBook (SourceBookID, Title, Edition, YearPublished, PageNumber) VALUES (@SourceBookID, @Title, @Edition, @YearPublished, @PageNumber)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Title", NewSourceBook.Title);
                            cmd.Parameters.AddWithValue("@Edition", NewSourceBook.Edition);
                            cmd.Parameters.AddWithValue("@YearPublished", NewSourceBook.YearPublished);
                            cmd.Parameters.AddWithValue("@PageNumber", NewSourceBook.PageNumber);
                        }    
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
