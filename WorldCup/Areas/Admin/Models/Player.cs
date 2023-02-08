using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldCup.Data.Enums;

namespace WorldCup.Areas.Admin.Models
{
    public class Player
    {
        [Key]

        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 chars")]

        public string FullName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 50 chars")]

        public string Surname { get; set; }
        public DateTime Birthday { get; set; }



        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }


        public State State { get; set; }


        public int ClubId { get; set; }
        [Display(Name = "Select a Club")]
        [ForeignKey("ClubId")]
        public Club Club { get; set; }

    }
}
