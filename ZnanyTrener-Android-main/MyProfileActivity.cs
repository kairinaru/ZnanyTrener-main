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
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "MyProfileActivity")]
    public class MyProfileActivity : AppCompatActivity
    {
        private ImageView avatarImg;
        private TextView firstAndLast;
        private TextView city;
        private TextView specialization;
        private TextView description;
        private TextView showCertificates;
        private TextView certificate;
        private TextView phone;
        private TextView email;
        private Button plannedBtn;
        private Button manageProfile;
        private Button search;
        
        private BaseProfilePresenter _presenter;
        private bool areCertificatesVisible = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_my_profile);
            ConnectViews();
            _presenter =new BaseProfilePresenter(this);
            AssignViews();
            // Create your application here
        }

        private void ConnectViews()
        {
            avatarImg = FindViewById<ImageView>(Resource.Id.avatarImg);
            firstAndLast = FindViewById<TextView>(Resource.Id.firstAndLast);
            city = FindViewById<TextView>(Resource.Id.city);
            specialization = FindViewById<TextView>(Resource.Id.specialization);
            description = FindViewById<TextView>(Resource.Id.description);
            showCertificates = FindViewById<TextView>(Resource.Id.showCertificates);
            certificate = FindViewById<TextView>(Resource.Id.certificate);
            phone = FindViewById<TextView>(Resource.Id.phone);
            email = FindViewById<TextView>(Resource.Id.email);
            plannedBtn = FindViewById<Button>(Resource.Id.plannedBtn);
            manageProfile = FindViewById<Button>(Resource.Id.manageProfile);
            search = FindViewById<Button>(Resource.Id.search);
            

            showCertificates.Click += (s, e) =>
            {
                areCertificatesVisible = !areCertificatesVisible;

                if (areCertificatesVisible)
                {
                    certificate.Visibility = ViewStates.Visible;
                    showCertificates.Text = "Ukryj certyfikaty";
                }
                else
                {
                    certificate.Visibility = ViewStates.Invisible;
                    showCertificates.Text = "Pokaż certyfikaty";
                }
            };

            manageProfile.Click += (s, e) => { StartActivity(typeof(ManageProfileActivity)); };
            plannedBtn.Click += (s, e) => 
            {
                var intent = new Intent(this, typeof(MyScheduleActivity));

                if (_presenter.IsCoach)
                {
                    intent.PutExtra("for", "me-coach");
                }
                else
                {
                    intent.PutExtra("for", "me-user");
                }

                StartActivity(intent);
            };

            
            search.Click += (s, e) => { StartActivity(typeof(SearchCoachesActivity)); };
        }

        private void AssignViews()
        {
            var user = _presenter.UserFromStorage;
            ViewsHelper.GetImageFromUrl(user.PhotoUrl, avatarImg);
            firstAndLast.Text = $"{user.FirstName} {user.LastName}";
            city.Text = $"Miejscowość: {user.City}";
            description.Text = $"O mnie: {user.Description}";
            phone.Text = $"Numer telefonu: {user.PhoneNumber}";
            email.Text = $"Adres e-mail: {user.Email}";

            if(_presenter.IsCoach)
            {
                specialization.Text = $"Specjalizacja: {user.Specialization}";
                certificate.Text = _presenter.GetCertificatesInOneString();
                search.Visibility = ViewStates.Gone;
            }
            else
            {
                specialization.Visibility = ViewStates.Gone;
                showCertificates.Visibility = ViewStates.Gone;
                certificate.Visibility = ViewStates.Gone;
                plannedBtn.Text = "Moje treningi";
            }

        }
    }
}