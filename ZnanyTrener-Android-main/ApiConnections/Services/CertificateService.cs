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
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly string _baseAddress
            = $"{ApiConstans.BaseAddress}{ApiConstans.CertificateEndpoint}";

        public async Task<string> AddCertificate(CertificateRequest request)
        {
            var data = ApiHelper.CreateJSONStringContent(request);

            var response = await SendRequestAsync(data);

            return ApiHelper.GetResponseContent(response);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(StringContent data)
        {
            using var client = new HttpClient();

            ApiHelper.AddAuthorizationHeader(client);

            var response = await client.PostAsync($"{_baseAddress}", data);

            return response;
        }
    }
}