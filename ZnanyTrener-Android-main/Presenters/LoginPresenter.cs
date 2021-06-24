using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public class LoginPresenter : ILoginPresenter
    {
        private readonly LoginActivity _activity;
        private readonly IAuthService _authService;

        public LoginPresenter(LoginActivity activity)
        {
            _activity = activity;
            _authService = new AuthService();
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task LoginUserAsync()
        {
            try
            {
                var request = new UserToLoginRequest
                {
                    UserName = UserName,
                    Password = Password
                };

                var response = await _authService.LoginAsync(request);

                if (response != null)
                {
                    // Zapisuje zalogowanego użytkownika do pamięci aplikacji

                    SharedPreferencesManager.SaveUser(response);
                    _activity.StartActivity(typeof(MyProfileActivity));
                    _activity.FinishAffinity();
                }

            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }


    }
}