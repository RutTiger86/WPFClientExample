using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;

namespace WPFClientExample.Repositories
{
    public interface IUserRepository
    {
        public AccountInfo? GetAccountInfo(UserSearchType searchType, long accountID, string accountName);
        public List<CharacterInfo> GetCharacterInfoList(long accountId);

        public CharacterDetailInfo? GetCharacterDetailInfo(long CharcterId);
    }

    public class UserReository:IUserRepository
    {
        private List<Account>? TestAccounts;
        private List<Character>? TestCharacters;
        private List<CharacterStatus>? TestCharacterStatuses;
        private List<Server>? TestServers;
        private List<Zone>? TestZones;
        private List<Guild>? TestGuilds;

        public UserReository()
        {
            InitTestData();
        }

        public void InitTestData()
        {
            TestAccounts =
                [
                    new Account()
                    {
                        Id = 12345678,
                        AccountName = "TestAccount",
                        AccountStatus = 0,
                        AccountType = 0,
                        IsOnLine = true,
                        LastLocation = "KR",
                        LastLoginIP = "127.0.0.1",
                        LastLoginTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                        TotalPlayTime = 150L * 24 * 60 * 60,
                        CreateDate = DateTime.Now,
                        IsDeleted = false
                    }
                ];

            TestCharacters =
                [
                    new Character()
                    {
                        Id = 1,
                        CharacterName = "Character1",
                        AccountId = 12345678,
                        Level = 99,
                        Class = 1,
                        CurrentChannel = 10,
                        GuildId = 1,
                        IsPvPMode = false,
                        PartyId = 1020,
                        ZoneId = 1,
                        CreateDate = DateTime.Now,
                        Race = 1,
                        ServerID = 1                       
                    },
                    new Character()
                    {
                        Id = 2,
                        CharacterName = "Character2",
                        AccountId = 12345678,
                        Level = 25,
                        Class = 2,
                        CurrentChannel =1,
                        GuildId = 0,
                        IsPvPMode = false,
                        PartyId = 0,
                        ZoneId =2,
                        CreateDate = DateTime.Now,
                        Race = 2,
                        ServerID = 1
                    },
                    new Character()
                    {
                        Id = 3,
                        CharacterName = "Character3",
                        AccountId = 12345678,
                        Level = 1,
                        Class = 3,
                        CurrentChannel = 1,
                        GuildId = 0,
                        IsPvPMode = false,
                        PartyId =0,
                        ZoneId = 1,
                        CreateDate = DateTime.Now,
                        Race = 3,
                        ServerID = 2
                    }
                ];

            TestCharacterStatuses =
                [
                    new CharacterStatus()
                    {
                        Id = 1,
                        CharacterId = 1,
                        Accuracy = 10,
                        AttackPower = 100,
                        Defense = 200,
                        MaginPower = 50,
                        Experience = 1245,
                        HealthPoint = 5000,
                        ManaPoint = 3000,
                        Reputation = 500,
                        CreateDate = DateTime.Now                    
                    },
                    new CharacterStatus()
                    {
                        Id = 2,
                        CharacterId = 2,
                        Accuracy = 20,
                        AttackPower = 200,
                        Defense = 300,
                        MaginPower = 150,
                        Experience = 2245,
                        HealthPoint = 2000,
                        ManaPoint = 1000,
                        Reputation = 1500,
                        CreateDate = DateTime.Now
                    },
                    new CharacterStatus()
                    {
                        Id = 3,
                        CharacterId = 3,
                        Accuracy = 30,
                        AttackPower = 300,
                        Defense = 500,
                        MaginPower = 30,
                        Experience = 1,
                        HealthPoint = 35000,
                        ManaPoint = 33000,
                        Reputation = 3500,
                        CreateDate = DateTime.Now
                    }
                ];

            TestServers =
                [
                    new Server()
                    {
                        Id = 1,
                        ServerName = "1 Server",
                        IsLive = true,
                        CreateDate = DateTime.Now
                    },
                    new Server()
                    {
                        Id = 2,
                        ServerName = "2 Server",
                        IsLive = true,
                        CreateDate = DateTime.Now
                    },
                    new Server()
                    {
                        Id = 3,
                        ServerName = "3 Server",
                        IsLive = true,
                        CreateDate = DateTime.Now
                    }
                    
                ];

            TestZones =
                [
                    new Zone()
                    {
                        Id = 1,
                        ZoneName = "1 Zone",
                        CreateDate = DateTime.Now
                    },
                    new Zone()
                    {
                        Id = 2,
                        ZoneName = "2 Zone",
                        CreateDate = DateTime.Now
                    },

                ];

            TestGuilds =
                [
                    new Guild()
                    {
                        Id  =0,
                        GuildName= "None"
                    },
                    new Guild()
                    {
                        Id  =1,
                        GuildName= "Guild 1"
                    },
                     new Guild()
                    {
                        Id  =2,
                        GuildName= "Guild 2"
                    }

                ];
        }

        public List<CharacterInfo> GetCharacterInfoList(long accountId)
        {
            return [.. (from ac in TestAccounts
             join tc in TestCharacters on ac.Id equals tc.AccountId
             join si in TestServers on tc.ServerID equals si.Id
             where ac.Id == accountId
             select new CharacterInfo()
             {
                 AccountId = ac.Id,
                 CharacterId = tc.Id,
                 CharacterName = tc.CharacterName,
                 ServerId = si.Id,
                 ServerName = si.ServerName
             })];
            
        }

        public AccountInfo? GetAccountInfo(UserSearchType searchType, long accountID, string accountName)
        {
            IEnumerable<Account>? accounts;
            if (searchType == UserSearchType.AccountId)
            {
                accounts = TestAccounts?.Where(p => p.Id == accountID);
            }
            else
            {
                accounts = TestAccounts?.Where(p => p.AccountName.Equals(accountName));
            }

            return (from ac in accounts
                    join tc in TestCharacters on ac.Id equals tc.AccountId
                    join si in TestServers on tc.ServerID equals si.Id
                    select new AccountInfo()
                    {
                        AccountId = ac.Id,
                        AccountName = ac.AccountName,
                        AccountStatus = (AccountStatus)ac.AccountStatus,
                        CreateDate = ac.CreateDate,
                        IsOnLine = ac.IsOnLine,
                        LastLocation = ac.LastLocation,
                        LastLoginIP = ac.LastLoginIP,
                        LastLoginTime = DateTimeOffset.FromUnixTimeSeconds(ac.LastLoginTime).UtcDateTime,
                        TotalPlayTime = TimeSpan.FromSeconds(ac.TotalPlayTime)
                    }).FirstOrDefault();
        }

        public CharacterDetailInfo? GetCharacterDetailInfo(long CharcterId)
        {
            return (from tc in TestCharacters
                    join si in TestServers on tc.ServerID equals si.Id
                    join cs in TestCharacterStatuses on tc.Id equals cs.CharacterId
                    join zn in TestZones on tc.ZoneId equals zn.Id
                    join gd in TestGuilds on tc.GuildId equals gd.Id
                    where tc.Id == CharcterId
                    select new CharacterDetailInfo()
                    {
                        Accuracy = cs.Accuracy,
                        AttackPower = cs.AttackPower,
                        CharacterClass = (CharacterClass) tc.Class,
                        CharacterId = tc.Id,
                        CharacterLevel = tc.Level,
                        CharacterName = tc.CharacterName,
                        CharacterRace = (CharacterRace) tc.Race,
                        CurrentChannel = tc.CurrentChannel,
                        Defense = cs.Defense,
                        Health = cs.HealthPoint,
                        MagicPower = cs.MaginPower,
                        Mana = cs.ManaPoint,
                        PartyId = tc.PartyId,
                        PvpMode = tc.IsPvPMode,
                        Requtation = cs.Reputation,
                        ServerId = tc.ServerID,
                        ServerName = si.ServerName,
                        TotalExperience = cs.Experience,
                        ZoneName = zn.ZoneName,
                        GuildName = gd.GuildName                                               
                    }).FirstOrDefault();
        }
    }
}
