using System.Collections.Generic;

namespace ZnanyTrener.API.Dtos
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoCloudinaryPublicId { get; set; }
        public ICollection<CertificateToAddDto> Certificates { get; set; }
    }
}