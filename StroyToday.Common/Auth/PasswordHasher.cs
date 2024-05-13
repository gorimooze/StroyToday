using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroyToday.Application.Interfaces;

namespace StroyToday.Common.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            var passwordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            return passwordHashed;
        }

        public bool Verify(string password, string hashedPassword)
        {
            var result = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);

            return result;
        }
    }
}
