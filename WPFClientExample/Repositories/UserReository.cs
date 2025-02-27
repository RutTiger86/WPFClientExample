using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Enums;
using WPFClientExample.Commons.Statics;
using WPFClientExample.Models;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;

namespace WPFClientExample.Repositories
{
    public interface IUserRepository
    {
        public AccountInfo? GetAccountInfo( long accountID);
        public AccountInfo? GetAccountInfoByName(string accountName);
        public List<CharacterInfo> GetCharacterInfoList(long accountId);
        public CharacterInfo? GetCharacterInfoByCharacterName(string charName);
        public CharacterDetailInfo? GetCharacterDetailInfo(long characterId);
        public List<CharacterEquipeedInfo>? GetCharacterEquipeedInfo(long characterId);
        public List<ChatLogInfo>? GetChatLogInfosByCharacterId(long characterId);
        public List<ChatLogInfo>? GetChatLogInfosByCharacterId(long characterId, DateTime startDate, DateTime endDate);
        public List<CharacterQuestInfo>? GetCharacterQuestInfoByCharacterId(long characterId);
        public List<InventoryHistoryLogInfo>? GetInventoryHistoryLog(long characterId, DateTime startDate, DateTime endDate);
    }

    public class UserReository : IUserRepository
    {
        public List<CharacterInfo> GetCharacterInfoList(long accountId)
        {
            return [.. (from ac in TestDataFactory.TestAccounts
             join tc in TestDataFactory.TestCharacters on ac.Id equals tc.AccountId
             join si in TestDataFactory.TestServers on tc.ServerID equals si.Id
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

        public CharacterInfo? GetCharacterInfoByCharacterName(string charName)
        {
            return (from tc in TestDataFactory.TestCharacters 
                    join si in TestDataFactory.TestServers on tc.ServerID equals si.Id
                    where tc.CharacterName == charName
                    select new CharacterInfo()
                    {
                        AccountId = tc.AccountId,
                        CharacterId = tc.Id,
                        CharacterName = tc.CharacterName,
                        ServerId = si.Id,
                        ServerName = si.ServerName
                    }).FirstOrDefault();
        }

        public AccountInfo? GetAccountInfo(long accountID)
        {
            return (from ac in TestDataFactory.TestAccounts
                    where ac.Id == accountID
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

        public AccountInfo? GetAccountInfoByName(string accountName)
        {
            return (from ac in TestDataFactory.TestAccounts
                    where ac.AccountName.Equals(accountName)
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
            return (from tc in TestDataFactory.TestCharacters
                    join si in TestDataFactory.TestServers on tc.ServerID equals si.Id
                    join cs in TestDataFactory.TestCharacterStatuses on tc.Id equals cs.CharacterId
                    join zn in TestDataFactory.TestZones on tc.ZoneId equals zn.Id
                    join gd in TestDataFactory.TestGuilds on tc.GuildId equals gd.Id
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
            return (from tce in TestDataFactory.TestCharEquipItems
                    join ti in TestDataFactory.TestGameItems on tce.ItemId equals ti.Id
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
            return (from tcl in TestDataFactory.TestChatlogs
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
            return (from cq in TestDataFactory.TestCharacterQuests
                    join q in TestDataFactory.TestQuests on cq.QuestId equals q.Id
                    where cq.CharacterId == characterId
                    select new CharacterQuestInfo()
                    {
                        QuestId = cq.Id,
                        CharacterId = cq.CharacterId,
                        CanceledTime = cq.CanceledTime,
                        CompletionTime = cq.CompletionTime,
                        CurrentCount = cq.CurrentCount,
                        CurrentStep = cq.CurrentStep,
                        IsCanceled = cq.IsCanceled,
                        QuestName = q.QuestName,
                        QuestStatus = (QUEST_STATUES) cq.QuestStatus,
                        QuestType = (QUEST_TYPE) q.QuestType,
                        StartTime = cq.StartTime
                    }).ToList();
        }

        public List<InventoryHistoryLogInfo>? GetInventoryHistoryLog(long characterId, DateTime startDate, DateTime endDate)
        {

            return (from tih in TestDataFactory.TestInventoryHistoryLogs
                    where tih.CharacterId == characterId
                    && (tih.Timestamp >=startDate)
                    && (tih.Timestamp <= endDate)
                    select new InventoryHistoryLogInfo()
                    {
                        LogId = tih.Id,
                        ItemName = tih.ItemName,
                        AfterQuantity = tih.AfterQuantity,
                        Timestamp = tih.Timestamp,
                        BeforeQuantity = tih.BeforeQuantity,
                        ChangeType = (INVENTORY_CHANGE_TYPE)tih.ChangeType,
                        CharacterId = tih.CharacterId,
                        ItemId = tih.ItemId,
                        QuantityChange = tih.QuantityChange,
                    }).OrderByDescending(p=> p.Timestamp).ToList();
        }

        public List<ChatLogInfo>? GetChatLogInfosByCharacterId(long characterId, DateTime startDate, DateTime endDate)
        {
            return (from tcl in TestDataFactory.TestChatlogs
                    where 
                    (
                        characterId == 0
                        || tcl.SenderCharacterId == characterId 
                        || tcl.ReceiverCharacterId == characterId
                    )
                    && tcl.SentTime >= startDate
                    && tcl.SentTime <= endDate
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
                    }).OrderByDescending(p=> p.SentTime).ToList();
        }
    }
}
