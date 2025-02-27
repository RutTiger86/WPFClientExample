namespace WPFClientExample.Models.Monitoring
{
    public class CcuInfo
    {
        public long ServerId { get; set; }

        public KeyValuePair<DateTime, int> CcuValue { get; set; }
    }
}
