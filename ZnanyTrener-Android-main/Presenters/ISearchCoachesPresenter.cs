using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public interface ISearchCoachesPresenter
    {
        string KeyWord { get; set; }
        List<UserDetailsResponse> Coaches { get; }
        List<string> CoachesUserNames { get; }

        Task GetCoachesAsync();
        void VisitCoachProfile(int position);
    }
}