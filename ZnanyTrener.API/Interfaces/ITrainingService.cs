using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener.API.Dtos;

namespace ZnanyTrener.API.Interfaces
{
    public interface ITrainingService
    {
         Task<IAsyncResult> AddAsync(TrainingToAddDto trainingToAdd);
         Task<IAsyncResult> DeleteAsync(int id);
         Task<IEnumerable<TrainingToReturnDto>> GetForUserAsync(int userId);
         Task<IEnumerable<TrainingToReturnDto>> GetForCoachAsync(int coachId);
         Task<TrainingToReturnDto> GetAsync(int id);
    }
}