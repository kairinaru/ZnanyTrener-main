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

namespace ZnanyTrener_Android.Models.Exceptions
{
    public class ApiException : Exception
    {
        public string StatusCode;

        public ApiException(string message) : base(message)
        {

        }
    }
}