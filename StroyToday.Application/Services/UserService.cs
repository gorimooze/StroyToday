using StroyToday.Application.Helpers;
using StroyToday.Application.Interfaces;
using StroyToday.Core.Dto;
using StroyToday.Core.IRepositories;

namespace StroyToday.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var userDto = new UserDto()
            {
                UserName = userName,
                Email = email,
                PasswordHash = hashedPassword
            };

            await _userRepository.Add(userDto);
        }

        public async Task<GenericResult<string>> Login(string email, string password)
        {
            var response = new GenericResult<string>()
            {
                IsSuccess = true
            };

            var userDto = await _userRepository.GetByEmail(email);

            if (userDto == null)
            {
                response.Result = "User not found";
                response.IsSuccess = false;

                return response;
            }

            var checkPassword = _passwordHasher.Verify(password, userDto.PasswordHash);

            if (checkPassword == false)
            {
                response.Result = "Incorrect Password";
                response.IsSuccess = false;

                return response;
            }

            var token = _jwtProvider.GenerateToken(userDto);

            response.Result = token;

            return response;
        }
    }
}
