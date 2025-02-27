using CommunityToolkit.Mvvm.Messaging.Messages;
using WPFClientExample.Models.GameLog;

namespace WPFClientExample.Commons.Messages.UserInfo
{
    public class SelectedCharacterMessage(CharacterInfo selectedCharacterInfo) : ValueChangedMessage<CharacterInfo?>(selectedCharacterInfo)
    {
    }
}
