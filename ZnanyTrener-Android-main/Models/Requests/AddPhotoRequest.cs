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

namespace ZnanyTrener_Android.Models.Requests
{
    public class AddPhotoRequest
    {
        public byte[] File { get; set; }
        public int UserId { get; set; }
    }
}