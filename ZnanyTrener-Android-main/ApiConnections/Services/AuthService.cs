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
using System.Threading.Tasks;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Exceptions;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _baseAddress
            = $"{ApiConstans.BaseAddress}{ApiConstans.AuthEndpoint}";

        public async Task<string> LoginAsync(UserToLoginRequest request)
        {
            var data = ApiHelper.CreateJSONStringContent(request);

            var response = await SendRequestAsync(data, "/login");

            return ApiHelper.GetResponseContent(response);
        }

        public async Task<string> RegisterAsync(UserToReqisterRequest request)
        {
            var data = ApiHelper.CreateJSONStringContent(request);

            var response = await SendRequestAsync(data, "/register");

            return ApiHelper.GetResponseContent(response);
        }
      
        private async Task<HttpResponseMessage> SendRequestAsync(StringContent data, string endpoint)
        {
            using var client = new HttpClient();

            var response = await client.PostAsync($"{_baseAddress}{endpoint}", data);

            return response;
        }

        
    }
}