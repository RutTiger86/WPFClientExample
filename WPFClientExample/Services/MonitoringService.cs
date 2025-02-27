using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.Monitoring;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IMonitoringService
    {
        List<Server> GetServers();
        List<ChatLogInfo> GetChatLogInfos(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate, CancellationToken token);
        List<CcuInfo> GetCcuSeries(DateTime startDate, DateTime endDate);
    }

    public class MonitoringService(IUserRepository userRepository, IServerRepository serverRepository, ILocalizationService localizationService) : IMonitoringService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IServerRepository serverRepository = serverRepository;
        private readonly ILocalizationService localizationService = localizationService;

        public List<Server> GetServers()
        {
            return serverRepository.GetServers();
        }

        public List<ChatLogInfo> GetChatLogInfos(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate, CancellationToken token)
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

            //비동기 테스트 Sleep
            int asyncTest = 0;
            while (asyncTest < 5)
            {
                if (token.IsCancellationRequested)
                {
                    return [];
                }
                asyncTest++;
                Thread.Sleep(1000);
            }

            Thread.Sleep(5000);//비동기 테스트 Sleep
            return userRepository.GetChatLogInfosByCharacterId(characterId, startDate.ToUniversalTime(), endDate.ToUniversalTime());
        }

        public List<CcuInfo> GetCcuSeries(DateTime startDate, DateTime endDate)
        {
            List<Server> servers = serverRepository.GetServers();

            List<CcuInfo> result = [];

            foreach (var server in servers)
            {
                result.AddRange(serverRepository.GetServerCcu(server.Id, startDate, endDate));
            }
            return result;
        }
    }
}
