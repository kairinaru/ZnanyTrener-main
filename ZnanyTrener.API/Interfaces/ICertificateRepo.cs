using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Others
{
    public interface ICertificateRepo
    {
        Task AddAsync(Certificate certificate);
        void Delete(Certificate certificate);
        Task<IEnumerable<Certificate>> GetAllAsync();
        Task<IEnumerable<Certificate>> GetAllForUserAsync(int id);
        Task<Certificate> GetAsync(int certificateId);
        Task<bool> SaveAllAsync();
    }
}