using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Models.DataBase;

namespace WPFClientExample.Commons.Messages
{
    public class LoginMessage(AuthAccount? loginUserInfo) : ValueChangedMessage<AuthAccount?>(loginUserInfo)
    {
    }
}
