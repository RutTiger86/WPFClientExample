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
        public List<CharacterInfo> GetCharacterInfoList(UserSearchType searchType, long accountID, string accountName);

    }

    public class UserReository:IUserRepository
    {
        private List<Account>? TestAccountList;
        private List<Character>? TestCharacterInfo;
        private List<Server>? TestServerInfo;
        
        public UserReository()
        {
            InitTestData();
        }

        public void InitTestData()
        {
            TestAccountList =
                [
                    new Account()
                    {
                        Id = 12345678,
                        AccountName = "TestAccount",
                        CreateDate = DateTime.Now,
                        IsDeleted = false
                    }
                ];

            TestCharacterInfo =
                [
                    new Character()
                    {
                        Id = 1,
                        CharacterName = "Character1",
                        AccountId = 12345678,
                        CombatLevel = 99,
                        CreateDate = DateTime.Now,
                        Race = 1,
                        ServerID = 1                        
                    },
                    new Character()
                    {
                        Id = 2,
                        CharacterName = "Character2",
                        AccountId = 12345678,
                        CombatLevel = 25,
                        CreateDate = DateTime.Now,
                        Race = 2,
                        ServerID = 1
                    },
                    new Character()
                    {
                        Id = 3,
                        CharacterName = "Character3",
                        AccountId = 12345678,
                        CombatLevel = 1,
                        CreateDate = DateTime.Now,
                        Race = 3,
                        ServerID = 2
                    }
                ];
            TestServerInfo =
                [
                    new Server()
                    {
                        ServerId = 1,
                        ServerName = "1 Server",
                        IsLive = true,
                        CreateDate = DateTime.Now
                    },
                    new Server()
                    {
                        ServerId = 2,
                        ServerName = "2 Server",
                        IsLive = true,
                        CreateDate = DateTime.Now
                    }
                ];
        }

        public List<CharacterInfo> GetCharacterInfoList(UserSearchType searchType, long accountID, string accountName)
        {
            IEnumerable<Account>? accounts;
            if ( searchType == UserSearchType.AccountId)
            {
                accounts = TestAccountList?.Where(p => p.Id == accountID);
            }
            else
            {
                accounts = TestAccountList?.Where(p => p.AccountName.Equals(accountName));
            }

            return [.. (from ac in accounts
             join tc in TestCharacterInfo on ac.Id equals tc.AccountId
             join si in TestServerInfo on tc.ServerID equals si.ServerId
             select new CharacterInfo()
             {
                 AccountId = ac.Id,
                 CharacterId = tc.Id,
                 CharacterName = tc.CharacterName,
                 ServerId = si.ServerId,
                 ServerName = si.ServerName
             })];
            
        }
    }
}
