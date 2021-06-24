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
    public class TrainingService : ITrainingService
    {
        private readonly string _baseAddress
            = $"{ApiConstans.BaseAddress}{ApiConstans.TrainingEndpoint}";

        public async Task<string> GetCoachAppointments(int coachId)
        {
            var response = await GetSingleAsync($"/coach/{coachId}");

            return ApiHelper.GetResponseContent(response);
        }

        public async Task<string> GetUserAppointments(int userId)
        {
            var response = await GetSingleAsync($"/user/{userId}");

            return ApiHelper.GetResponseContent(response);
        }

        public async Task<HttpResponseMessage> AddTrainingAsync(TrainingToAddRequest request)
        {
            var data = ApiHelper.CreateJSONStringContent(request);

            var response = await PostSingleAsync(data);

            return response;
        }

        private async Task<HttpResponseMessage> PostSingleAsync(StringContent data)
        {
            using var client = new HttpClient();

            ApiHelper.AddAuthorizationHeader(client);

            var response = await client.PostAsync($"{_baseAddress}", data);

            return response;
        }

        private async Task<HttpResponseMessage> GetSingleAsync(string endpoint)
        {
            using var client = new HttpClient();

            ApiHelper.AddAuthorizationHeader(client);

            var response = await client.GetAsync($"{_baseAddress}{endpoint}");

            return response;
        }

    }
}