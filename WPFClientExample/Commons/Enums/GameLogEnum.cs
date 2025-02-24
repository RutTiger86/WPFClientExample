using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.Commons.Enums
{
    public enum UserSearchType
    {
        AccountId = 0,
        AccountName = 1
    }

    public enum AccountStatus
    {
        ACTIVE,           // 정상 (계정이 활성화됨)
        SUSPENDED,        // 일시 정지 (일정 기간 동안 로그인 불가)
        BANNED,           // 영구 정지 (게임 이용 불가)
        RESTRICTED,       // 제한 계정 (일부 기능 사용 불가)
    }

    public enum CharacterClass
    {
        NONE =0,
        WARRIOR,           
        MAGE,        
        ARCHER,           
    }

    public enum CharacterRace
    {
        NONE =0,
        HUMAN,
        ELF,
        DWARF,
    }

}
