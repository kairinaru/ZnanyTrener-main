using System.Linq;
using AutoMapper;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Entities;

namespace ZnanyTrener.API.Others
{
    public class AutoMapperManager : Profile
    {
        public AutoMapperManager()
        {
            CreateMap<UserToLoginDto, AppUser>().ReverseMap();
            CreateMap<UserToRegisterDto, AppUser>().ReverseMap();
            CreateMap<AppUser, UserDetailDto>()
                .ForMember(opt => opt.Role, src => 
                src.MapFrom(src => src.UserRoles.FirstOrDefault(x => 
                x.UserId == src.Id).Role.Name));
            CreateMap<CertificateToAddDto, Certificate>().ReverseMap();
            CreateMap<TrainingToAddDto, Training>().ReverseMap();
            CreateMap<Training, TrainingToReturnDto>()
                .ForMember(opt => opt.UserFirstName, src =>
                src.MapFrom(src => src.User.FirstName))
                .ForMember(opt => opt.UserLastName, src =>
                src.MapFrom(src => src.User.LastName))
                .ForMember(opt => opt.UserPhoneNumber, src =>
                src.MapFrom(src => src.User.PhoneNumber))
                .ForMember(opt => opt.CoachFirstName, src =>
                src.MapFrom(src => src.Coach.FirstName))
                .ForMember(opt => opt.CoachLastName, src =>
                src.MapFrom(src => src.Coach.LastName))
                .ForMember(opt => opt.CoachPhoneNumber, src =>
                src.MapFrom(src => src.Coach.PhoneNumber));
        }
    }
}