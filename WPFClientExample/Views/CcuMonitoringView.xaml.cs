using System.Windows.Controls;
using WPFClientExample.ViewModels;

namespace WPFClientExample.Views
{
    /// <summary>
    /// CcuMonitoringView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CcuMonitoringView : UserControl
    {
        public CcuMonitoringView(ICcuMonitoringViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
