using System;
using System.Threading.Tasks;

namespace ZnanyTrener_Android.Presenters
{
    public interface IRegisterPresenter
    {
        DateTime CertificateGainDate { get; set; }
        string CertificateInstitution { get; set; }
        string CertificateNumber { get; set; }
        string City { get; set; }
        string Description { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string PhoneNumber { get; set; }
        string Role { get; set; }
        string Specialization { get; set; }
        string UserName { get; set; }
        string ConfirmPassword { get; set; }

        Task RegisterAsync();
    }
}