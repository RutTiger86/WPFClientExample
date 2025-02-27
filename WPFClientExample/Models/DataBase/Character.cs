namespace WPFClientExample.Models.DataBase
{
    public class Character
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public required string CharacterName { get; set; }

        public int ServerID { get; set; }

        public int Class { get; set; }

        public int Race { get; set; }

        public int Level { get; set; }

        public int GuildId { get; set; }

        public int ZoneId { get; set; }

        public int CurrentChannel { get; set; }

        public bool IsPvPMode { get; set; }

        public long PartyId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
