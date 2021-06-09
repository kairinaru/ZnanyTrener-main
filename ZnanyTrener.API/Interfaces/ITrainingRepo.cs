using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Interfaces
{
    public interface ITrainingRepo
    {
        Task AddAsync(Training training);
        void Delete(Training training);
        Task<IEnumerable<Training>> GetAllAsync();
        Task<IEnumerable<Training>> GetAllForUserAsync(int id);
        Task<IEnumerable<Training>> GetAllForCoachAsync(int id);
        Task<Training> GetAsync(int trainingId);
        Task<bool> SaveAllAsync();
    }
}