﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class CharacterEquippedItem
    {
        public long Id { get; set; }
        public long CharacterId { get; set; }
        public int SlotType { get; set; }
        public long? ItemId { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
