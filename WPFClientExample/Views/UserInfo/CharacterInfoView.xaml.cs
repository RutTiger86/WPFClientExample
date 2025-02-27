using System.Windows.Controls;
using WPFClientExample.ViewModels.UserInfo;

namespace WPFClientExample.Views.UserInfo
{
    /// <summary>
    /// CharacterInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CharacterInfoView : UserControl
    {
        public CharacterInfoView(ICharacterInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
