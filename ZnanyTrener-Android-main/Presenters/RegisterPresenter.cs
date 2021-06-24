using Android.Widget;
using System;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.Presenters
{
    public class RegisterPresenter : IRegisterPresenter
    {
        private readonly IAuthService _authService;
        private readonly RegisterActivity _activity;

        public RegisterPresenter(RegisterActivity activity)
        {
            Role = "User";
            _activity = activity;
            _authService = new AuthService();
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } 
        public string CertificateNumber { get; set; }
        public string CertificateInstitution { get; set; }
        public DateTime CertificateGainDate { get; set; }

        public async Task RegisterAsync()
        {
            
            try
            {
                CheckIfPasswordsMatch();

                var request = new UserToReqisterRequest
                {
                    UserName = UserName,
                    FirstName = FirstName,
                    LastName = LastName,
                    Description = Description,
                    Specialization = Specialization,
                    City = City,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Password = Password,
                    Role = Role,
                    CertificateNumber = CertificateNumber,
                    CertificateInstitution = CertificateInstitution,
                    CertificateGainDate = DateTime.UtcNow.AddYears(-2)
                };

                var response = await _authService.RegisterAsync(request);

                if (response != null)
                {
                    // Po pomyślnym utworzeniu konta przenieś do ekranu logowania
                    Toast.MakeText(_activity, "Dziękujemy za rejestrację.", ToastLength.Short).Show();
                    _activity.StartActivity(typeof(LoginActivity));
                    _activity.Finish();
                }

            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }

        private void CheckIfPasswordsMatch()
        {
            if (Password != ConfirmPassword)
            {
                throw new Exception("Hasla nie sa takie same.");
            }
        }
    }
}