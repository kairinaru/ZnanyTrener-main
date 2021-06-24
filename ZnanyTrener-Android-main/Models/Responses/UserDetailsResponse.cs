using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.Models.Responses
{
    public class UserDetailsResponse
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
        public ICollection<CertificateRequest> Certificates { get; set; }
    }
}