using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public partial class AuthAccount
    {
        public long Id { get; set; }
        public required string AuthId { get; set; }
        public required string Password { get; set; }
    }
}
