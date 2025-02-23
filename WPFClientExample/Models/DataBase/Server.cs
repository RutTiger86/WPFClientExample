using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Server
    {
        public int ServerId { get; set; }
        public required string ServerName { get; set; }
        public bool IsLive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
