using WPFClientExample.Commons.Enums;
using WPFClientExample.Models.Billing;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IBillingService
    {
        public List<BillHistoryInfo> GetBillHistoryInfo(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate, CancellationToken token);
    }

    public class BillingService(IBillingRepository billingRepository, IUserRepository userRepository, ILocalizationService localizationService) : IBillingService
    {
        private readonly IBillingRepository billingRepository = billingRepository;
        private readonly IUserRepository userRepository = userRepository;
        private readonly ILocalizationService localizationService = localizationService;

        public List<BillHistoryInfo> GetBillHistoryInfo(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate, CancellationToken token)
        {
            long accountID = 0;

            if (endDate < startDate)
            {
                throw new Exception(localizationService.GetString("MessageInvalidDateRange"));
            }

            if (!String.IsNullOrWhiteSpace(searchData))
            {
                if (searchType == USER_SEARCH_TYPE.NAME)
                {
                    var accountInfo = userRepository.GetAccountInfoByName(searchData);

                    if (accountInfo != null)
                    {
                        accountID = accountInfo.AccountId;
                    }
                    else
                    {
                        throw new Exception(localizationService.GetString("MessageCharacterNameNotExist"));
                    }
                }
                else
                {
                    if (!long.TryParse(searchData, out accountID))
                    {
                        throw new Exception(localizationService.GetString("MessageCharacterIdParseFaile"));
                    }

                }
            }

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

            return billingRepository.GetBillHistories(accountID, startDate.ToUniversalTime(), endDate.ToUniversalTime());
        }
    }
}
