using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Stripe.Android;
using Newtonsoft.Json;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    public class PaymentFragment : Android.Support.V4.App.DialogFragment
    {
        private readonly TrainingToAddRequest _training;
        private readonly bool _canAddTraining;
        private TextView start;
        private TextView end;
        private Button payBtn;
        

        public PaymentFragment(TrainingToAddRequest training, bool canAddTraining)
        {
            _training = training;
            _canAddTraining = canAddTraining;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view =  inflater.Inflate(Resource.Layout.payment_fragment, container, false);
            ConnectViews(view);
            return view;
        }

        private void ConnectViews(View view)
        {
            payBtn = view.FindViewById<Button>(Resource.Id.payBtn);
            start = view.FindViewById<TextView>(Resource.Id.start);
            end = view.FindViewById<TextView>(Resource.Id.end);
            start.Text = $"Rozpoczyna się: {_training.StartDate}";
            end.Text = $"Rozpoczyna się: {_training.EndDate}";

            if (!_canAddTraining)
            {
                payBtn.Visibility = ViewStates.Gone;
                return;
            }

            payBtn.Click += (s, e) => 
            {
                Dismiss();
                var intent = new Intent(Activity, typeof(CheckoutActivity));
                intent.PutExtra("trainingRequest", JsonConvert.SerializeObject(_training));
                Activity.StartActivity(intent);
            };
        }
    }
}