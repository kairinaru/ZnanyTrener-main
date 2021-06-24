using System.Threading.Tasks;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public interface IPaymentService
    {
        Task<string> GetClientSecretAsync();
    }
}