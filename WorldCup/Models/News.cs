using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldCup.Data.Base;

namespace WorldCup.Models
{
    public class News : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z].*", ErrorMessage ="The title cannot be empty and you should start with capital letter")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Description cannot be empty or more than 5000 characters")]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Thumbnail Url cannot be empty")]

        public string ThumbnailUrl { get; set; }
        
        
        public string? SecondaryImageUrl { get; set; }

        public string? VideoUrl { get; set; }


        public DateTime CreatedDate { get; set; }
        [Display(Name ="Select an Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]

        public Author Author { get; set; }
        
    }
}
