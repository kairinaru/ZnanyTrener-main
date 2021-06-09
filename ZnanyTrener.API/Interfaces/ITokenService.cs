using System.Threading.Tasks;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Interfaces
{
    public interface ITokenService
    {
         Task<string> CreateTokenAsync(AppUser user);
    }
}