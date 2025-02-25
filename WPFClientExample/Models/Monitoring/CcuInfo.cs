using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.Monitoring
{
    public class CcuInfo
    {
        public long ServerId { get; set; }

        public KeyValuePair<DateTime, int> CcuValue { get; set; }
    }
}
