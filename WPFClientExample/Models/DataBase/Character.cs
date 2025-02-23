using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Character
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public required string CharacterName { get; set; }

        public int ServerID { get; set; }

        public int Race { get; set; }
        public int CombatLevel { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
