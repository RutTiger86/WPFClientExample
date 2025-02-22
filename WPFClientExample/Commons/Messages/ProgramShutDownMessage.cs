using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CSharp.WPF.MVVM.Messages
{
    public class ProgramShutDownMessage(bool ShutDwonResult) : ValueChangedMessage<bool>(ShutDwonResult)
    {
    }
}
