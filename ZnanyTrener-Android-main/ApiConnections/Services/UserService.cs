using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public class UserService : IUserService
    {
        private readonly string _baseAddress
            = $"{ApiConstans.BaseAddress}{ApiConstans.UserEndpoint}";

        public async Task<string> PostPhotoAsync(AddPhotoRequest request)
        {
            var response = await SendAddPhotoRequest(request);

            return ApiHelper.GetResponseContent(response); 
        }

        public async Task<string> GetCoachesAsync(string keyWord)
        {
            var response = await SendGetRequestAsync($"?keyWord={keyWord}");

            return ApiHelper.GetResponseContent(response);
        }

        public async Task<string> GetUserAsync(int userId)
        {
            var response = await SendGetRequestAsync($"/{userId}");

            return ApiHelper.GetResponseContent(response);
        }

        private async Task<HttpResponseMessage> SendAddPhotoRequest(AddPhotoRequest request)
        {
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();

            ApiHelper.AddAuthorizationHeader(client);

            var imageContent = new ByteArrayContent(request.File);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");

            content.Add(imageContent, "File", "image.jpg");

            var response = await client.PostAsync($"{_baseAddress}", content);

            return response;
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(string endpoint)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_baseAddress}{endpoint}");

            return response;
        }
    }
}