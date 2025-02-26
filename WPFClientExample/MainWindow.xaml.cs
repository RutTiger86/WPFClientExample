using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClientExample.Models;

namespace WPFClientExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem && selectedItem.Tag is int menuId && DataContext is MainWindowModel viewModel)
            {
                viewModel.NavigateToCommand.Execute(menuId);
            }
        }

    }
}