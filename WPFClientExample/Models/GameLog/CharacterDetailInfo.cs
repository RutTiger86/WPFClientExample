using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.GameLog
{
    public class CharacterDetailInfo
    {
        public int ServerId { get; set; }

        public required string ServerName { get; set; }

        public long CharacterId { get; set; }
        public required string CharacterName { get; set; }

        public int CharacterLevel { get; set; }

        public CHARACTER_CLASS CharacterClass { get; set; }

        public CHARACTER_RACE CharacterRace { get; set; }
        
        public long AttackPower { get; set; }

        public long Defense { get; set; }

        public long MagicPower { get; set; }

        public long Accuracy { get; set; }

        public long Health { get; set; }

        public long Mana { get; set; }

        public long Requtation { get; set; }

        public decimal TotalExperience { get; set; }

        public string? ZoneName { get; set; }

        public long PartyId { get; set; }

        public bool PvpMode { get; set; }

        public int CurrentChannel { get; set; }

        public string? GuildName { get; set; }

    }
}
