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
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Helpers
{
    public static class SharedPreferencesManager
    {
        private static readonly ISharedPreferences _preferences = Application
           .Context
           .GetSharedPreferences("userinfo", FileCreationMode.Private);

        private static ISharedPreferencesEditor _editor;

        public static void SaveUser(UserDetailsResponse userObject)
        {
            var serialized = JsonConvert.SerializeObject(userObject);
            _editor = _preferences.Edit();
            _editor.PutString("user", serialized);
            _editor.Apply();
        }

        public static void SaveUser(string serialized)
        {
            _editor = _preferences.Edit();
            _editor.PutString("user", serialized);
            _editor.Apply();
        }

        public static UserDetailsResponse GetUser()
        {
            var user = JsonConvert
                .DeserializeObject<UserDetailsResponse>(_preferences.GetString("user", ""));

            return user;
        }

        public static string GetToken()
        {
            var user = GetUser();

            return user.Token;
        }

        public static bool IsCoach()
        {
            var user = GetUser();

            return user.Role == "Coach";
        }

        public static bool IsMe(int userId)
        {
            var user = GetUser();

            return user.Id == userId;
        }

        public static void ClearPreferences()
        {
            _editor = _preferences.Edit();
            _editor.Remove("user");
            _editor.Clear();
            _editor.Apply();
        }
    }
}