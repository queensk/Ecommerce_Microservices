using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auth.Model;

namespace E_Auth.Services.IService
{
    public interface IJWtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}