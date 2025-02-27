using WPFClientExample.Commons.Statics;
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

        public AuthAccount? GetAuthInfo(string userId)
        {
            return TestDataFactory.TestAuthAcountInfo.FirstOrDefault(p => p.AuthId.Equals(userId));
        }

        public string GetStoredPasswordHash(string userId)
        {
            if (TestDataFactory.TestAuthAcountInfo.Any(p => p.AuthId.Equals(userId)))
            {
                return TestDataFactory.TestAuthAcountInfo.Where(p => p.AuthId.Equals(userId)).First().Password;
            }
            return string.Empty;
        }

        public string GetSalt(string userId)
        {
            return TestDataFactory.TestAuthAcountInfo.Any(p => p.AuthId.Equals(userId)) ? hardcodedSalt : string.Empty;
        }

    }
}
