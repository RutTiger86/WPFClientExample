using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.DataBase
{
    public class BillTx
    {
        /// <summary>
        /// 트랜젝션 ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 트랜젝션 타입
        /// <see cref="BILL_TX_TYPES"/>
        /// </summary>
        public int TxType { get; set; }
        /// <summary>
        /// 트랜젝션 상태값
        /// <seealso cref="BILL_TX_STATUS"/>
        /// </summary>
        public int TxStatus { get; set; }
        /// <summary>
        /// 구매 토큰 
        /// </summary>
        public string? PurchaseToken { get; set; }
        /// <summary>
        /// 트랜젝션 종료 여부 
        /// </summary>
        public bool IsDone { get; set; }
        /// <summary>
        /// 생성일
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 변경일
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
