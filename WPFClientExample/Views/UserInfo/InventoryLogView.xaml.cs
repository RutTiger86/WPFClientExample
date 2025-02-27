using System.Windows.Controls;
using WPFClientExample.ViewModels.UserInfo;

namespace WPFClientExample.Views.UserInfo
{
    /// <summary>
    /// InventoryLogView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InventoryLogView : UserControl
    {
        public InventoryLogView(IInventoryLogViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
