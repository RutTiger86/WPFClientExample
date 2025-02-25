using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Commons.Enums
{
    public enum USER_SEARCH_TYPE
    {
        Id = 0,
        Name = 1
    }


    public enum ACCOUNT_STATE
    {
        ACTIVE,           // 정상 (계정이 활성화됨)
        SUSPENDED,        // 일시 정지 (일정 기간 동안 로그인 불가)
        BANNED,           // 영구 정지 (게임 이용 불가)
        RESTRICTED,       // 제한 계정 (일부 기능 사용 불가)
    }

    public enum CHARACTER_CLASS
    {
        NONE =0,
        WARRIOR,           
        MAGE,        
        ARCHER,           
    }

    public enum CHARACTER_RACE
    {
        NONE =0,
        HUMAN,
        ELF,
        DWARF,
    }

    public enum EQUIP_SLOT_TYPE
    {
        HEAD,
        CHEST,
        LEGS,
        GLOVES,
        BOOTS,
        RING_1,
        RING_2,
        EARRING,
        MAIN_HAND_WEAPON,
        OFF_HAND_WEAPON
    }

    public enum ITEM_GRADE
    {
        NOMAL,
        RARE,
        UNIQUE,
        LEGENDARY,
        MYTHIC
    }

    public enum ITEM_TYPE
    {
        CONSUMABLE,     // 소모성 아이템
        EQUIPMENT,      // 장착 아이템
        QUEST,          // 퀘스트 아이템
        TRADEABLE       // 판매용 아이템
    }
    public enum CHAT_TYPE
    {
        GLOBAL,  // 전체 채팅 (서버 내 모든 유저가 볼 수 있음)
        GUILD,   // 길드 채팅 (같은 길드 멤버끼리만 보임)
        PARTY,   // 파티 채팅 (같은 파티원끼리만 보임)
        WHISPER, // 귓속말 (특정 유저에게만 전달됨)
        LOCAL    // 일반 채팅 (같은 지역 내에서만 보임)
    }

    public enum QUEST_TYPE
    {
        MAIN,
        SIDE,
        DAILY,
        EVENT,
    }

    public enum QUEST_STATUES
    {
        IN_PROGRESS,
        COMPLETED,
        FAILED,
        CANCLE,
    }

    public enum INVENTORY_CHANGE_TYPE
    {
        OBTAINED,
        USED,
        DELETED,
        TRADED
    }


}
