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
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "SplashScreenActivity", Theme = "@style/GuestTheme", MainLauncher = true)]
    public class SplashScreenActivity : Activity
    {
        private ISplashScreenPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _presenter = new SplashScreenPresenter(this);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actvity_splash_screen);
            _presenter.RedirectToActivity();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _presenter = new SplashScreenPresenter(this);
            _presenter.RedirectToActivity();
        }
    }
}