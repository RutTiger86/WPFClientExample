using System.Windows.Controls;
using WPFClientExample.ViewModels;

namespace WPFClientExample.Views
{
    /// <summary>
    /// UserInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserInfoView : UserControl
    {
        public UserInfoView(IUserInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
