using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IGameLogService
    {
        public Task<AccountInfo?> GetAccountInfoAsync(USER_SEARCH_TYPE searchType, string searchData);
        public Task<CharacterDetailInfo?> GetCharacterInfoDetailInfoAsync(long characterId);
        public Task<List<CharacterInfo>> GetCharacterInfoListAsync(long accountId);
        public Task<List<CharacterEquipeedInfo>> GetCharacterEquipeedInfoAsync(long characterId);
        public Task<List<ChatLogInfo>> GetChatLogInfoByCharacterIdAsync(long characterId);
        public Task<List<CharacterQuestInfo>> GetCharacterQuestInfoByCharacterIdAsync(long characterId);
        public Task<List<InventoryHistoryLogInfo>> GetInventoryHistoryLogAsync(long characterId, DateTime startDate, DateTime endDate);
    }

    public class GameLogService(IUserRepository userRepository, ILocalizationService localizationService) : IGameLogService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly ILocalizationService localizationService = localizationService;

        public async Task<AccountInfo?> GetAccountInfoAsync(USER_SEARCH_TYPE searchType, string searchData)
        {
            long accountID = 0;

            if (!String.IsNullOrWhiteSpace(searchData))
            {
                if (searchType == USER_SEARCH_TYPE.NAME)
                {
                    return await Task.Run(() => userRepository.GetAccountInfoByName(searchData));
                }
                else
                {
                    if (long.TryParse(searchData, out accountID))
                    {
                        return await Task.Run(() => userRepository.GetAccountInfo(accountID));
                    }
                    else
                    {
                        throw new Exception(localizationService.GetString("MessageCharacterIdParseFaile"));
                    }
                }
            }
            else
            {
                MessageBox.Show(localizationService.GetString("MessageNeedAccountID"), localizationService.GetString("ErrorCaption"), MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public async Task<List<CharacterInfo>> GetCharacterInfoListAsync(long accountId)
        {
            return await Task.Run(() => userRepository.GetCharacterInfoList(accountId));
        }

        public async Task<CharacterDetailInfo?> GetCharacterInfoDetailInfoAsync(long characterId)
        {
            return await Task.Run(() => userRepository.GetCharacterDetailInfo(characterId));
        }

        public async Task<List<CharacterEquipeedInfo>> GetCharacterEquipeedInfoAsync(long characterId)
        {
            return await Task.Run(() => userRepository.GetCharacterEquipeedInfo(characterId));
        }

        public async Task<List<ChatLogInfo>> GetChatLogInfoByCharacterIdAsync(long characterId)
        {
            return await Task.Run(() => userRepository.GetChatLogInfosByCharacterId(characterId));
        }

        public async Task<List<CharacterQuestInfo>> GetCharacterQuestInfoByCharacterIdAsync(long characterId)
        {
            return await Task.Run(() => userRepository.GetCharacterQuestInfoByCharacterId(characterId));
        }

        public async Task<List<InventoryHistoryLogInfo>> GetInventoryHistoryLogAsync(long characterId, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new Exception(localizationService.GetString("MessageInvalidDateRange"));
            }

            return await Task.Run(() => userRepository.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime()));
        }

    }
}
