using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Models;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;

namespace WPFClientExample.Repositories
{
    public interface IUserRepository
    {
        public AccountInfo? GetAccountInfo(USER_SEARCH_TYPE searchType, long accountID, string accountName);
        public List<CharacterInfo> GetCharacterInfoList(long accountId);
        public CharacterDetailInfo? GetCharacterDetailInfo(long characterId);
        public List<CharacterEquipeedInfo>? GetCharacterEquipeedInfo(long characterId);

        public List<ChatLogInfo>? GetChatLogInfosByCharacterId(long characterId);

        public List<CharacterQuestInfo>? GetCharacterQuestInfoByCharacterId(long characterId);
    }

    public class UserReository : IUserRepository
    {
        private List<Account>? TestAccounts;
        private List<Character>? TestCharacters;
        private List<CharacterStatus>? TestCharacterStatuses;
        private List<Server>? TestServers;
        private List<Zone>? TestZones;
        private List<Guild>? TestGuilds;
        private List<Item>? TestItems;
        private List<CharacterEquippedItem>? TestCharEquipItems;
        private List<ChatLog> TestChatlogs;
        private List<Quest> TestQuests;
        private List<CharacterQuestProgress> TestCharacterQuests;

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

            TestItems =
                [
                    new Item()
                    {
                        Id = 1,
                        ItemName = "Iron Helmet",
                        CreateDate = DateTime.Now,
                        Grade = 2, // RARE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 2,
                        ItemName = "Steel Chestplate",
                        CreateDate = DateTime.Now,
                        Grade = 3, // UNIQUE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 3,
                        ItemName = "Leather Pants",
                        CreateDate = DateTime.Now,
                        Grade = 1, // NORMAL
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 4,
                        ItemName = "Golden Gloves",
                        CreateDate = DateTime.Now,
                        Grade = 4, // LEGENDARY
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 5,
                        ItemName = "Shadow Boots",
                        CreateDate = DateTime.Now,
                        Grade = 3, // UNIQUE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 6,
                        ItemName = "Ruby Ring",
                        CreateDate = DateTime.Now,
                        Grade = 2, // RARE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 7,
                        ItemName = "Sapphire Ring",
                        CreateDate = DateTime.Now,
                        Grade = 2, // RARE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 8,
                        ItemName = "Diamond Earring",
                        CreateDate = DateTime.Now,
                        Grade = 3, // UNIQUE
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 9,
                        ItemName = "Flaming Sword",
                        CreateDate = DateTime.Now,
                        Grade = 4, // MYTHIC
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    },
                    new Item()
                    {
                        Id = 10,
                        ItemName = "Crystal Shield",
                        CreateDate = DateTime.Now,
                        Grade = 4, // LEGENDARY
                        IsUse = true,
                        ItemType = 1 // EQUIPMENT
                    }
                ];
            
            TestCharEquipItems =
                [
                    // Character 1 (모든 부위 장착)
                    new CharacterEquippedItem()
                    {
                        Id = 1,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 1, // Iron Helmet
                        SlotType = 0, // HEAD
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 2,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 2, // Steel Chestplate
                        SlotType = 1, // CHEST
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 3,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 3, // Leather Pants
                        SlotType = 2, // LEGS
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 4,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 4, // Golden Gloves
                        SlotType = 3, // GLOVES
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 5,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 5, // Shadow Boots
                        SlotType = 4, // BOOTS
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 6,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 9, // Flaming Sword
                        SlotType = 8, // MAIN_HAND_WEAPON
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 7,
                        CharacterId = 1,
                        CreateDate = DateTime.Now,
                        ItemId = 10, // Crystal Shield
                        SlotType = 9, // OFF_HAND_WEAPON
                        UpdateTime = DateTime.Now
                    },
            
                    // Character 2 (몇 개만 장착)
                    new CharacterEquippedItem()
                    {
                        Id = 8,
                        CharacterId = 2,
                        CreateDate = DateTime.Now,
                        ItemId = 1, // Iron Helmet
                        SlotType = 0, // HEAD
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 9,
                        CharacterId = 2,
                        CreateDate = DateTime.Now,
                        ItemId = 2, // Steel Chestplate
                        SlotType = 1, // CHEST
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 10,
                        CharacterId = 2,
                        CreateDate = DateTime.Now,
                        ItemId = 7, // Sapphire Ring
                        SlotType = 6, // RING_2
                        UpdateTime = DateTime.Now
                    },
            
                    // Character 3 (반지만 장착)
                    new CharacterEquippedItem()
                    {
                        Id = 11,
                        CharacterId = 3,
                        CreateDate = DateTime.Now,
                        ItemId = 6, // Ruby Ring
                        SlotType = 5, // RING_1
                        UpdateTime = DateTime.Now
                    },
                    new CharacterEquippedItem()
                    {
                        Id = 12,
                        CharacterId = 3,
                        CreateDate = DateTime.Now,
                        ItemId = 7, // Sapphire Ring
                        SlotType = 6, // RING_2
                        UpdateTime = DateTime.Now
                    }
                ];

            TestChatlogs =
                [
                    new ChatLog { Id = 1, ChatType = 0, ChatRoomId = 0, SenderCharacterId = 1, SenderName = "Character1", Message = "Hello everyone!", SentTime = DateTime.UtcNow.AddMinutes(-1) },
                    new ChatLog { Id = 2, ChatType = 1, ChatRoomId = 1001, SenderCharacterId = 2, SenderName = "Character2", Message = "Guild meeting at 8 PM.", SentTime = DateTime.UtcNow.AddMinutes(-2) },
                    new ChatLog { Id = 3, ChatType = 2, ChatRoomId = 2001, SenderCharacterId = 3, SenderName = "Character3", Message = "Let's defeat the dungeon boss!", SentTime = DateTime.UtcNow.AddMinutes(-3) },
                    new ChatLog { Id = 4, ChatType = 3, ChatRoomId = 0, SenderCharacterId = 1, SenderName = "Character1", ReceiverCharacterId = 2, ReceiverName = "Character2", Message = "Do you have a health potion?", SentTime = DateTime.UtcNow.AddMinutes(-4) },
                    new ChatLog { Id = 5, ChatType = 4, ChatRoomId = 0, SenderCharacterId = 2, SenderName = "Character2", Message = "The town looks amazing!", SentTime = DateTime.UtcNow.AddMinutes(-5) },
                    new ChatLog { Id = 6, ChatType = 0, ChatRoomId = 0, SenderCharacterId = 3, SenderName = "Character3", Message = "Looking for party members!", SentTime = DateTime.UtcNow.AddMinutes(-6) },
                    new ChatLog { Id = 7, ChatType = 1, ChatRoomId = 1001, SenderCharacterId = 1, SenderName = "Character1", Message = "Our guild is the best!", SentTime = DateTime.UtcNow.AddMinutes(-7) },
                    new ChatLog { Id = 8, ChatType = 2, ChatRoomId = 2001, SenderCharacterId = 2, SenderName = "Character2", Message = "Need a healer for our team.", SentTime = DateTime.UtcNow.AddMinutes(-8) },
                    new ChatLog { Id = 9, ChatType = 3, ChatRoomId = 0, SenderCharacterId = 3, SenderName = "Character3", ReceiverCharacterId = 1, ReceiverName = "Character1", Message = "Trade me your sword?", SentTime = DateTime.UtcNow.AddMinutes(-9) },
                    new ChatLog { Id = 10, ChatType = 4, ChatRoomId = 0, SenderCharacterId = 1, SenderName = "Character1", Message = "Who wants to join my raid group?", SentTime = DateTime.UtcNow.AddMinutes(-10) },
                    new ChatLog { Id = 11, ChatType = 0, ChatRoomId = 0, SenderCharacterId = 2, SenderName = "Character2", Message = "Server event is live now!", SentTime = DateTime.UtcNow.AddMinutes(-11) },
                    new ChatLog { Id = 12, ChatType = 1, ChatRoomId = 1001, SenderCharacterId = 3, SenderName = "Character3", Message = "Prepare for the upcoming war.", SentTime = DateTime.UtcNow.AddMinutes(-12) },
                    new ChatLog { Id = 13, ChatType = 2, ChatRoomId = 2001, SenderCharacterId = 1, SenderName = "Character1", Message = "We need a strong tank.", SentTime = DateTime.UtcNow.AddMinutes(-13) },
                    new ChatLog { Id = 14, ChatType = 3, ChatRoomId = 0, SenderCharacterId = 2, SenderName = "Character2", ReceiverCharacterId = 3, ReceiverName = "Character3", Message = "Let's meet at the marketplace.", SentTime = DateTime.UtcNow.AddMinutes(-14) },
                    new ChatLog { Id = 15, ChatType = 4, ChatRoomId = 0, SenderCharacterId = 3, SenderName = "Character3", Message = "Anyone selling magic scrolls?", SentTime = DateTime.UtcNow.AddMinutes(-15) },
                    new ChatLog { Id = 16, ChatType = 0, ChatRoomId = 0, SenderCharacterId = 1, SenderName = "Character1", Message = "Great battle everyone!", SentTime = DateTime.UtcNow.AddMinutes(-16) },
                    new ChatLog { Id = 17, ChatType = 1, ChatRoomId = 1001, SenderCharacterId = 2, SenderName = "Character2", Message = "We need more members!", SentTime = DateTime.UtcNow.AddMinutes(-17) },
                    new ChatLog { Id = 18, ChatType = 2, ChatRoomId = 2001, SenderCharacterId = 3, SenderName = "Character3", Message = "This dungeon is hard!", SentTime = DateTime.UtcNow.AddMinutes(-18) },
                    new ChatLog { Id = 19, ChatType = 3, ChatRoomId = 0, SenderCharacterId = 1, SenderName = "Character1", ReceiverCharacterId = 2, ReceiverName = "Character2", Message = "Can you lend me some gold?", SentTime = DateTime.UtcNow.AddMinutes(-19) },
                    new ChatLog { Id = 20, ChatType = 4, ChatRoomId = 0, SenderCharacterId = 2, SenderName = "Character2", Message = "The blacksmith is crafting rare weapons!", SentTime = DateTime.UtcNow.AddMinutes(-20) }
                ];

            TestQuests =
                [
                    new Quest { Id = 1001, QuestName = "Defeat the Goblin Leader", Description = "Eliminate the Goblin Leader causing trouble in the village.", QuestType = 0, ObjectiveType = 0, ObjectiveTargetId = 5001, ObjectiveCount = 1, RewardExp = 500, RewardGold = 1000, RewardItemId = 2001 },
                    new Quest { Id = 1002, QuestName = "Collect Healing Herbs", Description = "Gather 5 Healing Herbs from the forest.", QuestType = 1, ObjectiveType = 1, ObjectiveTargetId = 6001, ObjectiveCount = 5, RewardExp = 200, RewardGold = 500, RewardItemId = null },
                    new Quest { Id = 1003, QuestName = "Talk to the Village Elder", Description = "Meet with the Village Elder for further instructions.", QuestType = 0, ObjectiveType = 2, ObjectiveTargetId = 7001, ObjectiveCount = 1, RewardExp = 300, RewardGold = 0, RewardItemId = null },
                    new Quest { Id = 1004, QuestName = "Hunt 10 Wolves", Description = "The village needs protection. Hunt 10 wolves in the nearby forest.", QuestType = 1, ObjectiveType = 0, ObjectiveTargetId = 5002, ObjectiveCount = 10, RewardExp = 800, RewardGold = 1500, RewardItemId = 2002 },
                    new Quest { Id = 1005, QuestName = "Retrieve the Lost Artifact", Description = "Find and retrieve the lost artifact from the ancient ruins.", QuestType = 2, ObjectiveType = 1, ObjectiveTargetId = 6002, ObjectiveCount = 1, RewardExp = 1000, RewardGold = 2000, RewardItemId = 2003 },
                    new Quest { Id = 1006, QuestName = "Escort the Merchant", Description = "Protect the merchant on his journey to the next town.", QuestType = 3, ObjectiveType = 2, ObjectiveTargetId = 7002, ObjectiveCount = 1, RewardExp = 700, RewardGold = 1200, RewardItemId = null },
                    new Quest { Id = 1007, QuestName = "Defeat the Bandit Chief", Description = "Take down the leader of the bandits terrorizing the roads.", QuestType = 0, ObjectiveType = 0, ObjectiveTargetId = 5003, ObjectiveCount = 1, RewardExp = 1200, RewardGold = 2500, RewardItemId = 2004 },
                    new Quest { Id = 1008, QuestName = "Gather 10 Magic Crystals", Description = "Collect magic crystals scattered around the dungeon.", QuestType = 2, ObjectiveType = 1, ObjectiveTargetId = 6003, ObjectiveCount = 10, RewardExp = 900, RewardGold = 1800, RewardItemId = 2005 },
                    new Quest { Id = 1009, QuestName = "Deliver the Secret Message", Description = "Deliver a confidential message to the castle.", QuestType = 1, ObjectiveType = 2, ObjectiveTargetId = 7003, ObjectiveCount = 1, RewardExp = 400, RewardGold = 800, RewardItemId = null }
                ];

            TestCharacterQuests =
                [
                    // Character 1
                    new CharacterQuestProgress { Id = 1, CharacterId = 1, QuestId = 1001, QuestStatus = (int)QUEST_STATUES.IN_PROGRESS, CurrentStep = 1, CurrentCount = 0, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-2) },
                    new CharacterQuestProgress { Id = 2, CharacterId = 1, QuestId = 1003, QuestStatus = (int)QUEST_STATUES.COMPLETED, CurrentStep = 1, CurrentCount = 1, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-3), CompletionTime = DateTime.UtcNow.AddHours(-1) },
                    new CharacterQuestProgress { Id = 3, CharacterId = 1, QuestId = 1006, QuestStatus = (int)QUEST_STATUES.CANCLE, CurrentStep = 1, CurrentCount = 0, IsCancled = true, StartTime = DateTime.UtcNow.AddHours(-4), CancledTime = DateTime.UtcNow.AddHours(-2) },

                    // Character 2
                    new CharacterQuestProgress { Id = 4, CharacterId = 2, QuestId = 1002, QuestStatus = (int)QUEST_STATUES.IN_PROGRESS, CurrentStep = 1, CurrentCount = 3, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-1) },
                    new CharacterQuestProgress { Id = 5, CharacterId = 2, QuestId = 1004, QuestStatus = (int)QUEST_STATUES.COMPLETED, CurrentStep = 1, CurrentCount = 10, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-5), CompletionTime = DateTime.UtcNow.AddHours(-2) },
                    new CharacterQuestProgress { Id = 6, CharacterId = 2, QuestId = 1007, QuestStatus = (int)QUEST_STATUES.FAILED, CurrentStep = 1, CurrentCount = 0, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-6) },

                    // Character 3
                    new CharacterQuestProgress { Id = 7, CharacterId = 3, QuestId = 1005, QuestStatus = (int)QUEST_STATUES.IN_PROGRESS, CurrentStep = 1, CurrentCount = 0, IsCancled = false, StartTime = DateTime.UtcNow.AddMinutes(-45) },
                    new CharacterQuestProgress { Id = 8, CharacterId = 3, QuestId = 1008, QuestStatus = (int)QUEST_STATUES.IN_PROGRESS, CurrentStep = 1, CurrentCount = 5, IsCancled = false, StartTime = DateTime.UtcNow.AddMinutes(-90) },
                    new CharacterQuestProgress { Id = 9, CharacterId = 3, QuestId = 1009, QuestStatus = (int)QUEST_STATUES.COMPLETED, CurrentStep = 1, CurrentCount = 1, IsCancled = false, StartTime = DateTime.UtcNow.AddHours(-4), CompletionTime = DateTime.UtcNow.AddMinutes(-30) }
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

        public AccountInfo? GetAccountInfo(USER_SEARCH_TYPE searchType, long accountID, string accountName)
        {
            IEnumerable<Account>? accounts;
            if (searchType == USER_SEARCH_TYPE.AccountId)
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
                        AccountStatus = (ACCOUNT_STATE)ac.AccountStatus,
                        CreateDate = ac.CreateDate,
                        IsOnLine = ac.IsOnLine,
                        LastLocation = ac.LastLocation,
                        LastLoginIP = ac.LastLoginIP,
                        LastLoginTime = DateTimeOffset.FromUnixTimeSeconds(ac.LastLoginTime).UtcDateTime,
                        TotalPlayTime = TimeSpan.FromSeconds(ac.TotalPlayTime)
                    }).FirstOrDefault();
        }

        public CharacterDetailInfo? GetCharacterDetailInfo(long charcterId)
        {
            return (from tc in TestCharacters
                    join si in TestServers on tc.ServerID equals si.Id
                    join cs in TestCharacterStatuses on tc.Id equals cs.CharacterId
                    join zn in TestZones on tc.ZoneId equals zn.Id
                    join gd in TestGuilds on tc.GuildId equals gd.Id
                    where tc.Id == charcterId
                    select new CharacterDetailInfo()
                    {
                        Accuracy = cs.Accuracy,
                        AttackPower = cs.AttackPower,
                        CharacterClass = (CHARACTER_CLASS)tc.Class,
                        CharacterId = tc.Id,
                        CharacterLevel = tc.Level,
                        CharacterName = tc.CharacterName,
                        CharacterRace = (CHARACTER_RACE)tc.Race,
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

        public List<CharacterEquipeedInfo>? GetCharacterEquipeedInfo(long characterId)
        {
            return (from tce in TestCharEquipItems
                    join ti in TestItems on tce.ItemId equals ti.Id
                    where tce.CharacterId == characterId
                    select new CharacterEquipeedInfo()
                    {
                        CharEquipId = tce.Id,
                        ItemGrade = (ITEM_GRADE) ti.Grade,
                        ItemId = tce.ItemId,
                        ItemName = ti.ItemName,
                        SlotType = (EQUIP_SLOT_TYPE)tce.SlotType,
                    }).ToList();
        }


        public List<ChatLogInfo>? GetChatLogInfosByCharacterId(long characterId)
        {
            return (from tcl in TestChatlogs
                    where tcl.SenderCharacterId == characterId
                    select new ChatLogInfo()
                    {
                        Id = tcl.Id,
                        ChatRoomId = tcl.ChatRoomId,
                        Message = tcl.Message,
                        ChatType = (CHAT_TYPE)tcl.ChatType,
                        ReceiverCharacterId = tcl.ReceiverCharacterId,
                        ReceiverName = tcl.ReceiverName,
                        SenderCharacterId = tcl.SenderCharacterId,
                        SenderName = tcl.SenderName,
                        SentTime = tcl.SentTime
                    }).ToList();
        }

        public List<CharacterQuestInfo>? GetCharacterQuestInfoByCharacterId(long characterId)
        {
            return (from cq in TestCharacterQuests
                    join q in TestQuests on cq.QuestId equals q.Id
                    where cq.CharacterId == characterId
                    select new CharacterQuestInfo()
                    {
                        QuestId = cq.Id,
                        CharacterId = cq.CharacterId,
                        CancledTime = cq.CancledTime,
                        CompletionTime = cq.CompletionTime,
                        CurrentCount = cq.CurrentCount,
                        CurrentStep = cq.CurrentStep,
                        IsCancled = cq.IsCancled,
                        QuestName = q.QuestName,
                        QuestStatus = (QUEST_STATUES) cq.QuestStatus,
                        QuestType = (QUEST_TYPE) q.QuestType,
                        StartTime = cq.StartTime
                    }).ToList();
        }
    }
}
