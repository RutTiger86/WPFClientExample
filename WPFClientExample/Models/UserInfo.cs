using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models
{
    public partial class UserInfo
    {
        public long Id { get; set; }
        public required string UserId { get; set; }
        public required string Password { get; set; }
    }
}
