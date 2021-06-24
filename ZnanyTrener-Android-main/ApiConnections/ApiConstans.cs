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

namespace ZnanyTrener_Android.ApiConnections
{
    public static class ApiConstans
    {
        public const string BaseAddress = "http://192.168.0.108:5000/api/";
        public const string UserEndpoint = "user";
        public const string TrainingEndpoint = "training";
        public const string AuthEndpoint = "auth";
        public const string PaymentEndpoint = "payment";
        public const string CertificateEndpoint = "certificate";
        public const string MediaType = "application/json";
    }
}