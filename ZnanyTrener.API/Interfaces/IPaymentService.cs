using System.Threading.Tasks;
using Stripe.Checkout;

namespace ZnanyTrener.API.Interfaces
{
    public interface IPaymentService
    {
         Task<Session> CreateSessionAsync();
    }
}