using Com.Stripe.Android;
using System.Threading.Tasks;

namespace ZnanyTrener_Android.Presenters
{
    public interface IPaymentPresenter
    {
        Stripe Stripe { get; }

        Task MakePaymentAsync();
    }
}