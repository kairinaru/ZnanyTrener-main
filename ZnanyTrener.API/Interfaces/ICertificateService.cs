using System;
using System.Threading.Tasks;
using ZnanyTrener.API.Dtos;

namespace ZnanyTrener.API.Interfaces
{
    public interface ICertificateService
    {
         Task<IAsyncResult> AddCertificate(CertificateToAddDto certificateToAdd);
    }
}