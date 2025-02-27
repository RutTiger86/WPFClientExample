using WPFClientExample.Commons.Statics;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.Monitoring;

namespace WPFClientExample.Repositories
{
    public interface IServerRepository
    {
        List<Server> GetServers();
        List<CcuInfo> GetServerCcu(int serverID, DateTime startDate, DateTime endDate);
    }

    public class ServerRepository : IServerRepository
    {
        public List<Server> GetServers()
        {
            return TestDataFactory.TestServers.ToList();
        }

        public List<CcuInfo> GetServerCcu(int serverID, DateTime startDate, DateTime endDate)
        {
            List<CcuInfo> result = [];
            Random random = new();
            for (DateTime targetDate = startDate; targetDate <= endDate; targetDate = targetDate.AddMinutes(1))
            {
                result.Add(new CcuInfo()
                {
                    ServerId = serverID,
                    CcuValue = new KeyValuePair<DateTime, int>(targetDate, random.Next(1000, 10000))
                });
            }

            return result;
        }
    }
}
