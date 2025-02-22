using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models;

namespace WPFClientExample.Commons.Messages
{
    public class LoginMessage(UserInfo? loginUserInfo) : ValueChangedMessage<UserInfo?>(loginUserInfo)
    {
    }
}
