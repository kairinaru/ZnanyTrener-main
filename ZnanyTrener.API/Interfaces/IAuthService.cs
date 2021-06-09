using System;
using System.Threading.Tasks;
using ZnanyTrener.API.Dtos;

namespace ZnanyTrener.API.Interfaces
{
    public interface IAuthService
    {
        Task<IAsyncResult> RegisterUserAsync(UserToRegisterDto userToRegisterDTO);
        Task<UserDetailDto> LoginUserAsync(UserToLoginDto userToLoginDTO);
    }
}