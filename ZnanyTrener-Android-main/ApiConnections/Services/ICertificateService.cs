using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public interface ICertificateService
    {
        Task<string> AddCertificate(CertificateRequest request);
    }
}