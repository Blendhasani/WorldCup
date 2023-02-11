using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using WorldCup.Data.Base;

namespace WorldCup.Models
{
    public class Stadium : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Name of the stadium cannot be empty and you should start with capital letter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Capacity cannot be empty")]
        public int Capacity { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Location of the stadium cannot be empty and you should start with capital letter")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Image Url cannot be empty")]
        public string ImageURL { get; set; }
    }
}
