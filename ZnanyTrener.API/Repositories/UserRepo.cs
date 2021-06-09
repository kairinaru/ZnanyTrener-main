using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepo(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> FilterCoaches(string keyWord)
        {
            return await _userManager.Users.Include(u => u.Certifiactes).Include(u => u.UserRoles).ThenInclude(x => x.Role)
                .Where(u => u.UserRoles.FirstOrDefault(x => x.UserId == u.Id).Role.Name == "Coach"
                && (u.City.Contains(keyWord.ToLower()) || u.FirstName.Contains(keyWord.ToLower()) || u.LastName.Contains(keyWord.ToLower())))
                .ToListAsync();
        }

        public async Task<AppUser> GetUser(int id) 
        {
            return await _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role).Include(x => x.Certifiactes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}