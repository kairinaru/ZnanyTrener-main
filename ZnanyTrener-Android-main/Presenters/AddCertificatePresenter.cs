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
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.Presenters
{
    public class AddCertificatePresenter : IAddCertificatePresenter
    {
        private readonly AddCertificateActivity _activity;
        private readonly ICertificateService _certificateService;
        private readonly IUserService _userService;

        public AddCertificatePresenter(AddCertificateActivity activity)
        {
            _activity = activity;
            _certificateService = new CertificateService();
            _userService = new UserService();
        }

        public string Institution { get; set; }
        public string Number { get; set; }

        public async Task AddCertificateAsync()
        {
            try
            {
                CheckIfEmpty();

                var userId = SharedPreferencesManager.GetUser().Id;

                var request = new CertificateRequest
                {
                    UserId = userId,
                    GainDate = DateTime.UtcNow,
                    Institution = Institution,
                    Number = Number
                };

                var response = await _certificateService.AddCertificate(request);

                if (response != null)
                {
                    // Po pomyślnym utworzeniu konta przenieś do ekranu logowania
                    Toast.MakeText(_activity, "Pomyślnie dodano certyfikat.", ToastLength.Short).Show();

                    await GetUpdatedUserAndSaveToSharedPreferencesAsync(userId);

                    _activity.FinishAffinity();
                    _activity.StartActivity(typeof(MyProfileActivity));

                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }

        private async Task GetUpdatedUserAndSaveToSharedPreferencesAsync(int userId)
        {
            try
            {
                var response = await _userService.GetUserAsync(userId);

                if (response != null)
                {
                    SharedPreferencesManager.SaveUser(response);
                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }

        private void CheckIfEmpty()
        {
            if (string.IsNullOrEmpty(Institution) || string.IsNullOrEmpty(Number))
                throw new Exception("Pola nie mogą być puste.");
        }
    }
}