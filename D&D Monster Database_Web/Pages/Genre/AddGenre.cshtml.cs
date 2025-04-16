using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Genre
{
    public class AddGenreModel : PageModel
    {
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
                }
                catch
                {
                    throw;
                }
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }
}
