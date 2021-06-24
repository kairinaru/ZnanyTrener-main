using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public class CoachProfileFromSearchPresenter : BaseProfilePresenter, ICoachProfileFromSearchPresenter
    {
        private readonly int coachId;
        private IUserService _userService;

        public CoachProfileFromSearchPresenter(AppCompatActivity activity) : base(activity)
        {
            _userService = new UserService();
            var coachIdString = _activity.Intent?.GetStringExtra("coachId")!;
            coachId = string.IsNullOrEmpty(coachIdString) ? 0 : int.Parse(coachIdString);
        }

        public UserDetailsResponse UserFromApi { get; private set; }

        public async Task GetUserAsync()
        {
            try
            {
                var response = await _userService.GetUserAsync(coachId);

                if (response != null)
                {
                    // Zapisuje zalogowanego użytkownika do pamięci aplikacji
                    UserFromApi = JsonConvert.DeserializeObject<UserDetailsResponse>(response);
                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }
    }
}