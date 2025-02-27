using System.Windows.Controls;
using WPFClientExample.ViewModels;

namespace WPFClientExample.Views
{
    /// <summary>
    /// SettingsVeiw.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ClientSettingsView : UserControl
    {
        public ClientSettingsView(IClientSettingsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
