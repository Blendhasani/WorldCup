using System.ComponentModel.DataAnnotations;

namespace WorldCup.Areas.User.Controllers
{
    public class ContactViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}