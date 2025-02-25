using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Models.DataBase
{
    public class Quest
    {
        public long Id { get; set; } // 퀘스트 고유 ID
        public string QuestName { get; set; } = string.Empty; // 퀘스트 이름
        public string Description { get; set; } = string.Empty; // 퀘스트 설명
        public int QuestType { get; set; } // 퀘스트 유형 (0 = Main, 1 = Side, 2 = Daily, 3 = Event)

        public int ObjectiveType { get; set; } // 목표 유형 (0 = Kill, 1 = Collect, 2 = Talk)
        public long ObjectiveTargetId { get; set; } // 목표 대상 ID (몬스터 ID, 아이템 ID, NPC ID 등)
        public int ObjectiveCount { get; set; } // 목표 개수

        public int RewardExp { get; set; } // 경험치 보상
        public int RewardGold { get; set; } // 골드 보상
        public long? RewardItemId { get; set; } // 보상 아이템 ID (없을 수도 있음)
    }

}
