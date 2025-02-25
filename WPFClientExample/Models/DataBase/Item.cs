using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Item
    {
        public long Id { get; set; }

        public required string ItemName { get; set; }

        public int Grade { get; set; }

        public int ItemType { get; set; }

        public bool IsUse { get; set;  }

        public DateTime CreateDate { get; set; }

    }
}
