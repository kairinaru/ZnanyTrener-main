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
using ZnanyTrener_Android.Helpers;

namespace ZnanyTrener_Android.Presenters
{
    public class SplashScreenPresenter : ISplashScreenPresenter
    {
        private readonly SplashScreenActivity _activity;

        public SplashScreenPresenter(SplashScreenActivity activity)
        {
            _activity = activity;
        }

        public void RedirectToActivity()
        {
            _activity.FinishAffinity();

            if (IsLoggedIn())
            {
                _activity.StartActivity(typeof(MyProfileActivity));

                return;
            }          
            _activity.StartActivity(typeof(LoginActivity));
        }

        private bool IsLoggedIn()
        {
            return SharedPreferencesManager.GetUser() != null;
        }
    }
}