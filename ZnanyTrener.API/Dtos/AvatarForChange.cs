using Microsoft.AspNetCore.Http;

namespace ZnanyTrener.API.Dtos
{
    public class AvatarForChange
    {
        public int UserId { get; set; } 
        public IFormFile File { get; set; }
    }
}