using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D_D_Monster_Database_Web.Pages.SourceBooks
{
    public class AddSourceBookModel : PageModel
    {
        public SourceBook NewSourceBook { get; set; } = new SourceBook();
        public void OnGet()
        {
        }
    }
}
