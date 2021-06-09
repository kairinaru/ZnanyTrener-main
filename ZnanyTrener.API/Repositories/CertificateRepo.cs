using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Others;

namespace ZnanyTrener.API.Repositories
{
    public class CertificateRepo : ICertificateRepo
    {
        private readonly DataContextIdentity _context;
        public CertificateRepo(DataContextIdentity context)
        {
            _context = context;

        }
        public async Task AddAsync(Certificate certificate)
        {
            await _context.AddAsync(certificate);
        }

        public void Delete(Certificate certificate)
        {
            _context.Remove(certificate);
        }

        public async Task<IEnumerable<Certificate>> GetAllAsync()
        {
            return await _context.Certificate
                .Include(x => x.User)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Certificate>> GetAllForUserAsync(int id)
        {
            return await _context.Certificate.Where(x => x.UserId == id)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Certificate> GetAsync(int certificateId)
        {
            return await _context.Certificate
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == certificateId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}