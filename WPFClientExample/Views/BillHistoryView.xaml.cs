using System.Windows.Controls;
using WPFClientExample.ViewModels;

namespace WPFClientExample.Views
{
    /// <summary>
    /// BillHisotryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BillHistoryView : UserControl
    {
        public BillHistoryView(IBillHistoryViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
