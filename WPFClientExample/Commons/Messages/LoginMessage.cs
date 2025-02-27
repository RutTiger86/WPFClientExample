using CommunityToolkit.Mvvm.Messaging.Messages;
using WPFClientExample.Models.DataBase;

namespace WPFClientExample.Commons.Messages
{
    public class LoginMessage(AuthAccount? loginUserInfo) : ValueChangedMessage<AuthAccount?>(loginUserInfo)
    {
    }
}
