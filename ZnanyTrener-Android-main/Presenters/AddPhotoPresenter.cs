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
    public class AddPhotoPresenter : IAddPhotoPresenter
    {
        private readonly AddPhotoActivity _activity;
        private readonly IUserService _userService;

        public AddPhotoPresenter(AddPhotoActivity activity)
        {
            _activity = activity;
            _userService = new UserService();
        }

        public async Task AddPhotoAsync()
        {
            try
            {
                var file = await ViewsHelper.SelectPhotoAsync();
                var userId = SharedPreferencesManager.GetUser().Id;

                var request = new AddPhotoRequest
                {
                    File = file,
                    UserId = userId
                };

                var response = await _userService.PostPhotoAsync(request);

                if (response != null)
                {
                    Toast.MakeText(_activity, "Dodano zdjęcie.", ToastLength.Short).Show();
                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Long).Show();
            }


        }
    }
}