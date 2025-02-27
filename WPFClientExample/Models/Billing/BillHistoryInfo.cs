using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.Billing
{
    public class BillHistoryInfo
    {
        public long BillTxId { get; set; }
        public BILL_TX_TYPES BillTxType { get; set; }
        public BILL_TX_STATUS BillTxStatus { get; set; }
        public string? PurchaseToken { get; set; }
        public bool IsDone { get; set; }
        public long ProductId { get; set; }
        public long AccountId { get; set; }
        public long? CharId { get; set; }
        public string? CharName { get; set; }
        public BILL_PRODUCT_TYPE BillProductType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public required string ProductKey { get; set; }
        public required string ProductName { get; set; }
        public required int Price { get; set; }
        public PRODUCT_TYPE ProductTypes { get; set; }
        public long GameItemId { get; set; }
        public int GameItemVolume { get; set; }
        public required string ItemName { get; set; }
        public ITEM_GRADE Grade { get; set; }
        public ITEM_TYPE ItemType { get; set; }
    }
}
