namespace WPFClientExample.Commons.Enums
{
    public enum BILL_TX_TYPES
    {
        WEB_PG,
        IAP_GOOGLE,
        IAP_IOS,
        POINT,
    }

    public enum BILL_TX_STATUS
    {
        INITIATED,
        COMPLETED,
        VALIDATE_START,
        DELIVERY_FAILED,
        POINT_CHARGE_START,
        POINT_CHARGE_END,
        POINT_CHARGE_FAILED,
        POINT_SPEND_START,
        POINT_SPEND_END,
        POINT_SPEND_FAILED,
        IAP_RECEIPT_PENDING,
        IAP_RECEIPT_VALID,
        IAP_RECEIPT_INVALID,
        REFUNDED,
        CANCELED,
        EXPIRED,
    }

    // 상품 타입
    public enum BILL_PRODUCT_TYPE
    {
        CONSUMABLE,
        NON_CONSUMABLE,
        SUBSCRIPTION_AUTO,
        SUBSCRIPTION_NON_AUTO,
        REFUND,
    }

    public enum PRODUCT_TYPE
    {
        POINT,
        ACCOUNT_ITEM,
        CHARACTER_ITEM,
    }
}
