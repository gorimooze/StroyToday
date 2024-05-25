using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StroyToday.Application.Helpers;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;
using StroyToday.Application.Interfaces.IServices;

namespace StroyToday.Application.Services
{
    public class UserCvService : IUserCvService
    {
        private readonly IUserCvRepository _userCvRepository;

        public UserCvService(IUserCvRepository userCvRepository)
        {
            _userCvRepository = userCvRepository;
        }

        public async Task Add(UserCvDto userCvDto)
        {
            await _userCvRepository.Add(userCvDto);
        }

        public async Task<GenericResult<UserCvDto>> GetByUserId(int userId)
        {
            var response = new GenericResult<UserCvDto>()
            {
                IsSuccess = true
            };

            var userCvDto = await _userCvRepository.GetById(userId);

            if (userCvDto == null)
            {
                response.ErrorMessage = "User Cv not found";
                response.IsSuccess = false;

                return response;
            }

            response.Result = userCvDto;

            return response;
        }
    }
}
