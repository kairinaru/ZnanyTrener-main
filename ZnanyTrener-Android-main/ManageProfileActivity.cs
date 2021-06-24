using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Helpers;

namespace ZnanyTrener_Android
{
    [Activity(Label = "ManageProfileActivity")]
    public class ManageProfileActivity : AppCompatActivity
    {
        private Button addCertificate;
        private Button editProfile;
        private Button logOut;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_manage_profile);
            ConnectViews();
        }

        private void ConnectViews()
        {
            addCertificate = FindViewById<Button>(Resource.Id.addCertificate);
            editProfile = FindViewById<Button>(Resource.Id.editProfile);
            logOut = FindViewById<Button>(Resource.Id.logOut);

            if (!SharedPreferencesManager.IsCoach()) addCertificate.Visibility = ViewStates.Gone;

            addCertificate.Click += (s, e) => { StartActivity(typeof(AddCertificateActivity)); };
            logOut.Click += (s, e) => 
            {
                SharedPreferencesManager.ClearPreferences();

                Toast.MakeText(this, "Wylogowano.", ToastLength.Long).Show();
                FinishAffinity();
                StartActivity(typeof(LoginActivity));
            };
            editProfile.Click += (s, e) => { StartActivity(typeof(AddPhotoActivity)); };
        }
    }
}