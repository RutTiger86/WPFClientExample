using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IAuthService
    {
        (bool AuthResult, AuthAccount? User) Authenticate(string userId, string password);
    }

    public class AuthService(IAuthRepository userRepository) : IAuthService
    {
        private readonly IAuthRepository userRepository = userRepository;

        public (bool AuthResult, AuthAccount? User) Authenticate(string userId, string password)
        {
            string storedSalt = userRepository.GetSalt(userId);
            string storedHash = userRepository.GetStoredPasswordHash(userId);

            if (string.IsNullOrEmpty(storedSalt) || string.IsNullOrEmpty(storedHash))
                return (false, null);

            string inputHash = HashPassword(password, storedSalt);
            return (inputHash == storedHash, inputHash == storedHash?userRepository.GetAuthInfo(userId):null);
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
