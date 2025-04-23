

using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class SourceBook
    {
        public int SourceBookID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Edition { get; set; }
        [Required]
        [Range(1970,2025, ErrorMessage = "Enter a valid year between 1970 and the current year")]
        public int YearPublished { get; set; }
        public int PageNumber { get; set; }

    }
}
