using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models;

namespace WPFClientExample.Repositories
{
    public interface IUserRepository
    {
        UserInfo? GetUserInfo(string userId);
        string GetStoredPasswordHash(string userId);
        string GetSalt(string userId);
    }
    public class UserRepository : IUserRepository
    {
        private static readonly string hardcodedSalt = "ucZpbywROXXRD9xMW2bvBw==";

        private List<UserInfo> hardcodedUserInfo =
        [
            new UserInfo()
            {
                 Id = 1,
                 UserId = "admin",
                 Password = "fY5qnfEZcadyCX6A+jWQSxaW3lRoKi35PpQdM0lEDAg=" //1234
            }
        ];

        public UserInfo? GetUserInfo(string userId)
        {
            return hardcodedUserInfo.FirstOrDefault(p => p.UserId.Equals(userId));
        }

        public string GetStoredPasswordHash(string userId)
        {
            if (hardcodedUserInfo.Any(p => p.UserId.Equals(userId)))
            {
                return hardcodedUserInfo.Where(p=> p.UserId.Equals(userId)).First().Password;
            }
            return string.Empty;
        }

        public string GetSalt(string userId)
        {
            return hardcodedUserInfo.Any(p=> p.UserId.Equals(userId)) ? hardcodedSalt : string.Empty;
        }

    }
}
