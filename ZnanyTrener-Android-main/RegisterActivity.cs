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
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : AppCompatActivity
    {
        private RadioGroup radioGroup;
        private RadioButton selectedButton;
        private EditText userName;
        private EditText firstName;
        private EditText lastName;
        private EditText description;
        private EditText city;
        private EditText phone;
        private EditText email;
        private EditText password;
        private EditText confirmPassword;
        private EditText specialization;
        private EditText certificateNumber;
        private EditText certificateInstitution;
        private Button registerBtn;
        private IRegisterPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            ConnectViews();
            _presenter = new RegisterPresenter(this);
        }

        private void ConnectViews()
        {
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioRole);
            userName = FindViewById<EditText>(Resource.Id.userName);
            firstName = FindViewById<EditText>(Resource.Id.firstName);
            lastName = FindViewById<EditText>(Resource.Id.lastName);
            description = FindViewById<EditText>(Resource.Id.description);
            city = FindViewById<EditText>(Resource.Id.city);
            phone = FindViewById<EditText>(Resource.Id.phone);
            email = FindViewById<EditText>(Resource.Id.email);
            password = FindViewById<EditText>(Resource.Id.password);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirmPassword);
            specialization = FindViewById<EditText>(Resource.Id.specialization);
            certificateNumber = FindViewById<EditText>(Resource.Id.certificateNumber);
            certificateInstitution = FindViewById<EditText>(Resource.Id.certificateInstitution);
            registerBtn = FindViewById<Button>(Resource.Id.registerBtn);

            userName.TextChanged += UserName_TextChanged;
            firstName.TextChanged += FirstName_TextChanged;
            lastName.TextChanged += LastName_TextChanged;
            description.TextChanged += Description_TextChanged;
            city.TextChanged += City_TextChanged;
            phone.TextChanged += Phone_TextChanged;
            email.TextChanged += Email_TextChanged;
            password.TextChanged += Password_TextChanged;
            confirmPassword.TextChanged += ConfirmPassword_TextChanged;
            specialization.TextChanged += Specialization_TextChanged;
            certificateNumber.TextChanged += CertificateNumber_TextChanged;
            certificateInstitution.TextChanged += CertificateInstitution_TextChanged;
            registerBtn.Click += RegisterBtn_Click;


            radioGroup.CheckedChange += RadioGroup_CheckedChange;
        }

        private async void RegisterBtn_Click(object sender, EventArgs e)
        {
            await _presenter.RegisterAsync();
        }

        private void CertificateInstitution_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.CertificateInstitution = certificateInstitution.Text;
        }

        private void CertificateNumber_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.CertificateNumber = certificateNumber.Text;
        }

        private void Specialization_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.Specialization = specialization.Text;
        }

        private void ConfirmPassword_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.ConfirmPassword = confirmPassword.Text;
        }

        private void Password_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.Password = password.Text;
        }

        private void Email_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.Email = email.Text;
        }

        private void Phone_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.PhoneNumber = phone.Text;
        }

        private void City_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.City = city.Text;
        }

        private void Description_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.Description = description.Text;
        }

        private void LastName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.LastName = lastName.Text;
        }

        private void FirstName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.FirstName = firstName.Text;
        }

        private void UserName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _presenter.UserName = userName.Text;
        }

        private void RadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var selectedId = radioGroup.CheckedRadioButtonId;
            selectedButton = FindViewById<RadioButton>(selectedId);
            var selectedText = selectedButton.Text;
            
            if (selectedText.ToLower() == "trener")
            {
                _presenter.Role = "Coach";
                certificateInstitution.Visibility = ViewStates.Visible;
                specialization.Visibility = ViewStates.Visible;
                certificateNumber.Visibility = ViewStates.Visible;
            }
            else
            {
                _presenter.Role = "User";
                certificateInstitution.Visibility = ViewStates.Gone;
                specialization.Visibility = ViewStates.Gone;
                certificateNumber.Visibility = ViewStates.Gone;
            }
        }
    }
}