using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Auth.Model.Dtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
    }
}