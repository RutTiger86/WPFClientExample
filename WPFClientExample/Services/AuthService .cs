using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IAuthService
    {
        bool Authenticate(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Authenticate(string username, string password)
        {
            string storedSalt = userRepository.GetSalt(username);
            string storedHash = userRepository.GetStoredPasswordHash(username);

            if (string.IsNullOrEmpty(storedSalt) || string.IsNullOrEmpty(storedHash))
                return false;

            string inputHash = HashPassword(password, storedSalt);
            return inputHash == storedHash;
        }

        private static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
