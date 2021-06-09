using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener.API.Dtos;

namespace ZnanyTrener.API.Interfaces
{
    public interface IUserService
    {
         Task<IAsyncResult> DeleteUser(int userId);
         Task<IAsyncResult> UpdateUser(UserToEditDto userToEdit);
         Task<IEnumerable<UserDetailDto>> FilterCoach(string keyWord);
         Task<UserDetailDto> GetUser(int userId);
         Task<IEnumerable<UserDetailDto>> GetAll();
         Task<IAsyncResult> ChangeAvatar(AvatarForChange avatarForChange);
    }
}