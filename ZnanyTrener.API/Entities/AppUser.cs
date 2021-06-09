using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ZnanyTrener.API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Specialization { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoCloudinaryPublicId { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Certificate> Certifiactes { get; set; }
        public ICollection<Training> TrainingsForCoach { get; set; }
        public ICollection<Training> TrainingsForUser { get; set; }
    }
}