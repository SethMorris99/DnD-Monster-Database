

using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class SourceBook
    {
        public int SourceBookID { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
        public string Title { get; set; }
        public string Edition { get; set; }
        public int YearPublished { get; set; }
        public int PageNumber { get; set; }

    }
}
