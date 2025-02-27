using System.Windows.Controls;
using WPFClientExample.ViewModels;

namespace WPFClientExample.Views
{
    /// <summary>
    /// ChatLogView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatLogView : UserControl
    {
        public ChatLogView(IChatLogViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
