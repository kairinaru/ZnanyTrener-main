using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;
using ZnanyTrener.API.Others;

namespace ZnanyTrener.API.Repositories
{
    public class TrainingRepo : ITrainingRepo
    {
        private readonly DataContextIdentity _context;
        public TrainingRepo(DataContextIdentity context)
        {
            _context = context;

        }
        public async Task AddAsync(Training training)
        {
            await _context.AddAsync(training);
        }

        public void Delete(Training training)
        {
            _context.Remove(training);
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _context.Trainings
                .Include(x => x.Coach)
                .Include(x => x.User)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Training>> GetAllForUserAsync(int id)
        {
            return await _context.Trainings.Where(x => x.UserId == id)
                .Include(x => x.User)
                .Include(x => x.Coach)
                .ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetAllForCoachAsync(int id)
        {
            return await _context.Trainings.Where(x => x.CoachId == id)
                .Include(x => x.User)
                .Include(x => x.Coach)
                .ToListAsync();
        }

        public async Task<Training> GetAsync(int trainingId)
        {
            return await _context.Trainings
                .Include(x => x.User)
                .Include(x => x.Coach)
                .FirstOrDefaultAsync(x => x.Id == trainingId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}