using System.ComponentModel.DataAnnotations;

namespace Epsic.Authx.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}