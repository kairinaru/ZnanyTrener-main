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
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public class BaseProfilePresenter
    {
        public UserDetailsResponse UserFromStorage { get; protected set; }     
        public bool IsCoach { get; protected set; }
        protected readonly AppCompatActivity _activity;

        public BaseProfilePresenter(AppCompatActivity activity)
        {
            _activity = activity;
            UserFromStorage = SharedPreferencesManager.GetUser();
            IsCoach = UserFromStorage.Role == "Coach";
        }

        public virtual string GetCertificatesInOneString()
        {
            if (!IsCoach) return string.Empty;

            var builder = new StringBuilder();
            int index = 1;

            foreach (var cert in UserFromStorage.Certificates)
            {
                builder.Append($"{index}. {cert.Number} Instytucja: {cert.Institution} (od: {GetDate(cert.GainDate)})\n");
                index++;
            }

            return builder.ToString();
        }

        private string GetDate(DateTime date)
        {
            var dateText = $"{date.Day}-{date.Month}-{date.Year}";

            return dateText;
        }


    }
}