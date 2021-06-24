using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Stripe.Android;
using Com.Stripe.Android.Model;
using Com.Stripe.Android.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Models.Responses;
using static Com.Stripe.Android.Model.PaymentMethod;

namespace ZnanyTrener_Android.Presenters
{
    public class PaymentPresenter : IPaymentPresenter
    {
        private readonly CheckoutActivity _activity;
        private IPaymentService _paymentService;
        private ITrainingService _trainingService;
        private string secret;
        private TrainingToAddRequest request;


        public PaymentPresenter(CheckoutActivity activity)
        {
            PaymentConfiguration.Init("pk_test_51I5YvcJvKXNzTQcriYlZbz2AY6gfE97xSpJ7mUaL2UWOAABOjPBN9mlS3421iRf2nL25anPw6bgSSP5mxg7yI1Z600SJy8Gsm7");
            _paymentService = new PaymentService();
            _trainingService = new TrainingService();
            _activity = activity;
            request = JsonConvert.DeserializeObject<TrainingToAddRequest>(_activity.Intent.GetStringExtra("trainingRequest"));
        }

        public Stripe Stripe { get; private set; }

        public async Task MakePaymentAsync()
        {
            try
            {               
                var response = await _paymentService.GetClientSecretAsync();

                if (response != null)
                {
                    var stripeResponse = JsonConvert.DeserializeObject<PaymentIntentResponse>(response);
                    secret = stripeResponse.ClientSecret;

                    var card = CreatePaymentMethodCard();
                    var payIntentParams = CreatePaymentIntentParams(card);

                    await Pay(payIntentParams);

                    await AddTraining();
                }

            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }
        }

        private async Task AddTraining()
        {
            try
            {
                var response = await _trainingService.AddTrainingAsync(request);

                _activity.FinishAffinity();
                _activity.StartActivity(typeof(MyProfileActivity));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task Pay(PaymentIntentParams payIntentParams)
        {
            Stripe = new Stripe(_activity, PaymentConfiguration.Instance.PublishableKey);

            var intent = await Task.Run(() => { return Stripe.ConfirmPaymentIntentSynchronous(payIntentParams, PaymentConfiguration.Instance.PublishableKey); });
        }

        private PaymentIntentParams CreatePaymentIntentParams(PaymentMethodCreateParams.Card card)
        {
            PaymentMethodCreateParams payParams = PaymentMethodCreateParams.Create(card, null);

            PaymentIntentParams payIntentParams = PaymentIntentParams.CreateConfirmPaymentIntentWithPaymentMethodCreateParams(payParams, secret, null);
            return payIntentParams;
        }

        private PaymentMethodCreateParams.Card CreatePaymentMethodCard()
        {
            var cardInputWidget = _activity.CardInputWidget;

            Com.Stripe.Android.Model.Card card = cardInputWidget.Card;

            PaymentMethodCreateParams.Card cardMeth = card.ToPaymentMethodParamsCard();
            return cardMeth;
        }
    }
}