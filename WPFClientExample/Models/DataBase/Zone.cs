namespace WPFClientExample.Models.DataBase
{
    public class Zone
    {
        public long Id { get; set; }

        public required string ZoneName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
