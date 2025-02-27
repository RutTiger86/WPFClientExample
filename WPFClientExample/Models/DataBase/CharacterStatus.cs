namespace WPFClientExample.Models.DataBase
{
    public class CharacterStatus
    {
        public long Id { get; set; }

        public long CharacterId { get; set; }

        public long AttackPower { get; set; }

        public long Defense { get; set; }

        public long MaginPower { get; set; }

        public long Accuracy { get; set; }

        public long HealthPoint { get; set; }

        public long ManaPoint { get; set; }

        public long Reputation { get; set; }

        public decimal Experience { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
