using System.ComponentModel.DataAnnotations;

namespace WorldCup.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required]  
        public DateTime Date { get; set; }
    }
}
