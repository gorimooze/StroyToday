﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;

namespace StroyToday.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task Register(string userName, string email, string password);
        Task<GenericResult<LoginResponseDto>> Login(string email, string password);
    }
}
