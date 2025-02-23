using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.GameLog
{
    public class CharacterInfo
    {
        public long AccountId { get; set; }
        public int ServerId { get; set; }

        public required string ServerName { get; set; }

        public long CharacterId { get; set; }
        public required string CharacterName { get; set; }
    }
}
