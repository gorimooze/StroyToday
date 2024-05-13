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

        public async Task<string> Login(string email, string password)
        {
            var userDto = await _userRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, userDto.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(userDto);

            return token;
        }
    }
}
