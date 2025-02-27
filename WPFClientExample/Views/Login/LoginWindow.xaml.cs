using System.Windows;
using WPFClientExample.ViewModels.Login;

namespace WPFClientExample.Views.Login
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(ILoginWindowModel windowModel)
        {
            InitializeComponent();
            this.DataContext = windowModel;
        }
    }
}
