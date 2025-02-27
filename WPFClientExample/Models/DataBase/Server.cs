namespace WPFClientExample.Models.DataBase
{
    public class Server
    {
        public int Id { get; set; }
        public required string ServerName { get; set; }
        public bool IsLive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
