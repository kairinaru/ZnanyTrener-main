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
using System.Net.Http;
using System.Text;
using ZnanyTrener_Android.ApiConnections;
using ZnanyTrener_Android.Models.Exceptions;

namespace ZnanyTrener_Android.Helpers
{
    public static class ApiHelper
    {
        public static StringContent CreateJSONStringContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);

            var data = new StringContent(json, Encoding.UTF8, ApiConstans.MediaType);

            return data;
        }

        public static string GetResponseContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                return result;
            }

            throw new ApiException(response.StatusCode.ToString());
        }

        public static void AddAuthorizationHeader(HttpClient client)
        {
            var token = SharedPreferencesManager.GetToken();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public static void AddAuthorizationHeaderStripe(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_51I5YvcJvKXNzTQcr0FXZ1sCpuOfJuK5bUGPYud0FdXGfhN1jlSAlKaDFEgx1xIvunT95xjYBr49uioeUEag5AZ9L0025qESCRb");
        }
    }
}