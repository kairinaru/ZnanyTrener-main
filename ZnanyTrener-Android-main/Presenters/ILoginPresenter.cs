using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public interface ILoginPresenter
    {
        string UserName { get; set; }
        string Password { get; set; }

        Task LoginUserAsync();
    }
}