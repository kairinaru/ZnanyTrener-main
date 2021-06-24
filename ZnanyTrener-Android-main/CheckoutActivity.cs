using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Stripe.Android.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "CheckoutActivity")]
    public class CheckoutActivity : AppCompatActivity
    {
        private Button payButton;
        private IPaymentPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_checkout);
            _presenter = new PaymentPresenter(this);
            ConnectViews();
        }

        public CardInputWidget CardInputWidget { get; private set; }

        private void ConnectViews()
        {
            CardInputWidget = FindViewById<CardInputWidget>(Resource.Id.cardInputWidget);
            payButton = FindViewById<Button>(Resource.Id.payButton);
            payButton.Click += async (s, e) => { await _presenter.MakePaymentAsync(); };
        }
    }
}