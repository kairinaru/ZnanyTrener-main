using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;
using ZnanyTrener.API.Others;

namespace ZnanyTrener.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        public UserService(UserManager<AppUser> userManager, IUserRepo userRepo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;

            var account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<IAsyncResult> DeleteUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded) return Task.CompletedTask;

            throw new Exception("Error");
        }

        public async Task<IEnumerable<UserDetailDto>> FilterCoach(string keyWord)
        {
            var coaches = await _userRepo.FilterCoaches(keyWord);

            var coachesToReturn = _mapper.Map<IEnumerable<UserDetailDto>>(coaches);
            
            return coachesToReturn;
        }

        public async Task<UserDetailDto> GetUser(int userId) 
        {
            var user = await _userRepo.GetUser(userId);
            
            var userToReturn = _mapper.Map<UserDetailDto>(user);

            userToReturn.Certificates = _mapper.Map<ICollection<CertificateToAddDto>>(user.Certifiactes);

            return userToReturn;
        }

        public async Task<IAsyncResult> UpdateUser(UserToEditDto userToEdit)
        {
            var user = await _userManager.FindByIdAsync(userToEdit.Id.ToString());

            user.PhoneNumber = userToEdit.PhoneNumber;
            user.Description = userToEdit.Description;
            user.Specialization = userToEdit.Specialization;
            user.Email = userToEdit.Email;
            user.City = userToEdit.City.ToLower();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Task.CompletedTask;

            throw new Exception("Error");
        }

        public async Task<IAsyncResult> ChangeAvatar(AvatarForChange avatarForChange)
        {
            var user = await _userManager.FindByIdAsync(avatarForChange.UserId.ToString());

            var file = avatarForChange.File;

            var uploadResult = new ImageUploadResult();

            if (!string.IsNullOrEmpty(user.PhotoCloudinaryPublicId) &&
                !string.IsNullOrEmpty(user.PhotoUrl) && file.Length > 0)
            {
                var deleteParams = new DeletionParams(user.PhotoCloudinaryPublicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Result == "ok")
                {
                    user.PhotoUrl = string.Empty;
                    user.PhotoCloudinaryPublicId = string.Empty;
                }
                else throw new Exception("Przy zmianie zdjęcia wystąpił bląd.");
            }

            var photoId = user.Id + "_main";

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face"),
                        PublicId = photoId
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            user.PhotoUrl = uploadResult.Url.ToString();
            user.PhotoCloudinaryPublicId = uploadResult.PublicId;

            await _userManager.UpdateAsync(user);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<UserDetailDto>> GetAll() {
            var users = await _userManager.Users.ToListAsync();
            var usersToReturn = _mapper.Map<IEnumerable<UserDetailDto>>(users);
            return usersToReturn;
        }
    }
}