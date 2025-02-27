using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.GameLog
{
    public class InventoryHistoryLogInfo
    {
        public long LogId { get; set; } // 로그 고유 ID
        public long CharacterId { get; set; } // 대상 캐릭터 ID
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // 변경 발생 시간
        public long ItemId { get; set; } // 변경된 아이템 ID
        public required string ItemName { get; set; } // 변경된 아이템 ID
        public int QuantityChange { get; set; } // 변화한 개수 (+추가, -삭제)
        public int BeforeQuantity { get; set; } // 변경 전 개수
        public int AfterQuantity { get; set; } // 변경 후 개수
        public INVENTORY_CHANGE_TYPE ChangeType { get; set; } // 변경 유형 (0 = 획득, 1 = 사용, 2 = 삭제, 3 = 거래)
    }
}
