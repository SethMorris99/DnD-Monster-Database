using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonsterDB_Business;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.SourceBook
{
    public class AddSourceBookModel : PageModel
    {
        public SourceBook NewSourceBook { get; set; } = new SourceBook();
        public void OnGet()
        {
        }
    }
}
