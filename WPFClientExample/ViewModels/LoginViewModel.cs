using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientExample.ViewModels
{
    public partial class LoginViewModel:ObservableObject
    {
        [ObservableProperty]
        private string title = "Login Page";
    }
}
