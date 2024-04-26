using AUTH_SERVICE.Models;
using AUTH_SERVICE.Models.DTOS;
using AutoMapper;

namespace AUTH_SERVICE.PROFILES
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, SystemUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(r => r.Email));

            CreateMap<UserDto, SystemUser>().ReverseMap();
        }
    }
}
