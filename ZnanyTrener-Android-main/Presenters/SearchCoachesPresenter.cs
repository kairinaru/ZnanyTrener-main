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
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public class SearchCoachesPresenter : ISearchCoachesPresenter
    {
        private readonly IUserService _userService;
        private readonly SearchCoachesActivity _activity;

        public SearchCoachesPresenter(SearchCoachesActivity activity)
        {
            _userService = new UserService();
            _activity = activity;
            Coaches = new List<UserDetailsResponse>();
        }

        public string KeyWord { get; set; }
        public List<UserDetailsResponse> Coaches { get; private set; }
        public List<string> CoachesUserNames
        {
            get
            {
                var userNames = new List<string>();

                foreach(var userName in Coaches)
                {
                    userNames.Add(userName.UserName);
                }

                return userNames;
            }
        }

        public async Task GetCoachesAsync()
        {
            if (KeyWord.Length < 3)
            {
                Coaches.Clear();
                return;
            };

            try
            {
                var response = await _userService.GetCoachesAsync(KeyWord);

                if (response != null)
                {
                    Coaches = JsonConvert.DeserializeObject<List<UserDetailsResponse>>(response);
                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }

        public void VisitCoachProfile(int position)
        {
            var intent = new Intent(_activity, typeof(CoachProfileFromSearchActivity));
            intent.PutExtra("coachId", Coaches[position].Id.ToString());
            _activity.StartActivity(intent);
        }
    }
}