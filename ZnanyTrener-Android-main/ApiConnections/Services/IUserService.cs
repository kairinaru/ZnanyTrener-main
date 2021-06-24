using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public interface IUserService
    {
        Task<string> GetCoachesAsync(string keyWord);
        Task<string> GetUserAsync(int userId);
        Task<string> PostPhotoAsync(AddPhotoRequest request);
    }
}