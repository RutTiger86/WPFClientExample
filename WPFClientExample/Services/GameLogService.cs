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
        public Task<AccountInfo?> GetAccountInfoAsync(UserSearchType searchType, string searchData);
        public Task<List<CharacterInfo>> GetCharacterInfoListAsync(long accountId);
        public Task<CharacterDetailInfo?> GetCharacterInfoDetailInfoAsync(long characterId);
    }

    public class GameLogService(IUserRepository userRepository) : IGameLogService
    {
        private readonly IUserRepository userRepository = userRepository;

        public Task<AccountInfo?> GetAccountInfoAsync(UserSearchType searchType, string searchData)
        {
            if (searchType == UserSearchType.AccountId)
            {
                if (long.TryParse(searchData, out long accountId))
                {
                    var userinfo = userRepository.GetAccountInfo(searchType, accountId, string.Empty);

                    return Task.FromResult(userinfo);
                }
                else
                {
                    throw new Exception("Account ID can only be numbers.");
                }
            }
            else
            {
                var userinfo = userRepository.GetAccountInfo(searchType, 0, searchData);

                return Task.FromResult(userinfo);
            }

        }

        public Task<List<CharacterInfo>> GetCharacterInfoListAsync(long accountId)
        {
            var characterInfoes = userRepository.GetCharacterInfoList(accountId);

            return Task.FromResult(characterInfoes);
        }

        public Task<CharacterDetailInfo?> GetCharacterInfoDetailInfoAsync(long characterId)
        {
            var characterDetail = userRepository.GetCharacterDetailInfo(characterId);

            return Task.FromResult(characterDetail);
        }

    }
}
