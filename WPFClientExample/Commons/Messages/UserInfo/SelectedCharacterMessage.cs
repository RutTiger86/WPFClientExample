using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Models.GameLog;

namespace WPFClientExample.Commons.Messages.UserInfo
{
    public class SelectedCharacterMessage(CharacterInfo selectedCharacterInfo) : ValueChangedMessage<CharacterInfo?>(selectedCharacterInfo)
    {
    }
}
