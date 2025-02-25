using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.GameLog
{
    public class CharacterEquipeedInfo
    {
        public long CharEquipId { get; set; }

        public EQUIP_SLOT_TYPE SlotType { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; } 
        public ITEM_GRADE ItemGrade { get; set; }
    }
}
