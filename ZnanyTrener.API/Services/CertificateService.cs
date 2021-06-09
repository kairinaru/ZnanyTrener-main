using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Entities;
using ZnanyTrener.API.Interfaces;
using ZnanyTrener.API.Others;

namespace ZnanyTrener.API.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICertificateRepo _certificateRepo;
        public CertificateService(IMapper mapper, ICertificateRepo certificateRepo, UserManager<AppUser> userManager)
        {
            _certificateRepo = certificateRepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IAsyncResult> AddCertificate(CertificateToAddDto certificateToAdd)
        {
            var user = await _userManager.FindByIdAsync(certificateToAdd.UserId.ToString());

            if(user == null) throw new Exception("User not found");

            var cert = _mapper.Map<Certificate>(certificateToAdd);

            await _certificateRepo.AddAsync(cert);

            if(await _certificateRepo.SaveAllAsync()) return Task.CompletedTask;

            throw new Exception("Error occured");
        }
    }
}