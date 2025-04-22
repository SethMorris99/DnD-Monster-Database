using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class SourceBook
    {
        public int SourceBookId { get; set; }
        [Required(ErrorMessage = "Please enter the name of the source book")]
        [StringLength(255, ErrorMessage = "The name of the source book cannot exceed 255 characters")]
        [Display(Name = "Source Book Name")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Please enter the edition")]
        [StringLength(50, ErrorMessage = "Editon cannot exceed 50 characters")]
        [Display(Name = "Edition")]
        public string Edition { get; set; }

        [Required (ErrorMessage = "Please enter the year published")]
        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }

        [Required (ErrorMessage = "Please enter the page count")]
        [Display(Name = "Page Count")]
        public int PageNumber { get; set; }
    }
}
