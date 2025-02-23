using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models.DataBase;

namespace WPFClientExample.Repositories
{
    public interface IAuthRepository
    {
        AuthAccount? GetAuthInfo(string userId);
        string GetStoredPasswordHash(string userId);
        string GetSalt(string userId);
    }
    public class AuthRepository : IAuthRepository
    {
        private static readonly string hardcodedSalt = "ucZpbywROXXRD9xMW2bvBw==";

        private List<AuthAccount> hardcodedUserInfo =
        [
            new AuthAccount()
            {
                 Id = 1,
                 AuthId = "admin",
                 Password = "fY5qnfEZcadyCX6A+jWQSxaW3lRoKi35PpQdM0lEDAg=" //1234
            }
        ];

        public AuthAccount? GetAuthInfo(string userId)
        {
            return hardcodedUserInfo.FirstOrDefault(p => p.AuthId.Equals(userId));
        }

        public string GetStoredPasswordHash(string userId)
        {
            if (hardcodedUserInfo.Any(p => p.AuthId.Equals(userId)))
            {
                return hardcodedUserInfo.Where(p=> p.AuthId.Equals(userId)).First().Password;
            }
            return string.Empty;
        }

        public string GetSalt(string userId)
        {
            return hardcodedUserInfo.Any(p=> p.AuthId.Equals(userId)) ? hardcodedSalt : string.Empty;
        }

    }
}
