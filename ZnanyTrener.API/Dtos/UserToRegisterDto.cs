using System;
using System.ComponentModel.DataAnnotations;

namespace ZnanyTrener.API.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

        //Certificate section
        public string CertificateNumber { get; set; }
        public string CertificateInstitution { get; set; }
        public DateTime CertificateGainDate { get; set; }
    }
}