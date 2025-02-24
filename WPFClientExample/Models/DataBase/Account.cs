using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Account
    {
        public long Id { get; set; }
        public required string AccountName { get; set; }                

        public int AccountType { get; set; }

        public int AccountStatus { get; set; }

        public long LastLoginTime { get; set; }

        public bool IsOnLine { get; set; }

        public string? LastLoginIP { get; set; }

        public string? LastLocation { get; set; }

        public long TotalPlayTime { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
