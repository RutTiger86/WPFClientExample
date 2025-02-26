using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models.Billing;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Repositories;

namespace WPFClientExample.Services
{
    public interface IBillingService
    {
        public Task<List<BillHistoryInfo>?> GetBillHistoryInfoAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate);
    }

    public class BillingService : IBillingService
    {
        private readonly IBillingRepository billingRepository;
        private readonly IUserRepository userRepository;

        public BillingService(IBillingRepository billingRepository, IUserRepository userRepository)
        {
            this.billingRepository = billingRepository;
            this.userRepository = userRepository;
        }

        public Task<List<BillHistoryInfo>?> GetBillHistoryInfoAsync(USER_SEARCH_TYPE searchType, string searchData, DateTime startDate, DateTime endDate)
        {
            long accountID = 0;

            if (endDate < startDate)
            {
                throw new Exception("Invalid date range setting");
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
                        throw new Exception("Character name that does not exist ");
                    }
                }
                else
                {
                    if (!long.TryParse(searchData, out accountID))
                    {
                        throw new Exception("Character ID can only be numbers.");
                    }

                }
            }

            return Task.FromResult(billingRepository.GetBillHistories(accountID, startDate.ToUniversalTime(), endDate.ToUniversalTime()));
        }
    }
}
