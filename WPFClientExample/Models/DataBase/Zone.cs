using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Zone
    {
        public long Id { get; set; }

        public required string ZoneName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
