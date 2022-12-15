using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z].*", ErrorMessage ="Start with big letter")]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required]
        [Url]
        public string ThumbnailUrl { get; set; }
        
        [Url]
        public string? SecondaryImageUrl { get; set; }
        [Required]
        
        public DateTime Date { get; set; }
    }
}
