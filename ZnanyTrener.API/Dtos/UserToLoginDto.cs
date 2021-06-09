using System.ComponentModel.DataAnnotations;

namespace ZnanyTrener.API.Dtos
{
    public class UserToLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}