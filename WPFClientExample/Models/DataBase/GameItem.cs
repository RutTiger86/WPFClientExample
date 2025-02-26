using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class GameItem
    {
        public long Id { get; set; }

        public required string ItemName { get; set; }
        /// <summary>
        /// 아이템 구매 타입 
        /// <see cref="ITEM_GRADE"/>
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 아이템 구매 타입 
        /// <see cref="ITEM_TYPE"/>
        /// </summary>
        public int ItemType { get; set; }

        public bool IsUse { get; set;  }

        public DateTime CreateDate { get; set; }

    }
}
