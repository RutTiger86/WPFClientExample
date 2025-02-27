namespace WPFClientExample.Models.DataBase
{
    public class ChatLog
    {
        public long Id { get; set; } // 메시지 고유 ID
        public int ChatType { get; set; } // 채팅 유형 (전체, 길드, 귓속말, 일반 등)
        public long ChatRoomId { get; set; } // 채팅방 ID (길드, 파티 등 그룹별 구분)

        public long SenderCharacterId { get; set; } // 보낸 캐릭터 ID
        public required string SenderName { get; set; } // 보낸 캐릭터명

        public long? ReceiverCharacterId { get; set; } // 받는 캐릭터 ID (귓속말일 경우)
        public string? ReceiverName { get; set; } // 받는 캐릭터명 (귓속말일 경우)

        public required string Message { get; set; }// 채팅 메시지 내용
        public DateTime SentTime { get; set; } // 메시지 보낸 시간
    }
}
