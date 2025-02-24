using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Guild
    {
        public long Id { get; set; }
        public required string GuildName { get; set; }
    }
}
