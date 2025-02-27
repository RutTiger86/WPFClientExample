namespace WPFClientExample.Models.DataBase
{
    public class CharacterQuestProgress
    {
        public long Id { get; set; }
        public long CharacterId { get; set; } // 캐릭터 ID (퀘스트 수행자)
        public long QuestId { get; set; } // 진행 중인 퀘스트 ID (QuestBase와 연결)
        public int QuestStatus { get; set; } // 퀘스트 상태 (0 = In Progress, 1 = Completed, 2 = Failed)

        public int CurrentStep { get; set; } // 현재 진행 단계
        public int CurrentCount { get; set; } // 목표 달성 개수
        public bool IsCanceled { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow; // 퀘스트 시작 시간
        public DateTime? CompletionTime { get; set; } // 퀘스트 완료 시간 (완료 시 업데이트)
        public DateTime? CanceledTime { get; set; } // 퀘스트 취소 시간 (취소 시 업데이트)
    }

}
