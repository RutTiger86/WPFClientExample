namespace WPFClientExample.Models.DataBase
{
    public partial class AuthAccount
    {
        public long Id { get; set; }
        public required string AuthId { get; set; }
        public required string Password { get; set; }
    }
}
