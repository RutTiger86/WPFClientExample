using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IGameLogService
    {
        public Task<AccountInfo?> GetAccountInfoAsync(USER_SEARCH_TYPE searchType, string searchData);
        public Task<List<CharacterInfo>> GetCharacterInfoListAsync(long accountId);
        public Task<CharacterDetailInfo?> GetCharacterInfoDetailInfoAsync(long characterId);
        public Task<List<CharacterEquipeedInfo>?> GetCharacterEquipeedInfoAsync(long characterId);
        public Task<List<ChatLogInfo>?> GetChatLogInfoByCharacterIdAsync(long characterId);
        public Task<List<CharacterQuestInfo>?> GetCharacterQuestInfoByCharacterIdAsync(long characterId);
        public Task<List<InventoryHistoryLogInfo>?> GetInventoryHistoryLogAsync(long characterId, DateTime startDate, DateTime endDate);
    }

    public class GameLogService(IUserRepository userRepository) : IGameLogService
    {
        private readonly IUserRepository userRepository = userRepository;

        public Task<AccountInfo?> GetAccountInfoAsync(USER_SEARCH_TYPE searchType, string searchData)
        {
            long accountID = 0;

            if (!String.IsNullOrWhiteSpace(searchData))
            {
                if (searchType == USER_SEARCH_TYPE.NAME)
                {
                    var accountInfo = userRepository.GetAccountInfoByName(searchData);

                    return Task.FromResult(accountInfo);
                }
                else
                {
                    if (long.TryParse(searchData, out accountID))
                    {
                        var userinfo = userRepository.GetAccountInfo(accountID);

                        return Task.FromResult(userinfo);
                    }
                    else
                    {
                        throw new Exception("Account ID can only be numbers.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Need Account ID","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return Task.FromResult< AccountInfo?>(null);
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

        public Task<List<CharacterEquipeedInfo>?> GetCharacterEquipeedInfoAsync(long characterId)
        {
            var characterEquiped = userRepository.GetCharacterEquipeedInfo(characterId);

            return Task.FromResult(characterEquiped);
        }

        public Task<List<ChatLogInfo>?> GetChatLogInfoByCharacterIdAsync(long characterId)
        {
            var chatLog = userRepository.GetChatLogInfosByCharacterId(characterId);

            return Task.FromResult(chatLog);
        }

        public Task<List<CharacterQuestInfo>?> GetCharacterQuestInfoByCharacterIdAsync(long characterId)
        {
            var charQuestInfos = userRepository.GetCharacterQuestInfoByCharacterId(characterId);

            return Task.FromResult(charQuestInfos);
        }

        public Task<List<InventoryHistoryLogInfo>?> GetInventoryHistoryLogAsync(long characterId, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new Exception("Invalid date range setting");
            }

            var InventoryInfos = userRepository.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime());
            return Task.FromResult(InventoryInfos);
        }

    }
}
