using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserToLoginRequest request);
        Task<string> RegisterAsync(UserToReqisterRequest request);
    }
}