using System.ComponentModel.DataAnnotations;

namespace ReadLater5.Models
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
