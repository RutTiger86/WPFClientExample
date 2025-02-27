using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Statics;
using WPFClientExample.Models.Billing;

namespace WPFClientExample.Repositories
{
    public interface IBillingRepository
    {
        public List<BillHistoryInfo> GetBillHistories(long accountID, DateTime startDate, DateTime endDate);
    }

    public class BillingReposiotry : IBillingRepository
    {
        public List<BillHistoryInfo> GetBillHistories(long accountID, DateTime startDate, DateTime endDate)
        {
            return (from tb in TestDataFactory.TestBillTxs
                    join tbd in TestDataFactory.TestBillDetails on tb.Id equals tbd.BillTxId
                    join tp in TestDataFactory.TestProducts on tbd.ProductId equals tp.Id
                    join tpgi in TestDataFactory.TestProductGameItems on tp.Id equals tpgi.ProductId
                    join gi in TestDataFactory.TestGameItems on tpgi.GameItemId equals gi.Id
                    join ta in TestDataFactory.TestAccounts on tbd.AccountId equals ta.Id
                    join tc in TestDataFactory.TestCharacters on tbd.CharId equals tc.Id into tcJoin
                    from tc in tcJoin.DefaultIfEmpty()
                    where tbd.CreateDate >= startDate
                    && tbd.CreateDate <= endDate
                    && (accountID == 0 || ta.Id == accountID)
                    select new BillHistoryInfo()
                    {
                        AccountId = ta.Id,
                        BillProductType = (BILL_PRODUCT_TYPE)tbd.BillProductType,
                        BillTxId = tb.Id,
                        BillTxStatus = (BILL_TX_STATUS)tb.TxStatus,
                        BillTxType = (BILL_TX_TYPES)tb.TxType,
                        CharId = tc?.Id,
                        CharName = tc?.CharacterName,
                        CreateDate = tbd.CreateDate,
                        GameItemId = tpgi.GameItemId,
                        GameItemVolume = tpgi.GameItemVolume,
                        Grade = (ITEM_GRADE)gi.Grade,
                        IsDone = tb.IsDone,
                        ItemName = gi.ItemName,
                        ItemType = (ITEM_TYPE)gi.ItemType,
                        Price = tp.Price,
                        ProductId = tp.Id,
                        ProductKey = tp.ProductKey,
                        ProductName = tp.ProductName,
                        ProductTypes = (PRODUCT_TYPE)tpgi.ProductTypes,
                        PurchaseToken = tb.PurchaseToken,
                        UpdateDate = tbd.UpdateDate
                    }).ToList();
        }
    }
}
