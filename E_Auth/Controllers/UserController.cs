using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auth.Services.IService;
using Microsoft.AspNetCore.Mvc;
using E_Auth.Model.Dtos;

namespace E_Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserInterface _userInterface;
        private readonly ResponseDto _responseDto;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> Register(RegisterRequestDto userDto)
        {
            var responseMessage = await _userInterface.RegisterUser(userDto);
            if (!string.IsNullOrEmpty(responseMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = responseMessage;
                return BadRequest(_responseDto);
            }
            _responseDto.Message = "User Registered Successfully";
            return Ok(_responseDto);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto userDto)
        {
            var responseMessage = await _userInterface.Login(userDto);
            if (responseMessage.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Credentials";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = responseMessage;
            return Ok(_responseDto);
        }

        [HttpGet("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterRequestDto roleDto)
        {
            var user = await _userInterface.AssignUserRole(roleDto.Email, roleDto.Password);
            if (!user)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Credentials";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = user;
            return Ok(_responseDto);
        }
    }
}