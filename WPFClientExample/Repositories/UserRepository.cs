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
        string GetStoredPasswordHash(string userName);
        string GetSalt(string userName);
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

        public string GetStoredPasswordHash(string username)
        {
            if (hardcodedUserInfo.Any(p => p.UserId.Equals(username)))
            {
                return hardcodedUserInfo.Where(p=> p.UserId.Equals(username)).First().Password;
            }
            return string.Empty;
        }

        public string GetSalt(string username)
        {
            return hardcodedUserInfo.Any(p=> p.UserId.Equals(username)) ? hardcodedSalt : string.Empty;
        }

    }
}
