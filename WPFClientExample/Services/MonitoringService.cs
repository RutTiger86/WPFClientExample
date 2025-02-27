using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.Monitoring;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IMonitoringService
    {
        Task<List<Server>> GetServers();
        Task<List<ChatLogInfo>> GetChatLogInfosAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate);
        Task<List<CcuInfo>> GetCcuSeriesAsync(DateTime startDate, DateTime endDate);
    }

    public class MonitoringService(IUserRepository userRepository, IServerRepository serverRepository, ILocalizationService localizationService) : IMonitoringService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IServerRepository serverRepository = serverRepository;
        private readonly ILocalizationService localizationService = localizationService;

        public async Task<List<Server>> GetServers()
        {
            return await Task.Run(() => serverRepository.GetServers());
        }
        public async Task<List<ChatLogInfo>> GetChatLogInfosAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate)
        {
            long characterId = 0;

            if (endDate < startDate)
            {
                throw new Exception(localizationService.GetString("MessageInvalidDateRange"));
            }

            if (!String.IsNullOrWhiteSpace(searchData))
            {
                if (searchType == USER_SEARCH_TYPE.NAME)
                {
                    var characterInfo = userRepository.GetCharacterInfoByCharacterName(searchData);

                    if (characterInfo != null)
                    {
                        characterId = characterInfo.CharacterId;
                    }
                    else
                    {
                        throw new Exception(localizationService.GetString("MessageCharacterNameNotExist"));
                    }
                }
                else
                {
                    if (!long.TryParse(searchData, out characterId))
                    {
                        throw new Exception(localizationService.GetString("MessageCharacterIdParseFaile"));
                    }

                }
            }

            return await Task.Run(() => userRepository.GetChatLogInfosByCharacterId(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime()));
        }

        public async Task<List<CcuInfo>> GetCcuSeriesAsync(DateTime startDate, DateTime endDate)
        {
            List<Server> servers = serverRepository.GetServers();

            List<CcuInfo> result = [];

            foreach (var server in servers)
            {
                result.AddRange(await Task.Run(() => serverRepository.GetServerCcu(server.Id, startDate, endDate)));
            }
            return result;
        }
    }
}
