using StroyToday.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(UserDto user);
        string GetUserIdFromToken(string token);
    }
}
