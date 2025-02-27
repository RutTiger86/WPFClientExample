using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WPFClientExample.Commons.Messages
{
    public class LogoutMessage(bool LogoutResult) : ValueChangedMessage<bool>(LogoutResult)
    {
    }
}
