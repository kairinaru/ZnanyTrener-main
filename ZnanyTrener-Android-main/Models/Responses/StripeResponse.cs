using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZnanyTrener_Android.Models.Responses
{
    public class StripeResponse
    {
        public string Id { get; set; }

        [JsonProperty("payment_intent")]
        public string PaymentIntent { get; set; }
    }
}