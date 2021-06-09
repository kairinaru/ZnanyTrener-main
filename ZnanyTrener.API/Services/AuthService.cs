using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;
using ZnanyTrener.API.Others;

namespace ZnanyTrener.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICertificateRepo _certificateRepo;

        public AuthService(IMapper mapper, UserManager<AppUser> manager,
            SignInManager<AppUser> signInManager, ITokenService tokenService, ICertificateRepo certificateRepo)
        {
            _mapper = mapper;
            _userManager = manager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _certificateRepo = certificateRepo;
        }
        public async Task<UserDetailDto> LoginUserAsync(UserToLoginDto userToLoginDTO)
        {
            userToLoginDTO.UserName = userToLoginDTO.UserName.ToLower();

            var userFromRepo = await _userManager.Users
                .Include(x => x.Certifiactes).FirstOrDefaultAsync(x => x.UserName == userToLoginDTO.UserName);

            if (userFromRepo == null)
                throw new Exception("Wrong credentials.");

            var roles = await _userManager.GetRolesAsync(userFromRepo);
     
            var result = await _signInManager
                .CheckPasswordSignInAsync(userFromRepo, userToLoginDTO.Password, false);

            if (!result.Succeeded) throw new Exception("Error occurred");

            var userToReturn = _mapper.Map<UserDetailDto>(userFromRepo);

            userToReturn.Role = roles[0];
            userToReturn.Token = await _tokenService.CreateTokenAsync(userFromRepo);
            userToReturn.Certificates = _mapper.Map<ICollection<CertificateToAddDto>>(userFromRepo.Certifiactes);

            return userToReturn;
        }
        public async Task<IAsyncResult> RegisterUserAsync(UserToRegisterDto userToRegisterDTO)
        {
            userToRegisterDTO.UserName = userToRegisterDTO.UserName.ToLower();
            userToRegisterDTO.FirstName = userToRegisterDTO.FirstName.ToLower();
            userToRegisterDTO.LastName = userToRegisterDTO.LastName.ToLower();
            userToRegisterDTO.Email = userToRegisterDTO.Email.ToLower();
            userToRegisterDTO.City = userToRegisterDTO.City.ToLower();

            if (await _userManager.FindByNameAsync(userToRegisterDTO.UserName) != null)
                throw new Exception("Username is already taken.");

            var userToCreate = _mapper.Map<AppUser>(userToRegisterDTO);

            if (userToRegisterDTO.Role == "Admin" || userToRegisterDTO.Role == "User" || userToRegisterDTO.Role == "Coach")
            {
                var result = await _userManager.CreateAsync(userToCreate, userToRegisterDTO.Password);

                await _userManager.AddToRoleAsync(userToCreate, userToRegisterDTO.Role);

                if (!result.Succeeded) throw new Exception(result.Errors.ToString());

                //Dodawanie certyfikatu podanego w rejestracji
                if(!(string.IsNullOrEmpty(userToRegisterDTO.CertificateNumber) && 
                string.IsNullOrEmpty(userToRegisterDTO.CertificateInstitution)))
                {
                    var user = await _userManager.FindByNameAsync(userToRegisterDTO.UserName.ToLower());

                    CertificateToAddDto certificate = new CertificateToAddDto
                    {
                        UserId = user.Id,
                        GainDate = userToRegisterDTO.CertificateGainDate,
                        Number = userToRegisterDTO.CertificateNumber,
                        Institution = userToRegisterDTO.CertificateInstitution
                    };

                    var cert = _mapper.Map<Certificate>(certificate);

                    await _certificateRepo.AddAsync(cert);

                    if (await _certificateRepo.SaveAllAsync()) return Task.CompletedTask;

                    throw new Exception("Error occured");
                }

                return Task.CompletedTask;
            }

            throw new Exception("Role doesn't exist");

        }
    }
}