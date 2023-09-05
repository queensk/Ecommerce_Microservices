using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Auth.Data;
using E_Auth.Model;
using E_Auth.Model.Dtos;
using E_Auth.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Auth.Services
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJWtTokenGenerator _jwtTokenGenerator;
        public UserService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IJWtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignUserRole(string email, string Rolename)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, Rolename);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u=>u.UserName.ToLower()==loginRequestDto.Username.ToLower());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user != null && isValid)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, userRoles);
                return new LoginResponseDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token
                };
            }
            return new LoginResponseDto();
        }

        public async Task<string> RegisterUser(RegisterRequestDto registerRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerRequestDto);
            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}