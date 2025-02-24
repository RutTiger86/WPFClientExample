using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;

namespace WPFClientExample.Models.GameLog
{
    public class AccountInfo
    {
        public long AccountId { get; set; }
        public required string AccountName { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public DateTime LastLoginTime { get; set; }

        public bool IsOnLine { get; set; }

        public string? LastLoginIP { get; set; }

        public string? LastLocation { get; set; }

        public TimeSpan TotalPlayTime { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
