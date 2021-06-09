using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepo _trainingRepo;
        private readonly IMapper _mapper;
        public TrainingService(ITrainingRepo trainingRepo, IMapper mapper)
        {
            this._mapper = mapper;
            this._trainingRepo = trainingRepo;
        }
        public async Task<IAsyncResult> AddAsync(TrainingToAddDto trainingToAdd)
        {
            var training = _mapper.Map<Training>(trainingToAdd);
            training.StartDate = training.StartDate.ToUniversalTime().AddHours(1);
            training.EndDate = training.EndDate.ToUniversalTime().AddHours(1);

            await _trainingRepo.AddAsync(training);

            if(await _trainingRepo.SaveAllAsync()) return Task.CompletedTask;

            throw new Exception("Error");
        }

        public async Task<IAsyncResult> DeleteAsync(int id)
        {
            var training = await _trainingRepo.GetAsync(id);

            if(training == null) throw new Exception("not found");

            _trainingRepo.Delete(training);

            if(await _trainingRepo.SaveAllAsync()) return Task.CompletedTask;

            throw new Exception("Error");
        }

        public async Task<TrainingToReturnDto> GetAsync(int id)
        {
            var training = await _trainingRepo.GetAsync(id);

            if(training == null) throw new Exception("not found");

            var toReturn = _mapper.Map<TrainingToReturnDto>(training);

            return toReturn;
        }

        public async Task<IEnumerable<TrainingToReturnDto>> GetForCoachAsync(int coachId)
        {
            var trainings = await _trainingRepo.GetAllForCoachAsync(coachId);

            var toReturn = _mapper.Map<IEnumerable<TrainingToReturnDto>>(trainings);

            return toReturn;
        }

        public async Task<IEnumerable<TrainingToReturnDto>> GetForUserAsync(int userId)
        {
            var trainings = await _trainingRepo.GetAllForUserAsync(userId);

            var toReturn = _mapper.Map<IEnumerable<TrainingToReturnDto>>(trainings);

            return toReturn;
        }
    }
}