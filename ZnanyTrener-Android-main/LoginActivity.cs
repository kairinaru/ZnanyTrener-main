using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net.Http;
using ZnanyTrener_Android.ApiConnections;
using System.Text;
using Newtonsoft.Json;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "Zaloguj się", Theme = "@style/AppTheme")]
    public class LoginActivity : AppCompatActivity
    {
        private Button loginBtn;
        private Button goToRegisterBtn;
        private EditText userNameEditText;
        private EditText passwordEditText;
        private ILoginPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_login);
            ConnectViews();
            _presenter = new LoginPresenter(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ConnectViews()
        {
            loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            goToRegisterBtn = FindViewById<Button>(Resource.Id.goToRegisterBtn);
            userNameEditText = FindViewById<EditText>(Resource.Id.userNameEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);

            userNameEditText.TextChanged += UserNameEditText_TextChanged;
            passwordEditText.TextChanged += PasswordEditText_TextChanged;
            loginBtn.Click += LoginBtn_Click;
            goToRegisterBtn.Click += (s, e) => { StartActivity(typeof(RegisterActivity)); };
        }

        private void PasswordEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.Password = passwordEditText.Text;
        }

        private void UserNameEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.UserName = userNameEditText.Text;
        }

        private async void LoginBtn_Click(object sender, System.EventArgs e)
        {
            await _presenter.LoginUserAsync();
        }
    }
}