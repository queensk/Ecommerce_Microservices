using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auth.Model;
using E_Auth.Model.Dtos;

namespace E_Auth.Profiles
{
    public class AuthProfiles: Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterRequestDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(u => u.Email));
            CreateMap<ApplicationUser, UserDto>();
        }     
    }
}