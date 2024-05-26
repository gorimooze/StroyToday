using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface IUserCvService
    {
        Task Add(string description, int userId);
        Task<GenericResult<UserCvDto>> GetByUserId(int userId);
    }
}
