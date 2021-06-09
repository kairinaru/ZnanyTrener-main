using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Interfaces
{
    public interface IUserRepo
    {
         Task<IEnumerable<AppUser>> FilterCoaches(string keyWord);
         Task<AppUser> GetUser(int id);
    }
}