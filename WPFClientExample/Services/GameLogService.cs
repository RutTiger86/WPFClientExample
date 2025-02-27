using System.Windows;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IGameLogService
    {
        public AccountInfo? GetAccountInfo(USER_SEARCH_TYPE searchType, string searchData);
        public CharacterDetailInfo? GetCharacterInfoDetailInfo(long characterId);
        public List<CharacterInfo> GetCharacterInfoList(long accountId);
        public List<CharacterEquipeedInfo> GetCharacterEquipeedInfo(long characterId);
        public List<ChatLogInfo> GetChatLogInfoByCharacterId(long characterId);
        public List<CharacterQuestInfo> GetCharacterQuestInfoByCharacterId(long characterId);
        public List<InventoryHistoryLogInfo> GetInventoryHistoryLog(long characterId, DateTime startDate, DateTime endDate);
    }

    public class GameLogService(IUserRepository userRepository, ILocalizationService localizationService) : IGameLogService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly ILocalizationService localizationService = localizationService;

        public AccountInfo? GetAccountInfo(USER_SEARCH_TYPE searchType, string searchData)
        {
            long accountID = 0;

            if (!String.IsNullOrWhiteSpace(searchData))
            {
                if (searchType == USER_SEARCH_TYPE.NAME)
                {
                    return userRepository.GetAccountInfoByName(searchData);
                }
                else
                {
                    if (long.TryParse(searchData, out accountID))
                    {
                        return userRepository.GetAccountInfo(accountID);
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

        public List<CharacterInfo> GetCharacterInfoList(long accountId)
        {
            return userRepository.GetCharacterInfoList(accountId);
        }

        public CharacterDetailInfo? GetCharacterInfoDetailInfo(long characterId)
        {
            return userRepository.GetCharacterDetailInfo(characterId);
        }

        public List<CharacterEquipeedInfo> GetCharacterEquipeedInfo(long characterId)
        {
            return userRepository.GetCharacterEquipeedInfo(characterId);
        }

        public List<ChatLogInfo> GetChatLogInfoByCharacterId(long characterId)
        {
            return userRepository.GetChatLogInfosByCharacterId(characterId);
        }

        public List<CharacterQuestInfo> GetCharacterQuestInfoByCharacterId(long characterId)
        {
            return userRepository.GetCharacterQuestInfoByCharacterId(characterId);
        }

        public List<InventoryHistoryLogInfo> GetInventoryHistoryLog(long characterId, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new Exception(localizationService.GetString("MessageInvalidDateRange"));
            }
            return userRepository.GetInventoryHistoryLog(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime());
        }

    }
}
