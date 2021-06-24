using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "AddCertificateActivity")]
    public class AddCertificateActivity : AppCompatActivity
    {
        private EditText certificateNumber;
        private EditText certificateInstitution;
        private Button add;
        private IAddCertificatePresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_certificate);
            _presenter = new AddCertificatePresenter(this);
            ConnectViews();       
        }

        private void ConnectViews()
        {
            certificateNumber = FindViewById<EditText>(Resource.Id.certificateNumber);
            certificateInstitution = FindViewById<EditText>(Resource.Id.certificateInstitution);
            add = FindViewById<Button>(Resource.Id.add);

            certificateNumber.TextChanged += (s, e) => { _presenter.Number = certificateNumber.Text; };
            certificateInstitution.TextChanged += (s, e) => { _presenter.Institution = certificateInstitution.Text; };            
            add.Click += async (s, e) => { await _presenter.AddCertificateAsync(); };
        }
    }
}