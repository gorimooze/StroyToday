﻿using StroyToday.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Core.IRepositories
{
    public interface IUserRepository
    {
        Task Add(UserDto user);
        Task<UserDto> GetByEmail(string email);
    }
}
