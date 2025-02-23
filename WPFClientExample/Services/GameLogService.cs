using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IGameLogService
    {
        public List<CharacterInfo> GetCharacterInfoList(UserSearchType searchType, string searchData);
    }

    public class GameLogService(IUserRepository userRepository) : IGameLogService
    {
        private readonly IUserRepository userRepository = userRepository;

        public List<CharacterInfo> GetCharacterInfoList(UserSearchType searchType, string searchData)
        {

            if(searchType == UserSearchType.AccountId)
            {
                if(long.TryParse(searchData, out long accountId))
                {
                    return userRepository.GetCharacterInfoList(searchType, accountId, string.Empty);
                }
                else
                {
                    throw new Exception("Account ID can only be numbers.");
                }
            }
            else
            {
                return userRepository.GetCharacterInfoList(searchType, 0, searchData);
            }
        }
    }
}
