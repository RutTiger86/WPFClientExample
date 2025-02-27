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
        Task<List<ChatLogInfo>?> GetChatLogInfosAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate);
        Task<List<CcuInfo>> GetCcuSeriesAsync(DateTime startDate, DateTime endDate);
    }

    public class MonitoringService : IMonitoringService
    {
        private readonly IUserRepository userRepository;
        private readonly IServerRepository serverRepository;
        private readonly ILocalizationService localizationService;

        public MonitoringService(IUserRepository userRepository, IServerRepository serverRepository, ILocalizationService localizationService)
        {
            this.userRepository = userRepository;
            this.serverRepository = serverRepository;
            this.localizationService = localizationService;
        }

        public Task<List<Server>> GetServers()
        {
            return Task.FromResult(serverRepository.GetServers());
        }
        public Task<List<ChatLogInfo>?> GetChatLogInfosAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate)
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

            var chatLogInfos = userRepository.GetChatLogInfosByCharacterId(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime());
            return Task.FromResult(chatLogInfos);
        }

        public Task<List<CcuInfo>> GetCcuSeriesAsync(DateTime startDate, DateTime endDate)
        {
            List<Server> servers = serverRepository.GetServers();

            List<CcuInfo> result = [];

            foreach (var server in servers)
            {
                result.AddRange(serverRepository.GetServerCcu(server.Id, startDate, endDate));
            }
            return Task.FromResult(result);
        }
    }
}
