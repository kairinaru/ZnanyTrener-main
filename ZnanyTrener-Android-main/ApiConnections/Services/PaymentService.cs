using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.Helpers;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _baseAddress
            = $"{ApiConstans.BaseAddress}{ApiConstans.PaymentEndpoint}";

        public async Task<string> GetClientSecretAsync()
        {
            var response = await SendRequestAsync();

            return ApiHelper.GetResponseContent(response);
        }

        private async Task<HttpResponseMessage> SendRequestAsync()
        {
            using var client = new HttpClient();

            ApiHelper.AddAuthorizationHeader(client);

            var response = await client.GetAsync($"{_baseAddress}/mobile");

            return response;
        }

        
    }
}