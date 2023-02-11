using System.ComponentModel.DataAnnotations;
using WorldCup.Data.Base;

namespace WorldCup.Models
{
    public class Highlights:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Title must not be empty")]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must not be empty")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL must not be empty")]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "Video URL must not be empty")]
        [Display(Name = "VideoUrl")]
        public string VideoUrl { get; set; }
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
