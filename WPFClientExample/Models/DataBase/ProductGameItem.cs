using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.DataBase
{
    public class ProductGameItem
    {
        /// <summary>
        /// 상품별 아이템 ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 상품 Id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 아이템 구매 타입 
        /// <see cref="PRODUCT_TYPE"/>
        /// </summary>
        public int ProductTypes { get; set; }
        /// <summary>
        /// 아이템 Id
        /// </summary>
        public long GameItemId { get; set; }
        /// <summary>
        /// 지급 Item 수량 
        /// </summary>
        public int GameItemVolume { get; set; }
        /// <summary>
        /// 사용여부 
        /// </summary>
        public bool IsUse { get; set; }
        /// <summary>
        /// 등록일 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 변경일
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
