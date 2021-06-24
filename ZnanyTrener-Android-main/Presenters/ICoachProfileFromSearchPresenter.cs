using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public interface ICoachProfileFromSearchPresenter
    {
        UserDetailsResponse UserFromApi { get; }

        Task GetUserAsync();
        string GetCertificatesInOneString();
    }
}